namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PassengerSpawnerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PassengerSpawnerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PassengerSpawnerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int MinSpawnTime
        {
            get; set;
        }

        public int MaxSpawnTime
        {
            get; set;
        }

        public int MaxAIPassengers
        {
            get; set;
        }

        public int MaxIdleAIPassengers
        {
            get; set;
        }

        public int AIVisitorCooldownIntervalSeconds
        {
            get; set;
        }
    }
}
