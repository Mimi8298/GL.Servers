namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ContentTestData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ContentTestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ContentTestData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ContentTestData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string SourceData
        {
            get; set;
        }

        internal string TargetData
        {
            get; set;
        }

        internal string Stat1
        {
            get; set;
        }

        internal string Operator
        {
            get; set;
        }

        internal string Stat2
        {
            get; set;
        }

        internal int Result
        {
            get; set;
        }

        internal bool Enabled
        {
            get; set;
        }

    }
}