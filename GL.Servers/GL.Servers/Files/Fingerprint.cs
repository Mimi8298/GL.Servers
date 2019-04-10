namespace GL.Servers.Files
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using GL.Servers.Extensions;

    using Newtonsoft.Json.Linq;

    public class Fingerprint
    {
        public static string Json;
        public static string Sha;
        public static string[] Version;

        public static bool Custom;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        public static void Initialize()
        {
            try
            {
                if (!Patches())
                {
                    if (File.Exists(@"Gamefiles\fingerprint.json"))
                    {
                        Fingerprint.Json        = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Gamefiles\fingerprint.json");
                        JObject _Json           = JObject.Parse(Fingerprint.Json);
                        Fingerprint.Sha         = _Json["sha"].ToObject<string>();
                        Fingerprint.Version     = _Json["version"].ToObject<string>().Split('.');

                        Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Fingerprint).Name, 15) + " : " + "The Fingerprint has been loaded, with version " + string.Join(".", Fingerprint.Version) + ".");
                    }
                    else
                    {
                        Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Fingerprint).Name, 15) + " : " + "The Fingerprint cannot be loaded, the file does not exist.");
                    }
                }
                else
                {
                    Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Fingerprint).Name, 15) + " : " + "The Fingerprint is loaded and custom, with version " + string.Join(".", Fingerprint.Version) + ".");
                }
            }
            catch (Exception Exception)
            {
                Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Fingerprint).Name, 15) + " : " + Exception.GetType().Name + " while parsing the fingerprint.");
            }
        }

        private static bool Patches()
        {
            bool _Result = false;

            if (File.Exists(Directory.GetCurrentDirectory() + "\\Patchs\\VERSION"))
            {
                string[] _Lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Patchs\\VERSION");

                if (!string.IsNullOrEmpty(_Lines[0]))
                {
                    Fingerprint.Version = _Lines[0].Split('.');

                    if (_Lines.Length > 1 && !string.IsNullOrEmpty(_Lines[1]))
                    {
                        Fingerprint.Sha = _Lines[1];

                        if (File.Exists(Directory.GetCurrentDirectory() + "\\Patchs\\" + Fingerprint.Sha + "\\fingerprint.json"))
                        {
                            Fingerprint.Json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Patchs\\" + Fingerprint.Sha + "\\fingerprint.json");
                            Fingerprint.Json.Trim('\n', '\r');

                            _Result = true;
                            Fingerprint.Custom = true;
                        }
                    }
                }
            }

            return _Result;
        }
    }
}