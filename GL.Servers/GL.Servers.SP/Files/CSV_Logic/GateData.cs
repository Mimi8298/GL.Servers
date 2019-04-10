namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class GateData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GateData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GateData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int GateTimeMin
        {
            get; set;
        }

        public int GateStars
        {
            get; set;
        }

        public int GateCost
        {
            get; set;
        }
    }
}
