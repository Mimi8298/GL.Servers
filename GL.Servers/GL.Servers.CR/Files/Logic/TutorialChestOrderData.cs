namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class TutorialChestOrderData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TutorialChestOrderData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TutorialChestOrderData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // TutorialChestOrderData.
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

        internal string NPC
        {
            get; set;
        }

        internal string PvE_Tutorial
        {
            get; set;
        }

    }
}