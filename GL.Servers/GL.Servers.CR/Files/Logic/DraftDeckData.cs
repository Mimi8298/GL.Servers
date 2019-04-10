namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class DraftDeckData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DraftDeckData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DraftDeckData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // DraftDeckData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string RequiredSets
        {
            get; set;
        }

        internal string OptionalSets
        {
            get; set;
        }

    }
}