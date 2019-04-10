namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NeighborhoodTaskData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NeighborhoodTaskData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NeighborhoodTaskData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TaskType
        {
            get; set;
        }

        public string SubTaskType
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

        public string IconTooltipTID
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string SubTaskIcon
        {
            get; set;
        }

        public int MinRequiredQuantity
        {
            get; set;
        }

        public int MaxRequiredQuantity
        {
            get; set;
        }

        public bool Deprecated
        {
            get; set;
        }

        public int MinXPLevelForSpawn
        {
            get; set;
        }

        public int SpawnProbabilityWeightLeague1
        {
            get; set;
        }

        public int SpawnProbabilityWeightLeague2
        {
            get; set;
        }

        public int SpawnProbabilityWeightLeague3
        {
            get; set;
        }

        public int SpawnProbabilityWeightLeague4
        {
            get; set;
        }

        public int SpawnProbabilityWeightLeague5
        {
            get; set;
        }

        public int MinEventTimeMinLeftForSpawn
        {
            get; set;
        }

        public string TaskParam1
        {
            get; set;
        }

        public string TaskParam2
        {
            get; set;
        }

        public string TaskParam3
        {
            get; set;
        }

        public int MinRewardNeighborhoodPoints
        {
            get; set;
        }

        public int MaxRewardNeighborhoodPoints
        {
            get; set;
        }

        public int MinTaskDurationMin
        {
            get; set;
        }

        public int MaxTaskDurationMin
        {
            get; set;
        }

        public int CooldownIntervalSeconds
        {
            get; set;
        }

        public int AdminCooldownIntervalSeconds
        {
            get; set;
        }

        public int InstantCompleteCooldown
        {
            get; set;
        }
    }
}
