namespace GL.Servers.BS.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files.CSV_Reader;

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

            CSV.Gamefiles.Add(1,  @"Gamefiles/csv_logic/globals.csv");
            CSV.Gamefiles.Add(5,  @"Gamefiles/csv_logic/resources.csv");
            CSV.Gamefiles.Add(8,  @"Gamefiles/csv_logic/alliance_badges.csv");
            CSV.Gamefiles.Add(15, @"Gamefiles/csv_logic/locations.csv");
            CSV.Gamefiles.Add(16, @"Gamefiles/csv_logic/characters.csv");
            CSV.Gamefiles.Add(23, @"Gamefiles/csv_logic/cards.csv");
            CSV.Gamefiles.Add(28, @"Gamefiles/csv_logic/player_thumbnails.csv");

            foreach (var File in CSV.Gamefiles)
            {
                if (new FileInfo(File.Value).Exists)
                {
                    CSV.Tables.Initialize(new Table(File.Value), File.Key);
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