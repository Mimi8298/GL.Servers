namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NetMakerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NetMakerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NetMakerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public int UnlockLevel
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

        public int Slots
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public int PriceIncrease
        {
            get; set;
        }

        public int DoPriceIncreaseEveryNObject
        {
            get; set;
        }

        public string MaxHabitatsColumnName
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameMastered
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

        public string BuySound
        {
            get; set;
        }

        public string RandomSounds
        {
            get; set;
        }

        public int BuildingMasteryHours
        {
            get; set;
        }

        public int BuildingMasteryBonusPercent
        {
            get; set;
        }

        public string BuildingMasteryBonusType
        {
            get; set;
        }

        public int BuildingMasteryStarDiamondPrice
        {
            get; set;
        }

        public bool BuildingMasteryEnabled
        {
            get; set;
        }

        public int RepairPrice
        {
            get; set;
        }
    }
}
