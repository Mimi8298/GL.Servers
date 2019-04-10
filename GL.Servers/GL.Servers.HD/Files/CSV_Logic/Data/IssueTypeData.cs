namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class IssueTypeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="IssueTypeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public IssueTypeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string IssueType
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DetailsTID
        {
            get; set;
        }

        public string Language
        {
            get; set;
        }
    }
}
