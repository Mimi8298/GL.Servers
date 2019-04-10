namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class HintData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HintData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HintData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
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
    }
}
