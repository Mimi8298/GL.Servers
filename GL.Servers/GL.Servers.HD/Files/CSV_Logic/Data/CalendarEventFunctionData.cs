namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CalendarEventFunctionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CalendarEventFunctionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CalendarEventFunctionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
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

        public int MinValue
        {
            get; set;
        }

        public int MaxValue
        {
            get; set;
        }

        public bool TargetingSupported
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }
    }
}
