namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class LiberatedIncomeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LiberatedIncomeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LiberatedIncomeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int IncomePerHour
        {
            get; set;
        }

        public int BoatCapacity
        {
            get; set;
        }

        public int ProtectedVillages
        {
            get; set;
        }
    }
}
