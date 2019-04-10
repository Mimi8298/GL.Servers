namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class DeepLinkData : Data
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeepLinkData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DeepLinkData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public bool Public
        {
            get; set;
        }
    }
}
