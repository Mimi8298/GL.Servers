namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class LevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string BackgroundSWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string Puzzle
        {
            get; set;
        }

        public string MusicPlaylist
        {
            get; set;
        }

        public int HeroCount
        {
            get; set;
        }

        public string PreferredHeroes
        {
            get; set;
        }

        public int StarThresholds
        {
            get; set;
        }

        public int Seed
        {
            get; set;
        }

        public bool FixedSeed
        {
            get; set;
        }

        public int FreeHeroSwaps
        {
            get; set;
        }

        public int EnergyCost
        {
            get; set;
        }

        public int EnergyReward
        {
            get; set;
        }

        public string GatePassRequired
        {
            get; set;
        }

        public int RedHeroEnergy
        {
            get; set;
        }

        public int GreenHeroEnergy
        {
            get; set;
        }

        public int BlueHeroEnergy
        {
            get; set;
        }

        public int YellowHeroEnergy
        {
            get; set;
        }

        public int PurpleHeroEnergy
        {
            get; set;
        }

        public bool DisableGreenBlock
        {
            get; set;
        }

        public bool DisableRedBlock
        {
            get; set;
        }

        public int MoveTimerMS
        {
            get; set;
        }

        public bool SkipFirstTurn
        {
            get; set;
        }
    }
}
