namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class EventData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string NotificationTID
        {
            get; set;
        }

        public int RegionIndex
        {
            get; set;
        }

        public int NodeIndex
        {
            get; set;
        }

        public string LaunchDate
        {
            get; set;
        }

        public int DurationDays
        {
            get; set;
        }

        public int IntervalDays
        {
            get; set;
        }

        public int NumLevels
        {
            get; set;
        }

        public int PersistentEventNumAttacks
        {
            get; set;
        }

        public bool PlayerDefenceEvent
        {
            get; set;
        }

        public bool MercenaryCampEvent
        {
            get; set;
        }

        public string BoomboxEntry
        {
            get; set;
        }

        public int C0
        {
            get; set;
        }

        public int C1
        {
            get; set;
        }

        public int C2
        {
            get; set;
        }

        public int R0
        {
            get; set;
        }

        public int R1
        {
            get; set;
        }

        public int R2
        {
            get; set;
        }

        public string DummyNpc
        {
            get; set;
        }

        public int CommonPieceInterval
        {
            get; set;
        }

        public int RarePieceInterval
        {
            get; set;
        }

        public int EpicPieceInterval
        {
            get; set;
        }

        public bool Deprecated
        {
            get; set;
        }
    }
}
