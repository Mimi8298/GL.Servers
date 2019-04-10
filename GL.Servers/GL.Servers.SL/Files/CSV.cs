namespace GL.Servers.SL.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.SL.Core;
    using GL.Servers.SL.Files.CSV_Reader;

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

            CSV.Gamefiles.Add(1, "csv_logic/locales.csv");
            CSV.Gamefiles.Add(2, "csv_logic/resources.csv");
            CSV.Gamefiles.Add(3, "csv_logic/effects.csv");
            CSV.Gamefiles.Add(4, "csv_logic/particle_emitters.csv");
            CSV.Gamefiles.Add(5, "csv_logic/globals.csv");
            CSV.Gamefiles.Add(6, "csv_logic/quests.csv");

            CSV.Gamefiles.Add(10, "csv_logic/worlds.csv");
            CSV.Gamefiles.Add(11, "csv_logic/heroes.csv");

            CSV.Gamefiles.Add(24, "csv_logic/taunts.csv");
            CSV.Gamefiles.Add(25, "csv_logic/decos.csv");
            CSV.Gamefiles.Add(26, "csv_logic/variables.csv");
            CSV.Gamefiles.Add(32, "csv_logic/energy_packages.csv");

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

            Logging.Info(typeof(CSV), "Loaded and stored in memory " + CSV.Gamefiles.Count + " gamefiles.");
        }
    }
}