namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class ProjectileData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ProjectileData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ProjectileData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public int TilesPerSecond
        {
            get; set;
        }
    }
}
