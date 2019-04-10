namespace GL.Servers.GS.Files
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using GL.Servers.GS.Core;

    internal class Home
    {
        internal static string Starting_Home = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        internal Home()
        {
            if (Directory.Exists("Gamefiles/level/"))
            {
                if (File.Exists("Gamefiles/level/starting_home.json"))
                {
                    Home.Starting_Home = File.ReadAllText("Gamefiles/level/starting_home.json", Encoding.UTF8);
                    Home.Starting_Home = Regex.Replace(Home.Starting_Home, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                }
                else
                {
                    Resources.Logger.Error(this.GetType().Name + " : " + "Couldn't load the starting home json file, the file is missing.");
                }
            }
            else
            {
                Resources.Logger.Error(this.GetType().Name + " : " + "Couldn't load the starting home json file, the folder is missing.");
            }
        }
    }
}