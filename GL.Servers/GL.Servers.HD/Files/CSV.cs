namespace GL.Servers.HD.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.HD.Core;
    using GL.Servers.HD.Files.CSV_Reader;

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

            CSV.Gamefiles.Add(1, "data/lumberjacks.csv");
            CSV.Gamefiles.Add(2, "data/cash_packages.csv");
            CSV.Gamefiles.Add(3, "data/decos.csv");

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