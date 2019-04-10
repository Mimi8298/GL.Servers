namespace GL.Servers.HD.Files.CSV_Logic.Data
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TownhallLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TownhallLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TownhallLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // Data.Load(this, this.GetType(), Row);
        }

        public List<int> AttackCost
        {
            get; set;
        }

        public List<int> ResourceStorageLootPercentage
        {
            get; set;
        }

        public List<int> DarkElixirStorageLootPercentage
        {
            get; set;
        }

        public List<int> ResourceStorageLootCap
        {
            get; set;
        }

        public List<int> DarkElixirStorageLootCap
        {
            get; set;
        }

        public List<int> WarPrizeResourceCap
        {
            get; set;
        }

        public List<int> WarPrizeDarkElixirCap
        {
            get; set;
        }

        public List<int> WarPrizeAllianceExpCap
        {
            get; set;
        }

        public List<int> CartLootCapResource
        {
            get; set;
        }

        public List<int> CartLootReengagementResource
        {
            get; set;
        }

        public List<int> CartLootCapDarkElixir
        {
            get; set;
        }

        public List<int> CartLootReengagementDarkElixir
        {
            get; set;
        }
    }
}
