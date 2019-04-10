namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ServiceBuildingData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ServiceBuildingData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ServiceBuildingData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string Goods
        {
            get; set;
        }

        public int ServiceTimeMin
        {
            get; set;
        }

        public int ServiceTimeSec
        {
            get; set;
        }

        public int InstantCompleteService
        {
            get; set;
        }

        public int ReputationPerService
        {
            get; set;
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public string ShopIconFileName
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public int ExpToBuild
        {
            get; set;
        }

        public int TimeMin
        {
            get; set;
        }

        public int TimeSec
        {
            get; set;
        }

        public int InstantComplete
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string TapProcessingSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public string BuySound
        {
            get; set;
        }

        public string RandomSounds
        {
            get; set;
        }

        public string CollectionTool1
        {
            get; set;
        }

        public int CollectionTool1Amount
        {
            get; set;
        }

        public string CollectionTool2
        {
            get; set;
        }

        public int CollectionTool2Amount
        {
            get; set;
        }

        public string CollectionTool3
        {
            get; set;
        }

        public int CollectionTool3Amount
        {
            get; set;
        }

        public int ReputationLevelRequired
        {
            get; set;
        }

        public int ServiceSlots
        {
            get; set;
        }

        public int CoinBonus
        {
            get; set;
        }

        public int ExpAndRepBonus
        {
            get; set;
        }

        public int TimeBonus
        {
            get; set;
        }

        public int CoinBonusLevelRequirement
        {
            get; set;
        }

        public int ExpAndRepBonusLevelRequirement
        {
            get; set;
        }

        public int TimeBonusLevelRequirement
        {
            get; set;
        }

        public int VisitorDoorTileX
        {
            get; set;
        }

        public int VisitorDoorTileY
        {
            get; set;
        }

        public int ReputationUnlockLevel
        {
            get; set;
        }

        public int VisitorRequestProbabilityBuildingUnlocked
        {
            get; set;
        }

        public int VisitorRequestProbabilityBuildingConstructed
        {
            get; set;
        }
    }
}
