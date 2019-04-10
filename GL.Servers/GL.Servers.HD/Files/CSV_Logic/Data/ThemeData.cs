namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ThemeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ThemeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ThemeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string StartDate
        {
            get; set;
        }

        public int StartHour
        {
            get; set;
        }

        public string EndDate
        {
            get; set;
        }

        public int EndHour
        {
            get; set;
        }

        public int LevelLimit
        {
            get; set;
        }

        public string ExportNamePostfix
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }
    }
}
