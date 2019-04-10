namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class ResourcePackData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourcePackData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourcePackData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string TID
        {
            get; set;
        }

        public string Resource
        {
            get; set;
        }

        public int CapacityPercentage
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }
    }
}
