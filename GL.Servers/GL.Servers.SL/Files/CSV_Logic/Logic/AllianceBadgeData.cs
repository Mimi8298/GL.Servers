namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class AllianceBadgeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceBadgeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceBadgeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }
    }
}
