namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class Leagues2Data : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Leagues2Data"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Leagues2Data(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int TrophyLimitLow
        {
            get; set;
        }

        public int TrophyLimitHigh
        {
            get; set;
        }

        public int GoldReward
        {
            get; set;
        }

        public int ElixirReward
        {
            get; set;
        }

        public int BonusGold
        {
            get; set;
        }

        public int BonusElixir
        {
            get; set;
        }

        public int SeasonTrophyReset
        {
            get; set;
        }

        public int MaxDiamondCost
        {
            get; set;
        }
    }
}
