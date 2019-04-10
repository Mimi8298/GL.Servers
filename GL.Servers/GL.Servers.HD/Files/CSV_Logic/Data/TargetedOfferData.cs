namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TargetedOfferData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TargetedOfferData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TargetedOfferData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public bool Active
        {
            get; set;
        }

        public string Rules
        {
            get; set;
        }

        public string Offer
        {
            get; set;
        }
    }
}
