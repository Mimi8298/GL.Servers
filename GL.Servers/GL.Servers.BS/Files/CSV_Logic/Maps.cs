namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Maps : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Maps"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Maps(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            CSV_Helpers.Data.Load(this, this.GetType(), Row);
        }

        public string CodeName
        {
            get; set;
        }

        public string Group
        {
            get; set;
        }

        public string Data
        {
            get; set;
        }
    }
}
