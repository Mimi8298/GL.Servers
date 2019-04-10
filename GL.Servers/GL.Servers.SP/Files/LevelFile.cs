namespace GL.Servers.SP.Files
{
    using System.IO;
    using System.Text;
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class LevelFile
    {
        public static JObject StartingHome;
        public static Dictionary<string, string> Files;

        public static void Initialize()
        {
            LevelFile.Files        = new Dictionary<string, string>(200);
            LevelFile.StartingHome = JObject.Parse(File.ReadAllText("Gamefiles/level/starting_home.json", Encoding.UTF8));
        }
    }
}