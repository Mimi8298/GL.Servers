namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class NewsData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NewsData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NewsData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // NewData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int ID
        {
            get; set;
        }

        internal bool Enabled
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string InfoTID
        {
            get; set;
        }

        internal string ItemSWF
        {
            get; set;
        }

        internal string ItemExportName
        {
            get; set;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal string ImageSWF
        {
            get; set;
        }

        internal string ImageExportName
        {
            get; set;
        }

        internal string ButtonUrl
        {
            get; set;
        }

        internal string ButtonTID
        {
            get; set;
        }

    }
}