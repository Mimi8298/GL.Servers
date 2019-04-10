namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Campaign : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Campaign"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Campaign(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string Location
        {
            get; set;
        }

        public string AllowedHeroes
        {
            get; set;
        }

        public string Reward
        {
            get; set;
        }

        public int LevelGenerationSeed
        {
            get; set;
        }

        public string Map
        {
            get; set;
        }

        public string Enemies
        {
            get; set;
        }

        public int EnemyLevel
        {
            get; set;
        }

        public string Boss
        {
            get; set;
        }

        public int BossLevel
        {
            get; set;
        }

        public string Base
        {
            get; set;
        }

        public int NumBases
        {
            get; set;
        }

        public int BaseLevel
        {
            get; set;
        }

        public string Tower
        {
            get; set;
        }

        public int NumTowers
        {
            get; set;
        }

        public int TowerLevel
        {
            get; set;
        }

        public int RequiredStars
        {
            get; set;
        }
    }
}
