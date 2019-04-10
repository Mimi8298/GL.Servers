namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class TeamGoalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TeamGoalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TeamGoalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Type
        {
            get; set;
        }

        public int Level
        {
            get; set;
        }

        public int SimilarityValue
        {
            get; set;
        }

        public bool CanBeInNewTeam
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
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

        public int DiamondReward
        {
            get; set;
        }

        public string CompletedTID
        {
            get; set;
        }

        public string Hero
        {
            get; set;
        }

        public string NPC
        {
            get; set;
        }

        public string Obstacle
        {
            get; set;
        }

        public int SortIndex
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }
    }
}
