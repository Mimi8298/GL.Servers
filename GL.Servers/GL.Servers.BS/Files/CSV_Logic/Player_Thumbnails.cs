namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Player_Thumbnails : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Player_Thumbnails"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Player_Thumbnails(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public int RequiredExpLevel
        {
            get; set;
        }

        public int RequiredTotalTrophies
        {
            get; set;
        }

        public string RequiredHero
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }
    }
}
