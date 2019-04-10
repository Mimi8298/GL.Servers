namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MineGoodData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MineGoodData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MineGoodData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ToolIconExportName
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int DumbValue
        {
            get; set;
        }

        public int Probability
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public string ProcessingBuilding
        {
            get; set;
        }
    }
}
