namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ResourcePackData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourcePackData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourcePackData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ResourcePackData.
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

        internal string Resource
        {
            get; set;
        }

        internal int Amount
        {
            get; set;
        }

        internal string IconFile
        {
            get; set;
        }

    }
}