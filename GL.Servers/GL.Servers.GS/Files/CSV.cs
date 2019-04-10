namespace GL.Servers.GS.Files
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GL.Servers.GS.Core;
    using GL.Servers.GS.Files.CSV_Reader;
    using GL.Servers.Files.CSV_Reader;

    internal class CSV
    {
        internal static readonly Dictionary<int, string> Gamefiles = new Dictionary<int, string>();

        internal static Gamefiles Tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSV"/> class.
        /// </summary>
        internal CSV()
        {
            CSV.Tables = new Gamefiles();
            
            Parallel.ForEach(CSV.Gamefiles, File =>
            {
                if (new System.IO.FileInfo(File.Value).Exists)
                {
                    CSV.Tables.Initialize(new Table(File.Value), File.Key);
                }
                else
                {
                    Resources.Logger.Error(this.GetType().Name + " : " + "The CSV file at \"" + File.Value + "\" is missing, aborting.");
                }
            });

            Console.WriteLine("Loaded " + CSV.Gamefiles.Count + " gamefiles, and stored them in memory." + Environment.NewLine);
        }
    }
}