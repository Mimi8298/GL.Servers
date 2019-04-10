namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ExpansionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExpansionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExpansionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string CollectionTool
        {
            get; set;
        }

        public int CollectionToolAmount
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string LimitedCollectionTool
        {
            get; set;
        }

        public int LimitedCollectionToolAmount
        {
            get; set;
        }
    }
}
