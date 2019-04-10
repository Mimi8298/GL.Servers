namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

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

        public int Order
        {
            get; set;
        }
    }
}
