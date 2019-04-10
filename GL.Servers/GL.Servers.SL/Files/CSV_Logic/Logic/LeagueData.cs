namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class LeagueData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LeagueData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LeagueData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string IconExportNameSmall
        {
            get; set;
        }

        public string MultiplayerIconSWF
        {
            get; set;
        }

        public string MultiplayerIconExportName
        {
            get; set;
        }

        public string IconText
        {
            get; set;
        }

        public int DemoteLimit
        {
            get; set;
        }

        public int PromoteLimit
        {
            get; set;
        }

        public int PlacementLimitLow
        {
            get; set;
        }

        public int PlacementLimitHigh
        {
            get; set;
        }

        public bool DemoteEnabled
        {
            get; set;
        }

        public bool PromoteEnabled
        {
            get; set;
        }

        public int PVPGoldReward
        {
            get; set;
        }

        public int PVPXpReward
        {
            get; set;
        }

        public string QuestItem
        {
            get; set;
        }
    }
}
