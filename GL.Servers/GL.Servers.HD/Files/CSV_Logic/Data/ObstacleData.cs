namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ObstacleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ObstacleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ObstacleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameBase
        {
            get; set;
        }

        public int ClearTimeSeconds
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public string ClearResource
        {
            get; set;
        }

        public int ClearCost
        {
            get; set;
        }

        public string LootResource
        {
            get; set;
        }

        public int LootCount
        {
            get; set;
        }

        public string ClearEffect
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public int RespawnWeight
        {
            get; set;
        }

        public bool IsTombstone
        {
            get; set;
        }

        public int TombGroup
        {
            get; set;
        }

        public int LootMultiplierForVersion2
        {
            get; set;
        }

        public int AppearancePeriodHours
        {
            get; set;
        }

        public int MinRespawnTimeHours
        {
            get; set;
        }

        public string SpawnObstacle
        {
            get; set;
        }

        public int SpawnRadius
        {
            get; set;
        }

        public int SpawnIntervalSeconds
        {
            get; set;
        }

        public int SpawnCount
        {
            get; set;
        }

        public int MaxSpawned
        {
            get; set;
        }

        public int MaxLifetimeSpawns
        {
            get; set;
        }

        public int LootDefensePercentage
        {
            get; set;
        }

        public int RedMul
        {
            get; set;
        }

        public int GreenMul
        {
            get; set;
        }

        public int BlueMul
        {
            get; set;
        }

        public int RedAdd
        {
            get; set;
        }

        public int GreenAdd
        {
            get; set;
        }

        public int BlueAdd
        {
            get; set;
        }

        public bool LightsOn
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }

        public int Village2RespawnCount
        {
            get; set;
        }

        public int VariationCount
        {
            get; set;
        }

        public bool TallGrass
        {
            get; set;
        }

        public bool TallGrassSpawnPoint
        {
            get; set;
        }

        public int LootHighlightPercentage
        {
            get; set;
        }

        public string HighlightExportName
        {
            get; set;
        }
    }
}
