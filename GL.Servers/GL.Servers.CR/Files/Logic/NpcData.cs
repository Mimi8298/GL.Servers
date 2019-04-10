namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class NpcData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NpcData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NpcData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // NpcData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Location
        {
            get; set;
        }

        internal string PredefinedDeck
        {
            get; set;
        }

        internal int Trophies
        {
            get; set;
        }

        internal int ManaRegenMs
        {
            get; set;
        }

        internal int ManaRegenMsEnd
        {
            get; set;
        }

        internal int ManaRegenMsOvertime
        {
            get; set;
        }

        internal int ExpLevel
        {
            get; set;
        }

        internal bool CanReplay
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal int ExpReward
        {
            get; set;
        }

        internal int Seed
        {
            get; set;
        }

        internal bool FullDeckNotNeeded
        {
            get; set;
        }

        internal int ManaReserve
        {
            get; set;
        }

        internal int StartingMana
        {
            get; set;
        }

        internal int WizardHpMultiplier
        {
            get; set;
        }

        internal string StartTaunt
        {
            get; set;
        }

        internal string OwnTowerDestroyedTaunt
        {
            get; set;
        }

        internal bool HighlightTargetsOnManaFull
        {
            get; set;
        }

        internal bool TrainingMatchAllowed
        {
            get; set;
        }

    }
}