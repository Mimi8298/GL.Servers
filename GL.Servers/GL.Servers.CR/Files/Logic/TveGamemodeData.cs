namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class TveGamemodeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TveGamemodeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TveGamemodeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // TveGamemodeData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string PrimarySpells
        {
            get; set;
        }

        internal string SecondarySpells
        {
            get; set;
        }

        internal string CastSpells
        {
            get; set;
        }

        internal bool RandomWaves
        {
            get; set;
        }

        internal int ElixirPerWave
        {
            get; set;
        }

        internal int WaveCount
        {
            get; set;
        }

        internal int TimePerWave
        {
            get; set;
        }

        internal int TimeToFirstWave
        {
            get; set;
        }

        internal string ForcedCards1
        {
            get; set;
        }

        internal string ForcedCards2
        {
            get; set;
        }

        internal bool RotateDecks
        {
            get; set;
        }

    }
}