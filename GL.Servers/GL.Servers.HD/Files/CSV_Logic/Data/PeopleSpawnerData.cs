namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PeopleSpawnerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PeopleSpawnerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PeopleSpawnerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int MinTutorialPeopleToSpawnBeforeUnlock
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

        public int MinOrderSize
        {
            get; set;
        }

        public int MaxOrderSize
        {
            get; set;
        }

        public int CanCompleteOrderChance
        {
            get; set;
        }

        public int MaxPeople
        {
            get; set;
        }

        public int GoodNumber
        {
            get; set;
        }

        public int GoldMultiplier
        {
            get; set;
        }

        public int ExpMultiplier
        {
            get; set;
        }

        public int ConstantExp
        {
            get; set;
        }

        public int ForcedUnlockLevel
        {
            get; set;
        }
    }
}
