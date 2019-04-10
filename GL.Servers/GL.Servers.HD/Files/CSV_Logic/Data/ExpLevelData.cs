namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ExpLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExpLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExpLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Level
        {
            get; set;
        }

        public int ExpToNextLevel
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string GoodReward
        {
            get; set;
        }

        public int GoodAmount
        {
            get; set;
        }

        public string LoadingHintTIDs
        {
            get; set;
        }

        public int MaxFields
        {
            get; set;
        }

        public int MaxEvents
        {
            get; set;
        }

        public int CaretakerStorageCapacity
        {
            get; set;
        }

        public int MillerStorageCapacity
        {
            get; set;
        }

        public int MaxCatAmount
        {
            get; set;
        }

        public int MaxCatHouseAmount
        {
            get; set;
        }

        public int MaxDogAmount
        {
            get; set;
        }

        public int MaxDogHouseAmount
        {
            get; set;
        }

        public int MaxHorseAmount
        {
            get; set;
        }

        public int MaxHorseHouseAmount
        {
            get; set;
        }

        public int MaxDonkeyAmount
        {
            get; set;
        }

        public int MaxDonkeyHouseAmount
        {
            get; set;
        }

        public int MaxBunnyAmount
        {
            get; set;
        }

        public int MaxBunnyHouseAmount
        {
            get; set;
        }

        public int MaxKittenAmount
        {
            get; set;
        }

        public int MaxKittenHouseAmount
        {
            get; set;
        }

        public int MaxPuppyAmount
        {
            get; set;
        }

        public int MaxPuppyHouseAmount
        {
            get; set;
        }

        public int MaxChickenHabitats
        {
            get; set;
        }

        public int MaxCowHabitats
        {
            get; set;
        }

        public int MaxSheepHabitats
        {
            get; set;
        }

        public int MaxPigHabitats
        {
            get; set;
        }

        public int MaxGoatHabitats
        {
            get; set;
        }

        public int MaxHammermills
        {
            get; set;
        }

        public int MaxSmelters
        {
            get; set;
        }

        public int MaxBeeHabitats
        {
            get; set;
        }

        public int MinGoodsInOrderDelivery
        {
            get; set;
        }

        public int MaxGoodsInOrderDelivery
        {
            get; set;
        }

        public int OrderCancelTimeMin
        {
            get; set;
        }

        public int OrderSpeedUpPrice
        {
            get; set;
        }

        public int BoatOrderSpeedUpPrice
        {
            get; set;
        }

        public int MaxOrderCount
        {
            get; set;
        }

        public int CashExpMultiplier
        {
            get; set;
        }

        public int EventCounterClicks
        {
            get; set;
        }

        public int OrderMinValue
        {
            get; set;
        }

        public int OrderMaxValue
        {
            get; set;
        }

        public int DiamondsToCoinsRate
        {
            get; set;
        }

        public int GuestOrderValue
        {
            get; set;
        }

        public int MinCrateAmountInBoatOrder
        {
            get; set;
        }

        public int MaxCrateAmountInBoatOrder
        {
            get; set;
        }

        public int MinBoatOrderValue
        {
            get; set;
        }

        public int MaxBoatOrderValue
        {
            get; set;
        }

        public int MinGoodTypesInBoatOrder
        {
            get; set;
        }

        public int MaxGoodTypesInBoatOrder
        {
            get; set;
        }

        public int MinBoatStayTimeMinutes
        {
            get; set;
        }

        public int MaxBoatStayTimeMinutes
        {
            get; set;
        }

        public int BoatETAMinutes
        {
            get; set;
        }

        public int BoatRewardMultiplier
        {
            get; set;
        }

        public int SpecialOrderProbablity
        {
            get; set;
        }

        public int SpecialOrderMaxCount
        {
            get; set;
        }

        public int LevelThresholds
        {
            get; set;
        }

        public string ThresholdRewardSet
        {
            get; set;
        }

        public string FeatureTID
        {
            get; set;
        }

        public string FeaturePNGURL
        {
            get; set;
        }
    }
}
