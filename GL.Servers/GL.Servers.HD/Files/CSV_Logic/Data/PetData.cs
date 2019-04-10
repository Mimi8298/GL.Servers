namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PetData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PetData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PetData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int HappyDurationMinutes
        {
            get; set;
        }

        public int FedDurationMinutes
        {
            get; set;
        }

        public string Habitat
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

        public int MaxPathLength
        {
            get; set;
        }

        public int MaxDistanceFromHouse
        {
            get; set;
        }

        public int MaxDistanceFromHouseHungry
        {
            get; set;
        }

        public int Exp
        {
            get; set;
        }

        public string Vouchers
        {
            get; set;
        }

        public int VoucherCount
        {
            get; set;
        }

        public string StateIdleExportName
        {
            get; set;
        }

        public string StateIdleHungryExportName
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

        public string StateRunExportName
        {
            get; set;
        }

        public string StateMoveExportName
        {
            get; set;
        }

        public string StateMoveHungryExportName
        {
            get; set;
        }

        public int StateMoveMinMS
        {
            get; set;
        }

        public int StateMoveMaxMS
        {
            get; set;
        }

        public int StateRunMinMS
        {
            get; set;
        }

        public int StateRunMaxMS
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public int HungrySpeed
        {
            get; set;
        }

        public int RunSpeed
        {
            get; set;
        }

        public string StateEatExportName
        {
            get; set;
        }

        public int StateEatMinMS
        {
            get; set;
        }

        public int StateEatMaxMS
        {
            get; set;
        }

        public string StateSleepExportName
        {
            get; set;
        }

        public int StateSleepMinMS
        {
            get; set;
        }

        public int StateSleepMaxMS
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

        public string ReadyNotificationName
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

        public string BuySound
        {
            get; set;
        }

        public string FeedSound
        {
            get; set;
        }

        public string RandomSounds
        {
            get; set;
        }

        public string MaxCountColumnName
        {
            get; set;
        }

        public int SpecialDropCounterIncrease
        {
            get; set;
        }

        public bool RandomAnimationFrame
        {
            get; set;
        }

        public int ShopOrder
        {
            get; set;
        }
    }
}
