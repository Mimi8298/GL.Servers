namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class AlliancePortalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AlliancePortalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AlliancePortalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // AlliancePortalData.
        }

        public string TID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }
    }
}
