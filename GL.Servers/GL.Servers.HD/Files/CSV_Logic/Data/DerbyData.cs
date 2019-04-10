namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DerbyData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DerbyData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DerbyData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public string BrokenExportName
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public string LockedExportName
        {
            get; set;
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public string FileName
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

        public int TimeMin
        {
            get; set;
        }

        public int TimeSec
        {
            get; set;
        }

        public int ExpToBuild
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

        public int RewardRerollPrice
        {
            get; set;
        }

        public string TrackDecoExportName
        {
            get; set;
        }

        public int TrackDecoLayer
        {
            get; set;
        }

        public bool TrackDecoUseColoring
        {
            get; set;
        }

        public int TrackDecoMaxVelocity
        {
            get; set;
        }

        public bool TrackDecoPlayerOnly
        {
            get; set;
        }

        public int KeysToFitInView
        {
            get; set;
        }

        public int HorseSpeed
        {
            get; set;
        }

        public int HorseMaxRunningTimeSec
        {
            get; set;
        }

        public string TrackHorses
        {
            get; set;
        }

        public string HorseLeagueUp
        {
            get; set;
        }

        public string HorseLeagueDown
        {
            get; set;
        }

        public string HorseStaySame
        {
            get; set;
        }

        public string HorseStaySameFinal
        {
            get; set;
        }

        public string HorseIdle
        {
            get; set;
        }

        public string HorseVanish
        {
            get; set;
        }

        public string TrackColors
        {
            get; set;
        }

        public string DecoColors
        {
            get; set;
        }

        public int AddTaskCountMax
        {
            get; set;
        }

        public int DiamondCostPerAddTask
        {
            get; set;
        }

        public string InfoTitleTID
        {
            get; set;
        }

        public string InfoTextTID
        {
            get; set;
        }

        public int DerbyEndWarningSeconds
        {
            get; set;
        }

        public string DerbyEndWarningTID
        {
            get; set;
        }

        public int DerbyEndBlockSeconds
        {
            get; set;
        }

        public string DerbyEndBlockTID
        {
            get; set;
        }

        public int Rank
        {
            get; set;
        }

        public int RankBonus
        {
            get; set;
        }

        public int NeighborhoodSize
        {
            get; set;
        }

        public int NeighborhoodSizeBonus
        {
            get; set;
        }

        public int TrashedTaskPenalty
        {
            get; set;
        }
    }
}
