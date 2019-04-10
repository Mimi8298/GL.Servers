namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class GameModeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GameModeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GameModeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // GameModeData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string TID
        {
            get; set;
        }

        internal string RequestTID
        {
            get; set;
        }

        internal string InProgressTID
        {
            get; set;
        }

        internal string CardLevelAdjustment
        {
            get; set;
        }

        internal int PlayerCount
        {
            get; set;
        }

        internal string DeckSelection
        {
            get; set;
        }

        internal int OvertimeSeconds
        {
            get; set;
        }

        internal string PredefinedDecks
        {
            get; set;
        }

        internal bool SameDeckOnBoth
        {
            get; set;
        }

        internal bool SeparateTeamDecks
        {
            get; set;
        }

        internal int ElixirProductionMultiplier
        {
            get; set;
        }

        internal int ElixirProductionOvertimeMultiplier
        {
            get; set;
        }

        internal bool UseStartingElixir
        {
            get; set;
        }

        internal int StartingElixir
        {
            get; set;
        }

        internal bool Heroes
        {
            get; set;
        }

        internal string ForcedDeckCards
        {
            get; set;
        }

        internal string Players
        {
            get; set;
        }

        internal string EventDeckSetLimit
        {
            get; set;
        }

        internal bool ForcedDeckCardsUsingCardTheme
        {
            get; set;
        }

        internal string PrincessSkin
        {
            get; set;
        }

        internal string KingSkin
        {
            get; set;
        }

        internal bool GivesClanScore
        {
            get; set;
        }

        internal int GoldPerTower1
        {
            get; set;
        }

        internal int GoldPerTower2
        {
            get; set;
        }

        internal int GoldPerTower3
        {
            get; set;
        }

        internal int GemsPerTower1
        {
            get; set;
        }

        internal int GemsPerTower2
        {
            get; set;
        }

        internal int GemsPerTower3
        {
            get; set;
        }

        internal string EndConfetti1
        {
            get; set;
        }

        internal string EndConfetti2
        {
            get; set;
        }

        internal int TargetTouchdowns
        {
            get; set;
        }

        internal string SkinSet
        {
            get; set;
        }

        internal bool FixedDeckOrder
        {
            get; set;
        }

        internal string Icon
        {
            get; set;
        }

    }
}