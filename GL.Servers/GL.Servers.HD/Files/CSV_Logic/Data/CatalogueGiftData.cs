namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CatalogueGiftData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CatalogueGiftData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CatalogueGiftData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Gifts
        {
            get; set;
        }

        public bool TimeLimited
        {
            get; set;
        }

        public int IntervalHours
        {
            get; set;
        }

        public int CooldownExpiredHours
        {
            get; set;
        }

        public int CooldownPurchasedHours
        {
            get; set;
        }

        public int SpeedUpDiamondPrice
        {
            get; set;
        }
    }
}
