namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ExpLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExpLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExpLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ExpLevelData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int ExpToNextLevel
        {
            get; set;
        }

        internal int SummonerLevel
        {
            get; set;
        }

        internal int TowerLevel
        {
            get; set;
        }

        internal int TroopLevel
        {
            get; set;
        }

        internal int Decks
        {
            get; set;
        }

        internal int SummonerKillGold
        {
            get; set;
        }

        internal int TowerKillGold
        {
            get; set;
        }

        internal int DiamondReward
        {
            get; set;
        }

        internal int QuestSlots
        {
            get; set;
        }

    }
}