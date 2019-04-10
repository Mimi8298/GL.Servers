namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class LumberjackData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LumberjackData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LumberjackData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public int DeadTreeLimit
        {
            get; set;
        }

        public int DelayHours
        {
            get; set;
        }

        public int DurationMinutes
        {
            get; set;
        }

        public int CooldownHours
        {
            get; set;
        }

        public int DiscountPercent
        {
            get; set;
        }

        public int TreeCutDelayMillis
        {
            get; set;
        }

        public int MaxTreeLimit
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string OfferImageURL
        {
            get; set;
        }
    }
}
