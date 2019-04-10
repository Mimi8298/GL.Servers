namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class AbTestData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AbTestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AbTestData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string CountryCodes
        {
            get; set;
        }

        public bool StoreApple
        {
            get; set;
        }

        public bool StoreGooglePlay
        {
            get; set;
        }

        public int Modulo
        {
            get; set;
        }

        public int RemainderMax
        {
            get; set;
        }
    }
}
