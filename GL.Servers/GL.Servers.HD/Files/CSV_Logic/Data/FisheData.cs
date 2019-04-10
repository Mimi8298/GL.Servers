namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FisheData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FisheData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FisheData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ExportNameOld
        {
            get; set;
        }

        public string AssetPrefix
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

        public string FavouriteBait
        {
            get; set;
        }

        public string FavouriteBait2
        {
            get; set;
        }

        public string HabitatExportName
        {
            get; set;
        }

        public string HabitatOldExportName
        {
            get; set;
        }

        public int PhotoAngle
        {
            get; set;
        }

        public int BaseProbability
        {
            get; set;
        }

        public string MainColour
        {
            get; set;
        }

        public string SubColour
        {
            get; set;
        }

        public string Shape
        {
            get; set;
        }

        public string AnimationExportNamePrefix
        {
            get; set;
        }

        public string Special
        {
            get; set;
        }

        public int WeightMin
        {
            get; set;
        }

        public int WeightMax
        {
            get; set;
        }

        public int StruggleSpeedMin
        {
            get; set;
        }

        public int StruggleSpeedMax
        {
            get; set;
        }

        public int RodPullStrengthPercentage
        {
            get; set;
        }

        public int SwimAgainstPercentage
        {
            get; set;
        }

        public int CircleRadiusCatch
        {
            get; set;
        }

        public int CircleCatchDurationMS
        {
            get; set;
        }

        public int CatchRewardDiamonds
        {
            get; set;
        }

        public int CatchRewardXP
        {
            get; set;
        }

        public bool EventFish
        {
            get; set;
        }
    }
}
