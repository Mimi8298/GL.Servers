namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GathererHabitatData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GathererHabitatData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GathererHabitatData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ExportNameMiddle
        {
            get; set;
        }

        public string ExportNameTop
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

        public string ShopIconExportName
        {
            get; set;
        }

        public string CollectIconExportName
        {
            get; set;
        }

        public string Nest
        {
            get; set;
        }

        public int NestCount
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string MaxHabitatsColumnName
        {
            get; set;
        }

        public string BuySound
        {
            get; set;
        }

        public string RandomSounds
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string ShopDescriptionTID
        {
            get; set;
        }

        public int MasteryGatherCount
        {
            get; set;
        }

        public int MasteryDiamondPrice
        {
            get; set;
        }
    }
}
