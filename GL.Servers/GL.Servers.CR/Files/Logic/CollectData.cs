namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class CollectData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CollectData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CollectData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // CollectData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int Size
        {
            get; set;
        }

        internal string Title
        {
            get; set;
        }

        internal string Info
        {
            get; set;
        }

        internal string ItemFile
        {
            get; set;
        }

        internal string ItemExportName
        {
            get; set;
        }

        internal int Count
        {
            get; set;
        }

        internal string DataType
        {
            get; set;
        }

        internal string Resource
        {
            get; set;
        }

        internal string Rarity
        {
            get; set;
        }

        internal string BaseChest
        {
            get; set;
        }

        internal int Weight
        {
            get; set;
        }

        internal string MinArena
        {
            get; set;
        }

        internal string MaxArena
        {
            get; set;
        }

    }
}