namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FishConfigData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FishConfigData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FishConfigData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int IntValue
        {
            get; set;
        }

        public string StringValue
        {
            get; set;
        }

        public bool BooleanValue
        {
            get; set;
        }
    }
}
