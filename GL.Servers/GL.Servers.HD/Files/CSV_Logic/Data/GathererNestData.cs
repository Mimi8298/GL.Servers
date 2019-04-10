namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GathererNestData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GathererNestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GathererNestData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string Gatherer
        {
            get; set;
        }

        public int MaxGatherer
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string CollectSound
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

        public string InputResource
        {
            get; set;
        }

        public int InputResourceAmount
        {
            get; set;
        }

        public string OutputGood
        {
            get; set;
        }

        public string ReadyNotificationName
        {
            get; set;
        }
    }
}
