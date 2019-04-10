namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class RegionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RegionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RegionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // RegionData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string TID
        {
            get; set;
        }

        internal string DisplayName
        {
            get; set;
        }

        internal bool IsCountry
        {
            get; set;
        }

        internal bool RegionPopup
        {
            get; set;
        }

    }
}