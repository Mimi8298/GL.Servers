namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DerbyRewardData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DerbyRewardData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DerbyRewardData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Rewards
        {
            get; set;
        }

        public int RewardAmounts
        {
            get; set;
        }

        public int Probabilities
        {
            get; set;
        }
    }
}
