namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CashPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CashPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CashPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int DiamondCost
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
    }
}
