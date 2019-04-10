namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TileData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TileData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TileData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public bool BlocksBuilding
        {
            get; set;
        }

        public string GreenTile
        {
            get; set;
        }

        public string SelectedTile
        {
            get; set;
        }

        public int Set
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public bool Platform
        {
            get; set;
        }

        public bool TrainCrossing
        {
            get; set;
        }

        public bool TrainCrossingWaitingArea
        {
            get; set;
        }

        public bool WaterTile
        {
            get; set;
        }

        public bool SanctuaryTile
        {
            get; set;
        }

        public bool SanctuaryViewingArea
        {
            get; set;
        }
    }
}
