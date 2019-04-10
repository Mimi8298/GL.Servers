namespace GL.Servers.CR.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.CSV_Reader;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class CSV
    {
        internal static readonly Dictionary<int, string> Paths = new Dictionary<int, string>();

        internal static Gamefiles Tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSV"/> class.
        /// </summary>
        internal static void Initialize()
        {
            CSV.Paths.Add(50, @"Gamefiles/csv_client/background_decos.csv");
            CSV.Paths.Add(2, @"Gamefiles/csv_client/billing_packages.csv");
            CSV.Paths.Add(20, @"Gamefiles/csv_client/client_globals.csv");
            CSV.Paths.Add(11, @"Gamefiles/csv_client/effects.csv");
            CSV.Paths.Add(40, @"Gamefiles/csv_client/health_bars.csv");
            CSV.Paths.Add(62, @"Gamefiles/csv_client/helpshift.csv");
            CSV.Paths.Add(1, @"Gamefiles/csv_client/locales.csv");
            CSV.Paths.Add(41, @"Gamefiles/csv_client/music.csv");
            CSV.Paths.Add(58, @"Gamefiles/csv_client/news.csv");
            CSV.Paths.Add(21, @"Gamefiles/csv_client/particle_emitters.csv");
            CSV.Paths.Add(4, @"Gamefiles/csv_client/sounds.csv");
            
            CSV.Paths.Add(60, @"Gamefiles/csv_logic/achievements.csv");
            CSV.Paths.Add(16, @"Gamefiles/csv_logic/alliance_badges.csv");
            CSV.Paths.Add(59, @"Gamefiles/csv_logic/alliance_roles.csv");
            CSV.Paths.Add(22, @"Gamefiles/csv_logic/area_effect_objects.csv");
            CSV.Paths.Add(54, @"Gamefiles/csv_logic/arenas.csv");
            CSV.Paths.Add(9, @"Gamefiles/csv_logic/character_buffs.csv");
            CSV.Paths.Add(35, @"Gamefiles/csv_logic/characters.csv");
            CSV.Paths.Add(52, @"Gamefiles/csv_logic/chest_order.csv");
            CSV.Paths.Add(42, @"Gamefiles/csv_logic/decos.csv");
            CSV.Paths.Add(46, @"Gamefiles/csv_logic/exp_levels.csv");
            CSV.Paths.Add(43, @"Gamefiles/csv_logic/gamble_chests.csv");
            CSV.Paths.Add(3, @"Gamefiles/csv_logic/globals.csv");
            CSV.Paths.Add(15, @"Gamefiles/csv_logic/locations.csv");
            CSV.Paths.Add(18, @"Gamefiles/csv_logic/npcs.csv");
            CSV.Paths.Add(12, @"Gamefiles/csv_logic/predefined_decks.csv");
            CSV.Paths.Add(10, @"Gamefiles/csv_logic/projectiles.csv");
            CSV.Paths.Add(14, @"Gamefiles/csv_logic/rarities.csv");
            CSV.Paths.Add(57, @"Gamefiles/csv_logic/regions.csv");
            CSV.Paths.Add(55, @"Gamefiles/csv_logic/resource_packs.csv");
            CSV.Paths.Add(5, @"Gamefiles/csv_logic/resources.csv");
            CSV.Paths.Add(27, @"Gamefiles/csv_logic/spells_buildings.csv");
            CSV.Paths.Add(26, @"Gamefiles/csv_logic/spells_characters.csv");
            CSV.Paths.Add(29, @"Gamefiles/csv_logic/spells_heroes.csv");
            CSV.Paths.Add(28, @"Gamefiles/csv_logic/spells_other.csv");
            CSV.Paths.Add(53, @"Gamefiles/csv_logic/taunts.csv");
            CSV.Paths.Add(19, @"Gamefiles/csv_logic/treasure_chests.csv");
            CSV.Paths.Add(45, @"Gamefiles/csv_logic/tutorials_home.csv");
            CSV.Paths.Add(48, @"Gamefiles/csv_logic/tutorials_npc.csv");

            CSV.Tables = new Gamefiles();

            foreach (var File in CSV.Paths)
            {
                if (new FileInfo(File.Value).Exists)
                {
                    CSV.Tables.Initialize(new Table(File.Value), File.Key);
                }
                else
                {
                    Logging.Error(typeof(CSV), "The CSV file at \"" + File.Value + "\" is missing, aborting.");
                }
            }

            foreach (DataTable DataTable in CSV.Tables.DataTables.Values)
            {
                foreach (Data Data in DataTable.Datas)
                {
                    Data.LoadingFinished();
                }   
            }

            CSV.Tables.MaxExpLevel = CSV.Tables.Get(Gamefile.ExpLevel).Datas.Count;

            CSV.Tables.GoldData = CSV.Tables.Get(Gamefile.Resource).GetData<ResourceData>("Gold");
            CSV.Tables.FreeGoldData = CSV.Tables.Get(Gamefile.Resource).GetData<ResourceData>("FreeGold");
            CSV.Tables.ChestCountData = CSV.Tables.Get(Gamefile.Resource).GetData<ResourceData>("CardCount");
            CSV.Tables.ChestCountData = CSV.Tables.Get(Gamefile.Resource).GetData<ResourceData>("ChestCount");

            foreach (DataTable DataTable in CSV.Tables.DataTables.Values)
            {
                foreach (Data Data in DataTable.Datas)
                {
                    Data.LoadingFinished2();
                }
            }

            Logging.Info(typeof(CSV), "Loaded and stored in memory " + CSV.Paths.Count + " gamefiles.");
        }
    }
}