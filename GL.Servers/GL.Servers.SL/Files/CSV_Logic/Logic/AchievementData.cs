namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

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

        public int ExpReward
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

        public bool ShowOnlyInBattle
        {
            get; set;
        }

        public string Hero
        {
            get; set;
        }

        public bool ShowOnlyInHomescreen
        {
            get; set;
        }

        public int SortIndex
        {
            get; set;
        }

        public bool Hidden
        {
            get; set;
        }
    }
}
