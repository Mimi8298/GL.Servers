namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MoneyData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MoneyData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MoneyData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ToolIconExportName
        {
            get; set;
        }

        public string BonusIconExportName
        {
            get; set;
        }

        public string BonusTID
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

        public int DiamondPrice
        {
            get; set;
        }
    }
}
