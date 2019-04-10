namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TrackingEventData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TrackingEventData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TrackingEventData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Label
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string Product
        {
            get; set;
        }

        public string ProductDetail
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public bool IsEvent
        {
            get; set;
        }
    }
}
