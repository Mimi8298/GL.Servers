namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Bosses : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Bosses"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Bosses(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int PlayerCount
        {
            get; set;
        }

        public int RequiredCampaignProgressToUnlock
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

        public string Boss
        {
            get; set;
        }

        public int BossLevel
        {
            get; set;
        }
    }
}
