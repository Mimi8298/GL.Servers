namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class AllianceLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public int ExpPoints
        {
            get; set;
        }

        public bool IsVisible
        {
            get; set;
        }

        public int TroopRequestCooldown
        {
            get; set;
        }

        public int TroopDonationLimit
        {
            get; set;
        }

        public int TroopDonationRefund
        {
            get; set;
        }

        public int TroopDonationUpgrade
        {
            get; set;
        }

        public int WarLootCapacityPercent
        {
            get; set;
        }

        public int WarLootMultiplierPercent
        {
            get; set;
        }

        public int BadgeLevel
        {
            get; set;
        }

        public string BannerSWF
        {
            get; set;
        }
    }
}
