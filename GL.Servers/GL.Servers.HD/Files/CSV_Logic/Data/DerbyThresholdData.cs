namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DerbyThresholdData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DerbyThresholdData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DerbyThresholdData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string League
        {
            get; set;
        }

        public string MemberCount
        {
            get; set;
        }

        public int Threshold1
        {
            get; set;
        }

        public int Threshold2
        {
            get; set;
        }

        public int Threshold3
        {
            get; set;
        }

        public int Threshold4
        {
            get; set;
        }

        public int Threshold5
        {
            get; set;
        }

        public int Threshold6
        {
            get; set;
        }

        public int Threshold7
        {
            get; set;
        }

        public int Threshold8
        {
            get; set;
        }

        public int Threshold9
        {
            get; set;
        }
    }
}
