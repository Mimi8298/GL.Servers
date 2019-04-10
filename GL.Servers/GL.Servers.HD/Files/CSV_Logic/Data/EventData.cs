namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

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

        public string Event
        {
            get; set;
        }

        public string GoodReward
        {
            get; set;
        }

        public int GoodAmount
        {
            get; set;
        }

        public int Group1
        {
            get; set;
        }

        public int Group2
        {
            get; set;
        }

        public int Group3
        {
            get; set;
        }

        public int Group4
        {
            get; set;
        }

        public int Group5
        {
            get; set;
        }

        public int Group6
        {
            get; set;
        }

        public int Group7
        {
            get; set;
        }

        public int Group8
        {
            get; set;
        }

        public int Group9
        {
            get; set;
        }

        public int Group10
        {
            get; set;
        }

        public int Group11
        {
            get; set;
        }

        public int Group12
        {
            get; set;
        }

        public int Group13
        {
            get; set;
        }

        public int Group14
        {
            get; set;
        }

        public int Group15
        {
            get; set;
        }

        public int Group16
        {
            get; set;
        }

        public int Group17
        {
            get; set;
        }

        public int Group18
        {
            get; set;
        }

        public int Group19
        {
            get; set;
        }

        public int Group20
        {
            get; set;
        }

        public int Group21
        {
            get; set;
        }

        public int Group22
        {
            get; set;
        }

        public int Group23
        {
            get; set;
        }

        public int Group24
        {
            get; set;
        }

        public int Group25
        {
            get; set;
        }

        public int Group26
        {
            get; set;
        }

        public int Group27
        {
            get; set;
        }
    }
}
