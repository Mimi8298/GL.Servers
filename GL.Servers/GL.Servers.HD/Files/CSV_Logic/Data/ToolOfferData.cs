namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ToolOfferData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ToolOfferData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ToolOfferData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Tool
        {
            get; set;
        }

        public int Amount
        {
            get; set;
        }

        public int NormalPrice
        {
            get; set;
        }

        public int DiscountPrice
        {
            get; set;
        }

        public int DiscountPercent
        {
            get; set;
        }

        public int LevelEnabled
        {
            get; set;
        }

        public int LevelDisabled
        {
            get; set;
        }

        public int DurationMinutes
        {
            get; set;
        }

        public bool Spender
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }
    }
}
