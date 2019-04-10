namespace GL.Servers.SP.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Files.CSV_Reader;

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

            CSV.Gamefiles.Add(1, "csv/locales.csv");
            CSV.Gamefiles.Add(2, "csv/globals.csv");
            CSV.Gamefiles.Add(3, "csv/billing_packages.csv");
            CSV.Gamefiles.Add(4, "csv/achievements.csv");
            CSV.Gamefiles.Add(5, "csv/credits.csv");
            CSV.Gamefiles.Add(6, "csv/sounds.csv");
            CSV.Gamefiles.Add(7, "csv/effects.csv");
            CSV.Gamefiles.Add(8, "csv/particle_emitters.csv");
            CSV.Gamefiles.Add(9, "csv/resource.csv");
            CSV.Gamefiles.Add(10, "csv/puzzles.csv");
            CSV.Gamefiles.Add(11, "csv/blocks.csv");
            CSV.Gamefiles.Add(12, "csv/monsters.csv");
            CSV.Gamefiles.Add(14, "csv/levels.csv");
            CSV.Gamefiles.Add(15, "csv/tutorials.csv");
            CSV.Gamefiles.Add(16, "csv/heroes.csv");
            CSV.Gamefiles.Add(17, "csv/boosts.csv");
            CSV.Gamefiles.Add(18, "csv/attack_types.csv");
            CSV.Gamefiles.Add(19, "csv/special_moves.csv");
            CSV.Gamefiles.Add(20, "csv/playlists.csv");
            CSV.Gamefiles.Add(21, "csv/projectiles.csv");
            CSV.Gamefiles.Add(22, "csv/match_patterns.csv");
            CSV.Gamefiles.Add(23, "csv/facebook_errors.csv");
            CSV.Gamefiles.Add(24, "csv/gates.csv");

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