namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CatalogueLayoutData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CatalogueLayoutData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CatalogueLayoutData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string PageA1
        {
            get; set;
        }

        public string PageB1
        {
            get; set;
        }

        public string PageA2
        {
            get; set;
        }

        public string PageB2
        {
            get; set;
        }

        public string PageA3
        {
            get; set;
        }

        public string PageB3
        {
            get; set;
        }

        public string PageA4
        {
            get; set;
        }

        public string PageB4
        {
            get; set;
        }

        public string PageM1
        {
            get; set;
        }

        public string PageM2
        {
            get; set;
        }

        public string PageM3
        {
            get; set;
        }

        public string PageM4
        {
            get; set;
        }

        public string PageM5
        {
            get; set;
        }

        public string PageM6
        {
            get; set;
        }
    }
}
