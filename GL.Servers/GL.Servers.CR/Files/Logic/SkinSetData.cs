namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class SkinSetData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SkinSetData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SkinSetData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SkinSetData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Character
        {
            get; set;
        }

        internal string Skin
        {
            get; set;
        }

    }
}