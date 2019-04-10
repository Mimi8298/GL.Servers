namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class AllianceBadgeLayerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceBadgeLayerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceBadgeLayerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string Type
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

        public int RequiredClanLevel
        {
            get; set;
        }
    }
}
