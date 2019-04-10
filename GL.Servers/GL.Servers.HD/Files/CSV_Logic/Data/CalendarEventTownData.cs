namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CalendarEventTownData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CalendarEventTownData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CalendarEventTownData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string StartDate
        {
            get; set;
        }

        public int StartHour
        {
            get; set;
        }

        public string EndDate
        {
            get; set;
        }

        public int EndHour
        {
            get; set;
        }

        public string EventType
        {
            get; set;
        }

        public string EventIcon
        {
            get; set;
        }

        public string GoalType
        {
            get; set;
        }

        public int GoalThresholds
        {
            get; set;
        }

        public string ThresholdRewardSets
        {
            get; set;
        }

        public string LeaderboardReward1
        {
            get; set;
        }

        public int LeaderboardReward1Amount
        {
            get; set;
        }

        public string LeaderboardReward2
        {
            get; set;
        }

        public int LeaderboardReward2Amount
        {
            get; set;
        }

        public string LeaderboardReward3
        {
            get; set;
        }

        public int LeaderboardReward3Amount
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }
    }
}
