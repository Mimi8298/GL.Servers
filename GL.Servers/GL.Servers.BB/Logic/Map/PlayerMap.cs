namespace GL.Servers.BB.Logic.Map
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Converter;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    [JsonConverter(typeof(PlayerMapConverter))]
    internal class PlayerMap
    {
        internal Player Player;

        internal int DeepseaLevel;

        [JsonProperty] internal bool DeepseaUpgrading;

        [JsonProperty] internal int ExplorationCounter;
        [JsonProperty] internal int ExplorationRegionId;
        [JsonProperty] internal bool ExplorationInProgress;

        [JsonProperty] internal int Frags;
        [JsonProperty] internal int Outposts;

        [JsonProperty] internal int MissionTile = -1;
        [JsonProperty] internal int MissionIndex = -1;
        [JsonProperty] internal int MissionCompletedAt;
        [JsonProperty] internal int MissionTotalLength;
        [JsonProperty] internal int MissionLeft;

        [JsonProperty] internal List<Sector> Sectors;
        [JsonProperty] internal List<MapRegion> MapRegions;

        /// <summary>
        /// Gets a value indicating the ckecksum of the <see cref="PlayerMap"/>.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating the number of explored regions.
        /// </summary>
        internal int NumExploredRegions
        {
            get
            {
                int Count = 0;

                for (int i = 0; i < this.MapRegions.Count; i++)
                {
                    Count += this.MapRegions[i].Explored ? 1 : 0;
                }

                return Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any sectors has been unlocked.
        /// </summary>
        internal bool AnySectorsUnlocked
        {
            get
            {
                return this.MapRegions.Exists(T => T.Explored);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMap"/> class.
        /// </summary>
        public PlayerMap()
        {
            List<Data> Regions     = CSV.Tables.Get(Gamefile.Region).Datas;
            List<Data> Sectors     = CSV.Tables.Get(Gamefile.Sector).Datas;

            this.MapRegions        = new List<MapRegion>(Regions.Count);
            this.Sectors           = new List<Sector>(Sectors.Count);

            for (int i = 0; i < Regions.Count; i++)
            {
                this.MapRegions.Add(new MapRegion(this));
            }

            for (int i = 0; i < Sectors.Count; i++)
            {
                this.Sectors.Add(new Sector((SectorData) Sectors[i], this));
            }

            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMap"/> class.
        /// </summary>
        public PlayerMap(Player Player) : this()
        {
            this.Player = Player;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            for (int i = 0; i < this.MapRegions.Count; i++)
            {
                this.MapRegions[i].Encode(Packet);
            }

            for (int i = 0; i < this.Sectors.Count; i++)
            {
                this.Sectors[i].Encode(Packet);
            }
            

            Packet.AddInt(this.ExplorationRegionId);
            Packet.AddBoolean(this.ExplorationInProgress);
            Packet.AddInt(this.ExplorationCounter);

            Packet.AddInt(this.Frags);
            Packet.AddInt(this.Outposts);

            Packet.AddInt(this.MissionTile);
            Packet.AddInt(this.MissionIndex);

            Packet.AddInt(this.MissionCompletedAt);
            Packet.AddInt(this.MissionTotalLength);
            Packet.AddInt(this.MissionLeft);

            Packet.AddBoolean(this.DeepseaUpgrading);
        }

        /// <summary>
        /// Returns if the specified region can be explored.
        /// </summary>
        internal bool CanExploreRegion(int regionId)
        {
            if (this.MapRegions.Count > regionId)
            {
                return !this.MapRegions[regionId].Explored;
            }

            Logging.Error(this.GetType(), "CanExploreRegion() - Index is out of range. Region " + regionId + " not exist.");

            return false;
        }

        /// <summary>
        /// Explores the specified region.
        /// </summary>
        /// <param name="Region"></param>
        internal void ExploreRegion(RegionData Region)
        {
            if (this.CanExploreRegion(Region.InstanceID))
            {
                this.MapRegions[Region.InstanceID].CreateFromData(Region);
            }
        }

        /// <summary>
        /// Skips the specified time.
        /// </summary>
        /// <param name="seconds">The time in seconds.</param>
        internal void FastForwardTime(int seconds)
        {
            if (seconds > 0)
            {
                for (int i = 0; i < this.Sectors.Count; i++)
                {
                    this.Sectors[i].FastForwardTime(seconds);
                }
            }
        }

        /// <summary>
        /// Returns the node corresponding at tile.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        internal MapNode GetNode(int tile)
        {
            int Region = tile >> 8;

            if (this.MapRegions.Count > Region)
            {
                return this.MapRegions[Region].GetNode(tile % 256);
            }

            Logging.Error(this.GetType(), "GetNode() - Index is out of range. Region " + Region + " not exist.");

            return null;
        }
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Initialize()
        {
            this.ExploreRegion((RegionData) CSV.Tables.GetDataById(38000000));
        }
    }
}