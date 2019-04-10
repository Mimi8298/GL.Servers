namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GathererData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GathererData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GathererData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ShopIconExportName
        {
            get; set;
        }

        public string GathererMine
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

        public string ExportName
        {
            get; set;
        }

        public string StateNestTakeOffExportName
        {
            get; set;
        }

        public int StateNestTakeOffMS
        {
            get; set;
        }

        public string StateMoveExportName
        {
            get; set;
        }

        public string StateMineLandingExportName
        {
            get; set;
        }

        public int StateMineLandingMS
        {
            get; set;
        }

        public string StateMineGatherExportName
        {
            get; set;
        }

        public string StateMineTakeOffExportName
        {
            get; set;
        }

        public int StateMineTakeOffMS
        {
            get; set;
        }

        public string StateMoveWithResourceExportName
        {
            get; set;
        }

        public string StateNestLandingExportName
        {
            get; set;
        }

        public int StateNestLandingMS
        {
            get; set;
        }

        public string StateIdleExportName
        {
            get; set;
        }

        public string StateSleepExportName
        {
            get; set;
        }

        public string StateReadyExportName
        {
            get; set;
        }

        public string StateTappedExportName
        {
            get; set;
        }

        public string StateTappedWithResourceExportName
        {
            get; set;
        }

        public int MovementSpeed
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
    }
}
