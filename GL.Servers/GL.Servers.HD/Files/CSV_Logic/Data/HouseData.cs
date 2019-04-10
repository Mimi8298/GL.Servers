namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class HouseData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HouseData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HouseData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int RequiredAchievementPoints
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

        public int BrokenPartCount
        {
            get; set;
        }

        public string BrokenExportName
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }
    }
}
