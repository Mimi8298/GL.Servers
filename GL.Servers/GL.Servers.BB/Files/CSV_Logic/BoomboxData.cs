namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class BoomboxData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BoomboxData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BoomboxData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SupportedPlatforms
        {
            get; set;
        }

        public string AllowedDomains
        {
            get; set;
        }

        public string AllowedUrls
        {
            get; set;
        }
    }
}
