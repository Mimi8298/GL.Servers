namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GathererMineData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GathererMineData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GathererMineData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public bool IsFruit
        {
            get; set;
        }

        public string DeadExportName
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

        public int TimeMin
        {
            get; set;
        }

        public int FruitCount
        {
            get; set;
        }

        public int ExpClear
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public string Tool
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
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

        public int Price
        {
            get; set;
        }

        public string CollectIconExportName
        {
            get; set;
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public bool BirdCanLand
        {
            get; set;
        }

        public string FBHelpTitle
        {
            get; set;
        }

        public string FBHelpText
        {
            get; set;
        }

        public string FBHelpDescription
        {
            get; set;
        }

        public string FBPictureURL
        {
            get; set;
        }

        public string GathererMineResource
        {
            get; set;
        }

        public string ChildExportName
        {
            get; set;
        }

        public int MineX
        {
            get; set;
        }

        public int MineY
        {
            get; set;
        }

        public string ResourceExportName
        {
            get; set;
        }

        public string MineExportName
        {
            get; set;
        }

        public int MineCount
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public string BuySound
        {
            get; set;
        }
    }
}
