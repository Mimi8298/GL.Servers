namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class DeeplinkData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DeeplinkData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DeeplinkData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // DeeplinkData.
        }

        public string ParameterType
        {
            get; set;
        }

        public string ParameterName
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public bool Public
        {
            get; set;
        }
    }
}
