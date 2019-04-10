namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class OfferData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="OfferData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public OfferData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string BasePackage
        {
            get; set;
        }

        public string OfferPackages
        {
            get; set;
        }

        public int IntervalDays
        {
            get; set;
        }

        public int DurationMinutes
        {
            get; set;
        }

        public bool NonBuyer
        {
            get; set;
        }

        public bool Buyer
        {
            get; set;
        }

        public bool StarterPackage
        {
            get; set;
        }

        public int StarterPackageOfferTimes
        {
            get; set;
        }

        public int StarterPackagePauses
        {
            get; set;
        }

        public int StarterPackagePauseHours
        {
            get; set;
        }

        public int StarterPackageLevelUpDelayMinutes
        {
            get; set;
        }

        public string RequiredTutorial
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int ReputationUnlockLevel
        {
            get; set;
        }

        public string OfferImageURL
        {
            get; set;
        }
    }
}
