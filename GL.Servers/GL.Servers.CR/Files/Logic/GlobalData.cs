namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class GlobalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GlobalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // GlobalData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int NumberValue
        {
            get; set;
        }

        internal bool BooleanValue
        {
            get; set;
        }

        internal string TextValue
        {
            get; set;
        }

        internal string[] StringArray
        {
            get; set;
        }

        internal int[] NumberArray
        {
            get; set;
        }

    }
}