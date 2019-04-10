namespace GL.Servers.BB.Files.CSV_Logic
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class RegionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RegionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RegionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string z
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int RequiredMapRoomLevel
        {
            get; set;
        }

        public int ExplorationOrder
        {
            get; set;
        }

        public int ExplorationCost
        {
            get; set;
        }

        public int ExplorationTimeSeconds
        {
            get; set;
        }

        public List<string> Nodes
        {
            get; set;
        }

        public List<string> RequiredBaseLevelFile
        {
            get; set;
        }

        public List<string> LevelFile
        {
            get; set;
        }

        public List<string> AreaExportName
        {
            get; set;
        }

        public List<string> TreasureResource
        {
            get; set;
        }

        public List<int> TreasureCount
        {
            get; set;
        }

        public List<int> ScoreCount
        {
            get; set;
        }

        public List<int> ArtifactType
        {
            get; set;
        }

        public string BossTeaserExportName
        {
            get; set;
        }

        public int DeepseaDepth
        {
            get; set;
        }

        internal int GetNodeCount()
        {
            return this.Nodes.Count;
        }

        internal int GetNodeType(int Id)
        {
            if (Id < this.GetNodeCount())
            {
                switch (this.Nodes[Id])
                {
                    case "home":
                        return 0;
                    case "player_base":
                        return 1;
                    case "player_outpost":
                        return 2;
                    case "npc_base":
                        return 3;
                    case "npc_outpost":
                        return 4;
                    case "treasure":
                        return 6;
                    case "unique_npc":
                        return 8;
                    case "deepsea":
                        return 12;
                    case "defence_event":
                        return 15;
                    case "megabase_event":
                        return 16;
                    case "sector_base":
                        return 18;
                    case "sector_hq":
                        return 19;
                    default:
                        Logging.Info(this.GetType(), "Corrupted node in regions.csv");
                        return 0;
                }
            }

            return 0;
        }
    }
}
