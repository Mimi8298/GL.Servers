namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ShopCycleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ShopCycleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ShopCycleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ShopCycleData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int A
        {
            get; set;
        }

        internal int B
        {
            get; set;
        }

        internal int C
        {
            get; set;
        }

        internal string D
        {
            get; set;
        }

    }
}