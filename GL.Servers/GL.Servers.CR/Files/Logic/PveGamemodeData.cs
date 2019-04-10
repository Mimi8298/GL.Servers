namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class PveGamemodeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PveGamemodeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PveGamemodeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // PveGamemodeData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string VictoryCondition
        {
            get; set;
        }

        internal string ForcedCards
        {
            get; set;
        }

        internal string Location
        {
            get; set;
        }

        internal string ComputerPlayerType
        {
            get; set;
        }

        internal string TowerRules
        {
            get; set;
        }

        internal string WaveSpell
        {
            get; set;
        }

        internal int WaveSpellLevelIndex
        {
            get; set;
        }

        internal int WaveDelay
        {
            get; set;
        }

        internal int WaveX
        {
            get; set;
        }

        internal int WaveY
        {
            get; set;
        }

        internal bool WaveRepeat
        {
            get; set;
        }

        internal int WaveRepeatTime
        {
            get; set;
        }

    }
}