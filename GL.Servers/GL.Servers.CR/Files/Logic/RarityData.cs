namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class RarityData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RarityData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RarityData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // RarityData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
		    if (this.Name == "Common")
		    {
		        CSV.Tables.RarityCommonData = this;
		    }
		}
	
        internal int LevelCount
        {
            get; set;
        }

        internal int RelativeLevel
        {
            get; set;
        }

        internal int MirrorRelativeLevel
        {
            get; set;
        }

        internal int CloneRelativeLevel
        {
            get; set;
        }

        internal int DonateCapacity
        {
            get; set;
        }

        internal int SortCapacity
        {
            get; set;
        }

        internal int DonateReward
        {
            get; set;
        }

        internal int DonateXP
        {
            get; set;
        }

        internal int GoldConversionValue
        {
            get; set;
        }

        internal int ChanceWeight
        {
            get; set;
        }

        internal int BalanceMultiplier
        {
            get; set;
        }

        internal int[] UpgradeExp
        {
            get; set;
        }

        internal int[] UpgradeMaterialCount
        {
            get; set;
        }

        internal int[] UpgradeCost
        {
            get; set;
        }

        internal int PowerLevelMultiplier
        {
            get; set;
        }

        internal int RefundGems
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string CardBaseFileName
        {
            get; set;
        }

        internal string BigFrameExportName
        {
            get; set;
        }

        internal string CardBaseExportName
        {
            get; set;
        }

        internal string StackedCardExportName
        {
            get; set;
        }

        internal string CardRewardExportName
        {
            get; set;
        }

        internal string CastEffect
        {
            get; set;
        }

        internal string InfoTitleExportName
        {
            get; set;
        }

        internal string CardRarityBGExportName
        {
            get; set;
        }

        internal int SortOrder
        {
            get; set;
        }

        internal int Red
        {
            get; set;
        }

        internal int Green
        {
            get; set;
        }

        internal int Blue
        {
            get; set;
        }

        internal string AppearEffect
        {
            get; set;
        }

        internal string BuySound
        {
            get; set;
        }

        internal string LoopEffect
        {
            get; set;
        }

        internal int CardTxtBgFrameIdx
        {
            get; set;
        }

        internal string CardGlowInstanceName
        {
            get; set;
        }

        internal string SpellSelectedSound
        {
            get; set;
        }

        internal string SpellAvailableSound
        {
            get; set;
        }

        internal string RotateExportName
        {
            get; set;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

    }
}