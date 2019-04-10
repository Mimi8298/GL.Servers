namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ScarecrowData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ScarecrowData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ScarecrowData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public string IdleAnim
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int GuestTilePositionX
        {
            get; set;
        }

        public int GuestTilePositionY
        {
            get; set;
        }

        public int BoatTilePositionX
        {
            get; set;
        }

        public int BoatTilePositionY
        {
            get; set;
        }

        public int NeighborhoodTilePositionX
        {
            get; set;
        }

        public int NeighborhoodTilePositionY
        {
            get; set;
        }
    }
}
