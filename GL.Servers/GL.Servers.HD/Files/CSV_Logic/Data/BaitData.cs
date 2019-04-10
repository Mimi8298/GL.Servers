namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class BaitData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BaitData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BaitData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string DescriptionTID
        {
            get; set;
        }

        public string Colour
        {
            get; set;
        }

        public int MainColourMultiplier
        {
            get; set;
        }

        public int SubColourMultiplier
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public int RequirementAmount
        {
            get; set;
        }

        public string Requirement
        {
            get; set;
        }

        public int TimeMin
        {
            get; set;
        }

        public string ProcessingBuilding
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public int InitialAmount
        {
            get; set;
        }
    }
}
