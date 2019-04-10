namespace GL.Servers.Files
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using GL.Servers.Extensions;

    public class Calendar
    {
        public static string JSON = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Calendar"/> class.
        /// </summary>
        public static void Initialize()
        {
            if (Directory.Exists("Gamefiles/offlinedata/"))
            {
                if (File.Exists("Gamefiles/offlinedata/calendar.json"))
                {
                    Calendar.JSON = File.ReadAllText("Gamefiles/offlinedata/calendar.json", Encoding.UTF8);
                    Calendar.JSON = Regex.Replace(Calendar.JSON, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

                    Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Home).Name, 15) + " : " + "The Calendar JSON has been loaded.");
                }
                else
                {
                    Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Home).Name, 15) + " : " + "Couldn't load the starting home json file, the file is missing.");
                }
            }
            else
            {
                Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Home).Name, 15) + " : " + "Couldn't load the starting home json file, the folder is missing.");
            }
        }
    }
}