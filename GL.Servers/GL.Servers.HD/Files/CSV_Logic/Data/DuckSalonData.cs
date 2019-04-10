namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DuckSalonData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DuckSalonData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DuckSalonData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
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

        public int Capacity
        {
            get; set;
        }

        public int NumIdleAnimation
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

        public string RandomSounds
        {
            get; set;
        }

        public int RepairPrice
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public int TimeMin
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

        public string CollectIconExportName
        {
            get; set;
        }

        public int UpgradeCashPrice
        {
            get; set;
        }
    }
}
