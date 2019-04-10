namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DerbyLeagueData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DerbyLeagueData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DerbyLeagueData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public int MaxAvailableTasks
        {
            get; set;
        }

        public int MaxDerbyTasksPerPlayer
        {
            get; set;
        }

        public bool MatchmakingEnabled
        {
            get; set;
        }

        public bool DemoteEnabled
        {
            get; set;
        }

        public bool PromoteEnabled
        {
            get; set;
        }

        public string DerbyReward1
        {
            get; set;
        }

        public int DerbyReward1Levels
        {
            get; set;
        }

        public string DerbyReward2
        {
            get; set;
        }

        public int DerbyReward2Levels
        {
            get; set;
        }

        public string DerbyReward3
        {
            get; set;
        }

        public int DerbyReward3Levels
        {
            get; set;
        }

        public int ThresholdCount
        {
            get; set;
        }

        public string ThresholdReward1
        {
            get; set;
        }

        public int ThresholdReward1Levels
        {
            get; set;
        }

        public string ThresholdReward2
        {
            get; set;
        }

        public int ThresholdReward2Levels
        {
            get; set;
        }

        public string ThresholdReward3
        {
            get; set;
        }

        public int ThresholdReward3Levels
        {
            get; set;
        }

        public string ThresholdReward4
        {
            get; set;
        }

        public int ThresholdReward4Levels
        {
            get; set;
        }

        public string ThresholdReward5
        {
            get; set;
        }

        public int ThresholdReward5Levels
        {
            get; set;
        }

        public string ThresholdReward6
        {
            get; set;
        }

        public int ThresholdReward6Levels
        {
            get; set;
        }

        public string ThresholdReward7
        {
            get; set;
        }

        public int ThresholdReward7Levels
        {
            get; set;
        }

        public string ThresholdReward8
        {
            get; set;
        }

        public int ThresholdReward8Levels
        {
            get; set;
        }

        public string ThresholdReward9
        {
            get; set;
        }

        public int ThresholdReward9Levels
        {
            get; set;
        }

        public string LeagueInfoTaskPaper
        {
            get; set;
        }

        public string LeagueFlag
        {
            get; set;
        }

        public string HorseShoeIcon
        {
            get; set;
        }

        public string ThresholdRewardColumn
        {
            get; set;
        }

        public string RaceOverFlag
        {
            get; set;
        }

        public string LeagueCurtain
        {
            get; set;
        }

        public string HorseCape
        {
            get; set;
        }

        public string HorseLaurel
        {
            get; set;
        }
    }
}
