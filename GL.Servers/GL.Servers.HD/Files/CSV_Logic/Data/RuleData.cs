namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class RuleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RuleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RuleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Function
        {
            get; set;
        }

        public string Parameters
        {
            get; set;
        }

        public int HourParameters
        {
            get; set;
        }
    }
}
