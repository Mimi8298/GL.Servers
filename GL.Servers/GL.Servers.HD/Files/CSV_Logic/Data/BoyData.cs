namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class BoyData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BoyData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BoyData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int HireDays
        {
            get; set;
        }

        public int HirePrice
        {
            get; set;
        }

        public int DiscountHirePrice
        {
            get; set;
        }

        public int DiscountPercent
        {
            get; set;
        }

        public int OfferMinutesMin
        {
            get; set;
        }

        public int OfferMinutesMax
        {
            get; set;
        }

        public int InitialHireDays
        {
            get; set;
        }

        public int CooldownMinutes
        {
            get; set;
        }

        public int CancelMinutes
        {
            get; set;
        }

        public int ItemCountMin
        {
            get; set;
        }

        public int ItemCountMax
        {
            get; set;
        }

        public int PriceCoefficient
        {
            get; set;
        }

        public int ItemVariance
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

        public string PopupSleepyExportName
        {
            get; set;
        }

        public int OfferIntervalDaysMin
        {
            get; set;
        }

        public int OfferIntervalDaysMax
        {
            get; set;
        }

        public int CollectIconOffsetX
        {
            get; set;
        }

        public int CollectIconOffsetY
        {
            get; set;
        }
    }
}
