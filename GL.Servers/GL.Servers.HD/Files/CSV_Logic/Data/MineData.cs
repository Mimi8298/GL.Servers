namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MineData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MineData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MineData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string TapProcessingSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public string Tool
        {
            get; set;
        }

        public int NumOres
        {
            get; set;
        }

        public int DiamondChance
        {
            get; set;
        }

        public int RepairPrice
        {
            get; set;
        }

        public int TimeMin
        {
            get; set;
        }

        public int TimeSec
        {
            get; set;
        }

        public int ExpToBuild
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }
    }
}
