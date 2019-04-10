namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Regions : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Regions"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Regions(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DisplayName
        {
            get; set;
        }

        public bool IsCountry
        {
            get; set;
        }
    }
}
