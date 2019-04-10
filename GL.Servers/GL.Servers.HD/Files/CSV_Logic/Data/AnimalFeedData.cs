namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AnimalFeedData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AnimalFeedData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AnimalFeedData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ToolIconExportName
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

        public int TimeSec
        {
            get; set;
        }

        public int ExpCollect
        {
            get; set;
        }

        public int Units
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string ProcessingBuilding
        {
            get; set;
        }

        public int DumbValue
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

        public string SpecialUnlockRequirement
        {
            get; set;
        }
    }
}
