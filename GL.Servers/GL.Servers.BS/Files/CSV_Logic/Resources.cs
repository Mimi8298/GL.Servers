namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Resources : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Resources(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string IconSWF
        {
            get; set;
        }

        public string CollectEffect
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string Rarity
        {
            get; set;
        }

        public bool PremiumCurrency
        {
            get; set;
        }

        public int TextRed
        {
            get; set;
        }

        public int TextGreen
        {
            get; set;
        }

        public int TextBlue
        {
            get; set;
        }

        public int Cap
        {
            get; set;
        }
    }
}
