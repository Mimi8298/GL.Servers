namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ResourceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string CollectEffect
        {
            get; set;
        }

        public string ResourceIconExportName
        {
            get; set;
        }

        public string StealEffect
        {
            get; set;
        }

        public int StealLimitMid
        {
            get; set;
        }

        public string StealEffectMid
        {
            get; set;
        }

        public int StealLimitBig
        {
            get; set;
        }

        public string StealEffectBig
        {
            get; set;
        }

        public bool PremiumCurrency
        {
            get; set;
        }

        public string HudInstanceName
        {
            get; set;
        }

        public string CapFullTID
        {
            get; set;
        }

        public int TextRed
        {
            get; set;
        }

        public int TextGreen
        {
            get; set;
        }

        public int TextBlue
        {
            get; set;
        }

        public string WarRefResource
        {
            get; set;
        }

        public string BundleIconExportName
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }
    }
}
