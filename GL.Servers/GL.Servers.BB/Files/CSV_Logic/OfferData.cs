namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class OfferData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="OfferData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public OfferData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int OfferSlot
        {
            get; set;
        }

        public int HqLevel
        {
            get; set;
        }

        public int PurchaseLimit
        {
            get; set;
        }

        public string CostResource
        {
            get; set;
        }

        public int CostAmount
        {
            get; set;
        }

        public string OfferResource
        {
            get; set;
        }

        public int OfferAmount
        {
            get; set;
        }

        public string BannerTID
        {
            get; set;
        }

        public string BannerValue
        {
            get; set;
        }
    }
}
