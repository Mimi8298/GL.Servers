namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class HintData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HintData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HintData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string TID
        {
            get; set;
        }

        public int TownHallLevelMin
        {
            get; set;
        }

        public int TownHallLevelMax
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }

        public string iOSTID
        {
            get; set;
        }

        public string AndroidTID
        {
            get; set;
        }
    }
}
