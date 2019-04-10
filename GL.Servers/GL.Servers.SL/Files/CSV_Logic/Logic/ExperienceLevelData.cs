namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class ExperienceLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExperienceLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExperienceLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int ExpPoints
        {
            get; set;
        }

        public int PvpBonusXp
        {
            get; set;
        }

        public int MapChestMinGold
        {
            get; set;
        }

        public int MapChestMaxGold
        {
            get; set;
        }

        public int MapChestMinXP
        {
            get; set;
        }

        public int MapChestMaxXP
        {
            get; set;
        }

        public int DefaultQuestRewardGoldPerEnergy
        {
            get; set;
        }

        public int DefaultQuestRewardXpPerEnergy
        {
            get; set;
        }
    }
}
