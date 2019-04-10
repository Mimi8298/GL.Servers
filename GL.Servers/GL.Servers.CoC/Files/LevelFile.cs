namespace GL.Servers.CoC.Files
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic;
    using Newtonsoft.Json.Linq;

    public class LevelFile
    {
        internal static JObject StartingHome;
        internal static Dictionary<string, Home> Files;

        internal static void Initialize()
        {
            LevelFile.Files        = new Dictionary<string, Home>(200);
            LevelFile.StartingHome = JObject.Parse(File.ReadAllText("Gamefiles/level/starting_home.json", Encoding.UTF8));

            foreach (NpcData Data in CSV.Tables.Get(Gamefile.Npc).Datas)
            {
                if (Data.LevelFile != null)
                {
                    LevelFile.Files.Add(Data.LevelFile, new Home
                    {
                        LastSave = File.ReadAllText("Gamefiles/" + Data.LevelFile)
                    });
                }
            }
        }
    }
}