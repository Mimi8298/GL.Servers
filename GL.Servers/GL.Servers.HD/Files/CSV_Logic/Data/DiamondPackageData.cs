namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DiamondPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DiamondPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DiamondPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public int CurrencyInUSD
        {
            get; set;
        }

        public int Diamonds
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public bool TLOPackage
        {
            get; set;
        }

        public bool RED
        {
            get; set;
        }

        public bool ChronosPackage
        {
            get; set;
        }

        public bool SpecialPackage
        {
            get; set;
        }

        public string DecoReward
        {
            get; set;
        }

        public int DecoAmount
        {
            get; set;
        }

        public bool BigButton
        {
            get; set;
        }

        public int BoosterRandomAmount
        {
            get; set;
        }

        public int BoosterRandomMinStars
        {
            get; set;
        }

        public int Booster5StarAmount
        {
            get; set;
        }

        public bool StarterPackage
        {
            get; set;
        }

        public string StarterPackageReward
        {
            get; set;
        }
    }
}
