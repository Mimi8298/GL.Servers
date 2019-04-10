namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PassengerAnimalBonuData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PassengerAnimalBonuData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PassengerAnimalBonuData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int WeightAnimals1
        {
            get; set;
        }

        public int WeightAnimals2
        {
            get; set;
        }

        public int WeightAnimals3
        {
            get; set;
        }

        public int WeightAnimals4
        {
            get; set;
        }
    }
}
