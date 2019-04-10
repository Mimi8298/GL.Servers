namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NewspaperStorieData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NewspaperStorieData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NewspaperStorieData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Type
        {
            get; set;
        }

        public string FallbackStory
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string Text1
        {
            get; set;
        }

        public string Text2
        {
            get; set;
        }

        public string URL
        {
            get; set;
        }
    }
}
