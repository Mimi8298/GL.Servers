namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class PredefinedDeckData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PredefinedDeckData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PredefinedDeckData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // PredefinedDeckData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Spells
        {
            get; set;
        }

        internal int SpellLevel
        {
            get; set;
        }

        internal string RandomSpellSets
        {
            get; set;
        }

        internal string Description
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

    }
}