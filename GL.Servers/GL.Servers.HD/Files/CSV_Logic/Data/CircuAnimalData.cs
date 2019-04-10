namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CircuAnimalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CircuAnimalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CircuAnimalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string StateIdleExportName
        {
            get; set;
        }

        public int StateIdleMinMS
        {
            get; set;
        }

        public int StateIdleMaxMS
        {
            get; set;
        }

        public string Feed
        {
            get; set;
        }

        public int TimeMin
        {
            get; set;
        }

        public int TimeSec
        {
            get; set;
        }

        public int HungerValue
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string FeedSound
        {
            get; set;
        }
    }
}
