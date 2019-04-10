namespace GL.Servers.Files
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using GL.Servers.Extensions;
    using Newtonsoft.Json.Linq;

    public class Home
    {
        public static string JSON = string.Empty;
        public static JObject JObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        public static void Initialize()
        {
            if (Directory.Exists("Gamefiles/level/"))
            {
                if (File.Exists("Gamefiles/level/starting_home.json"))
                {
                    Home.JSON   = File.ReadAllText("Gamefiles/level/starting_home.json", Encoding.UTF8);
                    Home.JSON   = Regex.Replace(Home.JSON, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

                    Home.JObject = JObject.Parse(Home.JSON);

                    Debug.WriteLine("[*] " + ConsolePad.Padding(typeof(Home).Name, 15) + " : " + "The Starting Home JSON has been loaded.");
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