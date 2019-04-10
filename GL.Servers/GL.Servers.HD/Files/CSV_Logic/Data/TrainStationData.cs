namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TrainStationData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TrainStationData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TrainStationData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNamePlatform
        {
            get; set;
        }

        public string ExportNameWaterTank
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

        public int BrokenPartCount
        {
            get; set;
        }

        public string BrokenExportName
        {
            get; set;
        }

        public string BrokenExportNamePlatform
        {
            get; set;
        }

        public string BrokenExportNameWaterTank
        {
            get; set;
        }

        public int TileWidthWaterTank
        {
            get; set;
        }

        public int TileHeightWaterTank
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public int NPCTrainMinPassengers
        {
            get; set;
        }

        public int NPCTrainMaxPassengers
        {
            get; set;
        }

        public int NPCTrainIntervalSeconds
        {
            get; set;
        }

        public int NPCTrainSpeedUpPrice
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
    }
}
