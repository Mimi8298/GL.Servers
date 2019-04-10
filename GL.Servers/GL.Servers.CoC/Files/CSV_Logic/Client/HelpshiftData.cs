namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class HelpshiftData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HelpshiftData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HelpshiftData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string HelpshiftId
        {
            get; set;
        }
    }
}
