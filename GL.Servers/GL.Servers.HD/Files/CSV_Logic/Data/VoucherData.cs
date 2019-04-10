namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class VoucherData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="VoucherData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public VoucherData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public bool Deprecated
        {
            get; set;
        }

        public int VoucherType
        {
            get; set;
        }

        public string ToolIconExportName
        {
            get; set;
        }

        public string ToolBigIconExportName
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

        public int Probability
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }
    }
}
