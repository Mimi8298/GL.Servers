namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class AllianceBadgeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceBadgeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceBadgeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // AllianceBadgeData.
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

        internal string Category
        {
            get; set;
        }

    }
}