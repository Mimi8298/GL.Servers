namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class BackgroundDecoData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BackgroundDecoData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BackgroundDecoData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // BackgroundDecoData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string Layer
        {
            get; set;
        }
    }
}