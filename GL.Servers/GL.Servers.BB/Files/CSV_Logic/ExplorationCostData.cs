namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ExplorationCostData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExplorationCostData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExplorationCostData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Cost
        {
            get; set;
        }

        public int NormalRewardMin
        {
            get; set;
        }

        public int NormalRewardMax
        {
            get; set;
        }

        public int RareRewardMin
        {
            get; set;
        }

        public int RareRewardMax
        {
            get; set;
        }

        public int EpicRewardMin
        {
            get; set;
        }

        public int EpicRewardMax
        {
            get; set;
        }

        public int RareRewardChance
        {
            get; set;
        }

        public int EpicRewardChance
        {
            get; set;
        }

        public int ResourceRewardChance
        {
            get; set;
        }

        public int GemRewardChance
        {
            get; set;
        }

        public int NormalPieceRewardChance
        {
            get; set;
        }

        public int RarePieceRewardChance
        {
            get; set;
        }

        public int EpicPieceRewardChance
        {
            get; set;
        }

        public int GoldRewardMultiplier
        {
            get; set;
        }

        public int ResourceRewardMultiplier
        {
            get; set;
        }

        public int GemRewardMultiplier
        {
            get; set;
        }

        public int NormalPieceRewardMultiplier
        {
            get; set;
        }

        public int RarePieceRewardMultiplier
        {
            get; set;
        }

        public int EpicPieceRewardMultiplier
        {
            get; set;
        }
    }
}
