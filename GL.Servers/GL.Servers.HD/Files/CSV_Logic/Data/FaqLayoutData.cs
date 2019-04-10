namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FaqLayoutData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FaqLayoutData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FaqLayoutData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Language
        {
            get; set;
        }

        public int NumberButtons
        {
            get; set;
        }

        public string ButtonTID
        {
            get; set;
        }

        public string ButtonLink
        {
            get; set;
        }
    }
}
