namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class EasterEggData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EasterEggData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EasterEggData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Probability
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int OpenDurationMillis
        {
            get; set;
        }

        public string TapSound
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

        public string GoodReward
        {
            get; set;
        }

        public int GoodAmount
        {
            get; set;
        }

        public string Area
        {
            get; set;
        }

        public string HomeOrVisiting
        {
            get; set;
        }

        public string Theme
        {
            get; set;
        }

        public string CalendarEvent
        {
            get; set;
        }

        public int DayMod
        {
            get; set;
        }

        public int DayGroup
        {
            get; set;
        }

        public int HourMod
        {
            get; set;
        }

        public int HourGroup
        {
            get; set;
        }
    }
}
