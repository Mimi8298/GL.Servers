namespace GL.Servers.CoC.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Reader;

    using GL.Servers.Files.CSV_Reader;

    internal class CSV
    {
        internal static readonly Dictionary<int, string> Gamefiles = new Dictionary<int, string>();

        internal static Gamefiles Tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSV"/> class.
        /// </summary>
        internal static void Initialize()
        {
            CSV.Tables = new Gamefiles();

            CSV.Gamefiles.Add(1, @"logic/buildings.csv");
            CSV.Gamefiles.Add(2, @"logic/locales.csv");
            CSV.Gamefiles.Add(3, @"logic/resources.csv");
            CSV.Gamefiles.Add(4, @"logic/characters.csv");
            // CSV.Gamefiles.Add(5, @"csv/animations.csv");
            CSV.Gamefiles.Add(6, @"logic/projectiles.csv");
            CSV.Gamefiles.Add(7, @"logic/building_classes.csv");
            CSV.Gamefiles.Add(8, @"logic/obstacles.csv");
            CSV.Gamefiles.Add(9, @"logic/effects.csv");
            // CSV.Gamefiles.Add(10, @"csv/particle_emitters.csv");
            CSV.Gamefiles.Add(11, @"logic/experience_levels.csv");
            CSV.Gamefiles.Add(12, @"logic/traps.csv");
            CSV.Gamefiles.Add(13, @"logic/alliance_badges.csv");
            CSV.Gamefiles.Add(14, @"logic/globals.csv");
            CSV.Gamefiles.Add(15, @"logic/townhall_levels.csv");
            CSV.Gamefiles.Add(16, @"logic/alliance_portal.csv");
            CSV.Gamefiles.Add(17, @"logic/npcs.csv");
            CSV.Gamefiles.Add(18, @"logic/decos.csv");
            CSV.Gamefiles.Add(19, @"csv/resource_packs.csv");
            CSV.Gamefiles.Add(20, @"logic/shields.csv");
            CSV.Gamefiles.Add(21, @"logic/missions.csv");
            CSV.Gamefiles.Add(22, @"csv/billing_packages.csv");
            CSV.Gamefiles.Add(23, @"logic/achievements.csv");
            CSV.Gamefiles.Add(25, @"csv/faq.csv");
            CSV.Gamefiles.Add(26, @"logic/spells.csv");
            CSV.Gamefiles.Add(27, @"csv/hints.csv");
            CSV.Gamefiles.Add(28, @"logic/heroes.csv");
            CSV.Gamefiles.Add(29, @"logic/leagues.csv");
            CSV.Gamefiles.Add(30, @"csv/news.csv");

            CSV.Gamefiles.Add(34, @"logic/alliance_badge_layers.csv");
            CSV.Gamefiles.Add(37, @"logic/variables.csv");
            CSV.Gamefiles.Add(38, @"logic/gem_bundles.csv");
            CSV.Gamefiles.Add(39, @"logic/village_objects.csv");

            foreach (var File in CSV.Gamefiles)
            {
                if (new FileInfo("Gamefiles/" + File.Value).Exists)
                {
                    CSV.Tables.Initialize(new Table("Gamefiles/" + File.Value), File.Key);
                }
                else
                {
                    Logging.Info(typeof(CSV), "The CSV file at \"" + File.Value + "\" is missing, aborting.");
                }
            }

            foreach (DataTable Table in CSV.Tables.DataTables.Values)
            {
                foreach (Data Data in Table.Datas)
                {
                    Data.LoadingFinished();
                }
            }

            Logging.Info(typeof(CSV), "Loaded and stored in memory " + CSV.Gamefiles.Count + " gamefiles.");
        }
    }
}
