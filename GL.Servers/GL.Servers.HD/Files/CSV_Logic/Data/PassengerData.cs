namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PassengerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PassengerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PassengerData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string IconExportName
        {
            get; set;
        }

        public string BodyIconExportName
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int WalkSpeedMin
        {
            get; set;
        }

        public int WalkSpeedMax
        {
            get; set;
        }

        public int WalkSpeedChangeTime
        {
            get; set;
        }

        public int WalkToServiceSpeedModifier
        {
            get; set;
        }

        public int WalkToTrainStationSpeedModifier
        {
            get; set;
        }

        public int IdleTime
        {
            get; set;
        }

        public int PathLengthMax
        {
            get; set;
        }

        public int DistanceFromTrainMax
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int AlightTrainTime
        {
            get; set;
        }

        public int ServiceTime
        {
            get; set;
        }

        public int ServiceFinishHappyTime
        {
            get; set;
        }

        public int BoardTrainTime
        {
            get; set;
        }

        public int SpeechBubbleTimer
        {
            get; set;
        }

        public string MumbleSound
        {
            get; set;
        }

        public string AttentionSound
        {
            get; set;
        }

        public string HappySound
        {
            get; set;
        }

        public string EatSound
        {
            get; set;
        }

        public int MumbleTimerMin
        {
            get; set;
        }

        public int MumbleTimerMax
        {
            get; set;
        }

        public int VoicePitch
        {
            get; set;
        }

        public int PickCountMax
        {
            get; set;
        }

        public bool IsMale
        {
            get; set;
        }

        public int VisitorPickReputationRewardPoints
        {
            get; set;
        }
    }
}
