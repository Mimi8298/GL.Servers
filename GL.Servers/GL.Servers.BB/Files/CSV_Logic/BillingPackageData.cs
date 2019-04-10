namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class BillingPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BillingPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BillingPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string BannerTID
        {
            get; set;
        }

        public string BonusTID
        {
            get; set;
        }

        public int BonusValue
        {
            get; set;
        }

        public bool Disabled
        {
            get; set;
        }

        public int Diamonds
        {
            get; set;
        }

        public int Workers
        {
            get; set;
        }

        public bool InstantTraining
        {
            get; set;
        }

        public int ExpirationDays
        {
            get; set;
        }

        public bool IsSubscription
        {
            get; set;
        }

        public string CostTID
        {
            get; set;
        }

        public int USD
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string ShopItemExportName
        {
            get; set;
        }

        public int RMB
        {
            get; set;
        }

        public bool KunlunOnly
        {
            get; set;
        }

        public int LenovoID
        {
            get; set;
        }

        public bool RED
        {
            get; set;
        }

        public bool ExistsApple
        {
            get; set;
        }

        public bool ExistsAndroid
        {
            get; set;
        }

        public int OverrideDisabledOnMinorVersion
        {
            get; set;
        }
    }
}
