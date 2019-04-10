namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AnimalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AnimalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AnimalData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string Feed
        {
            get; set;
        }

        public string Good
        {
            get; set;
        }

        public int Value
        {
            get; set;
        }

        public int ProcessValue
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

        public string StateIdleExportName
        {
            get; set;
        }

        public int StateIdleMinMS
        {
            get; set;
        }

        public int StateIdleMaxMS
        {
            get; set;
        }

        public string StatePanicExportName
        {
            get; set;
        }

        public int StatePanicMinMS
        {
            get; set;
        }

        public int StatePanicMaxMS
        {
            get; set;
        }

        public string StateMoveExportName
        {
            get; set;
        }

        public int MovementSpeed
        {
            get; set;
        }

        public int PanicMovementSpeed
        {
            get; set;
        }

        public string StateHungryExportName
        {
            get; set;
        }

        public string StateReadyExportName
        {
            get; set;
        }

        public string HarvestExportName
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string ReadyNotificationName
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

        public string FeedSound
        {
            get; set;
        }
    }
}
