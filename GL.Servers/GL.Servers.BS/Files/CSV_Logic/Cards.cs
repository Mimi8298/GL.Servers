namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Cards : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Cards"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Cards(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string Target
        {
            get; set;
        }

        public string RequiresCard
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string Skill
        {
            get; set;
        }

        public int Value
        {
            get; set;
        }

        public int Value2
        {
            get; set;
        }

        public int NumCards
        {
            get; set;
        }

        public string Rarity
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string PowerNumberTID
        {
            get; set;
        }

        public string PowerNumber2TID
        {
            get; set;
        }

        public string PowerIcon1ExportName
        {
            get; set;
        }

        public string PowerIcon2ExportName
        {
            get; set;
        }
    }
}
