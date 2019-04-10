namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ReputationLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ReputationLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ReputationLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Level
        {
            get; set;
        }

        public int RepToNextLevel
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

        public int MinGoodsInPassengerService
        {
            get; set;
        }

        public int MaxGoodsInPassengerService
        {
            get; set;
        }

        public int PassengerServiceMinValue
        {
            get; set;
        }

        public int PassengerServiceMaxValue
        {
            get; set;
        }

        public int PassengerServiceMinCount
        {
            get; set;
        }

        public int PassengerServiceMaxCount
        {
            get; set;
        }

        public int PassengerAnimalViewChance
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }
    }
}
