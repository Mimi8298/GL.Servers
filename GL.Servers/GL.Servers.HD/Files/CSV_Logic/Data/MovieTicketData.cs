namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MovieTicketData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MovieTicketData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MovieTicketData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int RequiredLevel
        {
            get; set;
        }

        public bool OnlyForNonPayingUsers
        {
            get; set;
        }

        public bool SkipVideo
        {
            get; set;
        }

        public bool SkipVideoSpenders
        {
            get; set;
        }

        public int FirstRewardIsDiamondProbability
        {
            get; set;
        }

        public int FirstRewardIsDiamondProbabilitySpenders
        {
            get; set;
        }

        public int DaysWithoutSpendingLimit
        {
            get; set;
        }

        public int FinishedDiamondReward
        {
            get; set;
        }

        public int CycleDurationMinutes
        {
            get; set;
        }

        public int MoviesPerCycle
        {
            get; set;
        }

        public int MoviesPerCycleSpenders
        {
            get; set;
        }

        public bool OnlyInHighMemoryDevices
        {
            get; set;
        }

        public bool EnableInAndroid
        {
            get; set;
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

        public string RewardsSpenders
        {
            get; set;
        }

        public int RewardAmountsSpenders
        {
            get; set;
        }

        public int ProbabilitiesSpenders
        {
            get; set;
        }
    }
}
