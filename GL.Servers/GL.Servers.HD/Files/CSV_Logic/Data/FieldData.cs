namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FieldData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FieldData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FieldData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ToolIconExportName
        {
            get; set;
        }

        public int Value
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int Harvest
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

        public int OrderPrice
        {
            get; set;
        }

        public int OrderExp
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public int InstantFertilize
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public int OrderValue
        {
            get; set;
        }

        public int BoatOrderValue
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public int DumbValue
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int DiamondPrice
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

        public string BuySound
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int Difficulty
        {
            get; set;
        }
    }
}
