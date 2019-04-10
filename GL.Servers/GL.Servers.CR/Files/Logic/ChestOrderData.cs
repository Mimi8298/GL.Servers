namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ChestOrderData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ChestOrderData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ChestOrderData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ChestOrderData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Chest
        {
            get; set;
        }

        internal int QuestThreshold
        {
            get; set;
        }

        internal string ArenaThreshold
        {
            get; set;
        }

        internal bool OneTime
        {
            get; set;
        }

    }
}