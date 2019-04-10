namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GathererMineResourceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GathererMineResourceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GathererMineResourceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
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

        public string DescriptionTID
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }
    }
}
