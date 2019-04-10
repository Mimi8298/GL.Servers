namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AiRoadsideShopData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AiRoadsideShopData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AiRoadsideShopData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Good
        {
            get; set;
        }

        public int Amount
        {
            get; set;
        }

        public int Probability
        {
            get; set;
        }
    }
}
