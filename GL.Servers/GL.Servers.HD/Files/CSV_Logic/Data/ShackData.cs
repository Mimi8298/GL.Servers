namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ShackData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ShackData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ShackData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public string FileName
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

        public bool Deprecated
        {
            get; set;
        }
    }
}
