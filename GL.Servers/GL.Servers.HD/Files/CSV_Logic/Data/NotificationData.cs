namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NotificationData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NotificationData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NotificationData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public bool Enabled
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int DelayMinutes
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public int MinLevel
        {
            get; set;
        }

        public int MaxLevel
        {
            get; set;
        }
    }
}
