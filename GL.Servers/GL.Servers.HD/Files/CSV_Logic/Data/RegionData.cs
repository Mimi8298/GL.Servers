namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class RegionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RegionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RegionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
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

        public bool HelpshiftHardDeflection
        {
            get; set;
        }
    }
}
