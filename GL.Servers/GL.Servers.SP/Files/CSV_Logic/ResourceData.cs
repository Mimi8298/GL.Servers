namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class ResourceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string InfoTID
        {
            get; set;
        }

        public int GemCostPerThousand
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public bool Material
        {
            get; set;
        }
    }
}
