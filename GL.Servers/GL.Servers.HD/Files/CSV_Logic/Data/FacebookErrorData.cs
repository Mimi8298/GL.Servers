namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FacebookErrorData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FacebookErrorData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FacebookErrorData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Category
        {
            get; set;
        }

        public int CategoryID
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public string OS
        {
            get; set;
        }
    }
}
