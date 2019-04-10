namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class CountryData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CountryData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CountryData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string DisplayName
        {
            get; set;
        }

        public bool HelpshiftHardDeflect
        {
            get; set;
        }
    }
}
