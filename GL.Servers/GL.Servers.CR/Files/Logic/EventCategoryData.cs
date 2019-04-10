namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EventCategoryData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventCategoryData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventCategoryData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EventCategoryData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string CSVFiles
        {
            get; set;
        }

        internal string CSVRows
        {
            get; set;
        }

        internal string CustomNames
        {
            get; set;
        }

    }
}