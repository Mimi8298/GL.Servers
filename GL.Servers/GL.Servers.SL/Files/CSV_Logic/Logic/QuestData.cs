namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class QuestData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="QuestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public QuestData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string IconUiExportName
        {
            get; set;
        }

        public string World
        {
            get; set;
        }

        public int RequiredXpLevel
        {
            get; set;
        }

        public string RequiredQuest
        {
            get; set;
        }

        public string QuestType
        {
            get; set;
        }

        public string LevelFile
        {
            get; set;
        }

        public int GoalMoveCount
        {
            get; set;
        }

        public int MinEnemies
        {
            get; set;
        }

        public int MinObstacles
        {
            get; set;
        }

        public int Energy
        {
            get; set;
        }

        public int GoldRewardOverride
        {
            get; set;
        }

        public int XpRewardOverride
        {
            get; set;
        }

        public string AIBehaviour
        {
            get; set;
        }

        public int AIInaccuracy
        {
            get; set;
        }

        public bool Hidden
        {
            get; set;
        }

        public int MinGoldDrop
        {
            get; set;
        }

        public int MaxGoldDrop
        {
            get; set;
        }

        public string League
        {
            get; set;
        }

        public int SortOrder
        {
            get; set;
        }
    }
}
