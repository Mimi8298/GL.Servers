namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FontData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FontData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FontData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public int MarginTop
        {
            get; set;
        }

        public int MarginRight
        {
            get; set;
        }

        public int MarginBottom
        {
            get; set;
        }

        public int MarginLeft
        {
            get; set;
        }

        public int MarginDefaultFontSize
        {
            get; set;
        }
    }
}
