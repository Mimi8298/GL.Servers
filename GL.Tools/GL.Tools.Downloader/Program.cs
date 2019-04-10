namespace GL.Tools.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    internal class Program
    {
        private const string Game      = "CLASH ROYALE";
        private static string SHA;

        private static JObject JSON;

        /// <summary>
        /// Gets the patch update host for the specified game.
        /// </summary>
        private static string Host
        {
            get
            {
                switch (Program.Game)
                {
                    case "CLASH OF CLANS":
                    {
                        return "http://b46f744d64acd2191eda-3720c0374d47e9a0dd52be4d281c260f.r11.cf2.rackcdn.com/";
                    }

                    case "CLASH ROYALE":
                    {
                        return "http://7166046b142482e67b30-2a63f4436c967aa7d355061bd0d924a1.r65.cf1.rackcdn.com/";
                    }

                    case "BOOM BEACH":
                    {
                        return "http://df70a89d32075567ba62-1e50fe9ed7ef652688e6e5fff773074c.r40.cf1.rackcdn.com/";
                    }

                    case "BRAWL STARS":
                    {
                        return "http://a678dbc1c015a893c9fd-4e8cc3b1ad3a3c940c504815caefa967.r87.cf2.rackcdn.com/";
                    }
                }
            }
        }

        /// <summary>
        /// The ignored files, not meant to be downloaded.
        /// </summary>
        private static string[] Ignored =
        {
            // "sc", "sfx"
        };

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Gamefiles/");

            Program.JSON    = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "/fingerprint.json"));
            Program.SHA     = Program.JSON["sha"].ToObject<string>();

            ParallelOptions Parameters = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            Parallel.ForEach(Program.JSON["files"], Parameters, Token =>
            {
                string[] Path       = Token["file"].ToObject<string>().Split('/');

                if (!Program.Ignored.Any(Path[0].Contains))
                {
                    Program.Download(Path);
                }
                else
                {
                    Debug.WriteLine("[*] Skipping the download of a SC file, '/" + string.Join("/", Path.Take(Path.Length)) + "'");
                }
            });
        }

        /// <summary>
        /// Downloads the specified file.
        /// </summary>
        /// <param name="Path">The file path.</param>
        private static void Download(IReadOnlyCollection<string> Path)
        {
            DirectoryInfo Folder            = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Gamefiles/" + string.Join("/", Path.Take(Path.Count - 1)));
            WebClient Browser               = new WebClient();

            Console.WriteLine("[*] Downloading '/" + string.Join("/", Path.Take(Path.Count)) + "'");

            byte[] Result                   = Browser.DownloadData(Program.Host + Program.SHA + "/" + string.Join("/", Path.Take(Path.Count)));

            if (!string.IsNullOrEmpty(Folder.FullName + "/" + Path.Last()))
            {
                File.WriteAllBytes(Folder.FullName + "/" + Path.Last(), Result);
            }
        }
    }
}