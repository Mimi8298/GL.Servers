namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class CalendarEventData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CalendarEventData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CalendarEventData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string EventPhoto
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

        public bool ThresholdRewardAlwaysShow
        {
            get; set;
        }

        public int GlobalGoal
        {
            get; set;
        }

        public string GlobalReward
        {
            get; set;
        }

        public int GlobalRewardAmount
        {
            get; set;
        }

        public int GlobalRewardThresholdIndex
        {
            get; set;
        }

        public string EventFish
        {
            get; set;
        }

        public string EventParam1
        {
            get; set;
        }

        public string EventParam2
        {
            get; set;
        }

        public string EventParam3
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string IntroTitle
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }

        public string Notification
        {
            get; set;
        }

        public string GlobalRewardTitle
        {
            get; set;
        }

        public string GlobalRewardText
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

        public string AltText
        {
            get; set;
        }

        public string SuccessTitle
        {
            get; set;
        }

        public string SuccessText
        {
            get; set;
        }

        public string SuccessFluff
        {
            get; set;
        }

        public string FailTitle
        {
            get; set;
        }

        public string FailText
        {
            get; set;
        }

        public string FailFluff
        {
            get; set;
        }

        public string SuccessFileName
        {
            get; set;
        }

        public string SuccessExportName
        {
            get; set;
        }

        public string FailFileName
        {
            get; set;
        }

        public string FailExportName
        {
            get; set;
        }
    }
}
