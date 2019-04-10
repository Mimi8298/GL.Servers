namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CollectionToolData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CollectionToolData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CollectionToolData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ToolIconExportName
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int DumbValue
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public int ReputationUnlockLevel
        {
            get; set;
        }
    }
}
