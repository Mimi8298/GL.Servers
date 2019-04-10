namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class HelpshiftData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HelpshiftData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HelpshiftData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // HelpshiftData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string HelpshiftId
        {
            get; set;
        }

    }
}