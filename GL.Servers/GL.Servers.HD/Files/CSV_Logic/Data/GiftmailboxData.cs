namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GiftmailboxData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GiftmailboxData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GiftmailboxData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int LetterCapacity
        {
            get; set;
        }

        public int GiftCardCapacity
        {
            get; set;
        }
    }
}
