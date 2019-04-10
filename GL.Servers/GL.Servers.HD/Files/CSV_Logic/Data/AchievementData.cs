namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AchievementData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AchievementData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AchievementData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Level
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

        public string Action
        {
            get; set;
        }

        public int ActionCount
        {
            get; set;
        }

        public string ActionData
        {
            get; set;
        }

        public int MinutesLimit
        {
            get; set;
        }

        public int Exp
        {
            get; set;
        }

        public int DiamondReward
        {
            get; set;
        }

        public string DecoReward
        {
            get; set;
        }

        public string CustomReward
        {
            get; set;
        }

        public string CustomRewardTooltipTID
        {
            get; set;
        }

        public int Points
        {
            get; set;
        }

        public int FbPoints
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public int Priority
        {
            get; set;
        }

        public string FbObjectImageName
        {
            get; set;
        }

        public string AndroidID
        {
            get; set;
        }
    }
}
