namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PersonalTrainData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PersonalTrainData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PersonalTrainData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string Carts
        {
            get; set;
        }

        public int RepairPrice
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

        public string IconName
        {
            get; set;
        }

        public string TID
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

        public int StopX
        {
            get; set;
        }

        public int StopY
        {
            get; set;
        }

        public int ParkX
        {
            get; set;
        }

        public int ParkY
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string ChooSound
        {
            get; set;
        }

        public string WhistleSound
        {
            get; set;
        }

        public int WheelCircum
        {
            get; set;
        }

        public string ChooEffectExportName
        {
            get; set;
        }

        public int StoppedTime
        {
            get; set;
        }

        public int MoveTime
        {
            get; set;
        }

        public int MoveDistanceX
        {
            get; set;
        }

        public int Capacity
        {
            get; set;
        }

        public int CooldownIntervalSeconds
        {
            get; set;
        }

        public int InstantCompleteCooldown
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
