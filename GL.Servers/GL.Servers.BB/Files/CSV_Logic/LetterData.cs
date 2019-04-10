namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class LetterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LetterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LetterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string Signature
        {
            get; set;
        }

        public string Chain
        {
            get; set;
        }

        public string Location
        {
            get; set;
        }

        public int Weight
        {
            get; set;
        }

        public string Style
        {
            get; set;
        }

        public string Decoration
        {
            get; set;
        }
    }
}
