namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class HelperData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HelperData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HelperData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public int HireDays
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public string Voucher
        {
            get; set;
        }

        public int VoucherCount
        {
            get; set;
        }

        public int FirstTryHireDays
        {
            get; set;
        }

        public int DiscountPercent
        {
            get; set;
        }

        public int OfferMinutes
        {
            get; set;
        }

        public int FreeUseDelayDays
        {
            get; set;
        }

        public int FreeUseDelayDaysSpender
        {
            get; set;
        }

        public string PopupFileName
        {
            get; set;
        }

        public string PopupExportName
        {
            get; set;
        }

        public string SelectionExportName
        {
            get; set;
        }

        public int MinimumCropLimit
        {
            get; set;
        }

        public int ExcessFeedPerAnimal
        {
            get; set;
        }

        public int LowResourceLimitCrop
        {
            get; set;
        }

        public int LowResourceLimitOther
        {
            get; set;
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public string AttentionSound
        {
            get; set;
        }

        public int Version
        {
            get; set;
        }

        public int VersionExtraHireDays
        {
            get; set;
        }
    }
}
