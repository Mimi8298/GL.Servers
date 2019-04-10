namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class SurvivalModeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SurvivalModeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SurvivalModeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SurvivalModeData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string IconSWF
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal string GameMode
        {
            get; set;
        }

        internal string WinsIconExportName
        {
            get; set;
        }

        internal bool Enabled
        {
            get; set;
        }

        internal bool EventOnly
        {
            get; set;
        }

        internal int JoinCost
        {
            get; set;
        }

        internal string JoinCostResource
        {
            get; set;
        }

        internal int FreePass
        {
            get; set;
        }

        internal int MaxWins
        {
            get; set;
        }

        internal int MaxLoss
        {
            get; set;
        }

        internal int RewardCards
        {
            get; set;
        }

        internal int RewardGold
        {
            get; set;
        }

        internal int RewardSpellCount
        {
            get; set;
        }

        internal string RewardSpell
        {
            get; set;
        }

        internal int RewardSpellMaxCount
        {
            get; set;
        }

        internal string ItemExportName
        {
            get; set;
        }

        internal string ConfirmExportName
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string CardTheme
        {
            get; set;
        }

    }
}