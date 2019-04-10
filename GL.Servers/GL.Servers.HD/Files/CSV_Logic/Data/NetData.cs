namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NetData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NetData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NetData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ToolIconExportName
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

        public string NetType
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public int RequirementAmount
        {
            get; set;
        }

        public string Requirement
        {
            get; set;
        }

        public int TimeMin
        {
            get; set;
        }

        public string ProcessingBuilding
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public int InitialAmount
        {
            get; set;
        }

        public int FishingDurationMinutes
        {
            get; set;
        }

        public int InstantCompleteFishingPrice
        {
            get; set;
        }

        public int DropCountMin
        {
            get; set;
        }

        public int DropCountMax
        {
            get; set;
        }

        public string DropType
        {
            get; set;
        }

        public int DropTypeProbability
        {
            get; set;
        }

        public int DropTypeMax
        {
            get; set;
        }

        public string CollectHUD
        {
            get; set;
        }

        public int CollectHUDSlots
        {
            get; set;
        }

        public string IngameAnimation
        {
            get; set;
        }

        public string PlaceSound
        {
            get; set;
        }

        public string ReadySound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public string TapNetSound
        {
            get; set;
        }
    }
}
