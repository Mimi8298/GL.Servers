namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ShopData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ShopData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ShopData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ShopData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Category
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string Rarity
        {
            get; set;
        }

        internal bool Disabled
        {
            get; set;
        }

        internal string Resource
        {
            get; set;
        }

        internal int Cost
        {
            get; set;
        }

        internal int Count
        {
            get; set;
        }

        internal int CycleDuration
        {
            get; set;
        }

        internal int CycleDeadzoneStart
        {
            get; set;
        }

        internal int CycleDeadzoneEnd
        {
            get; set;
        }

        internal bool TopSection
        {
            get; set;
        }

        internal bool SpecialOffer
        {
            get; set;
        }

        internal int DurationSecs
        {
            get; set;
        }

        internal int AvailabilitySecs
        {
            get; set;
        }

        internal bool SyncToShopCycle
        {
            get; set;
        }

        internal string Chest
        {
            get; set;
        }

        internal int TrophyLimit
        {
            get; set;
        }

        internal string IAP
        {
            get; set;
        }

        internal string StarterPack_Item0_Type
        {
            get; set;
        }

        internal string StarterPack_Item0_ID
        {
            get; set;
        }

        internal int StarterPack_Item0_Param1
        {
            get; set;
        }

        internal string StarterPack_Item1_Type
        {
            get; set;
        }

        internal string StarterPack_Item1_ID
        {
            get; set;
        }

        internal int StarterPack_Item1_Param1
        {
            get; set;
        }

        internal string StarterPack_Item2_Type
        {
            get; set;
        }

        internal string StarterPack_Item2_ID
        {
            get; set;
        }

        internal int StarterPack_Item2_Param1
        {
            get; set;
        }

        internal int ValueMultiplier
        {
            get; set;
        }

        internal bool AppendArenaToChestName
        {
            get; set;
        }

        internal string TiedToArenaUnlock
        {
            get; set;
        }

        internal string RepeatPurchaseGemPackOverride
        {
            get; set;
        }

        internal string EventName
        {
            get; set;
        }

        internal bool CostAdjustBasedOnChestContents
        {
            get; set;
        }

        internal bool IsChronosOffer
        {
            get; set;
        }

    }
}