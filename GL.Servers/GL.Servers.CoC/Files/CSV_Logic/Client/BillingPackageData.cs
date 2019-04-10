namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class BillingPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BillingPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BillingPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // BillingPackageData.
        }

        public string TID
        {
            get; set;
        }

        public bool Disabled
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

        public int Diamonds
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

        public string OfferItemExportName
        {
            get; set;
        }

        public int Order
        {
            get; set;
        }

        public bool RED
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

        public string TencentID
        {
            get; set;
        }

        public bool isOfferPackage
        {
            get; set;
        }

        public bool OfferedByCalendar
        {
            get; set;
        }
    }
}
