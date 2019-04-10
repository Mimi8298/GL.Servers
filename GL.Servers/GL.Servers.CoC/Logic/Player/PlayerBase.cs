namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Logic.Slots;
    using Newtonsoft.Json;

    internal class PlayerBase
    {
        internal Level Level;

        [JsonProperty] internal int AllianceHighID;
        [JsonProperty] internal int AllianceLowID;

        [JsonProperty] internal DataSlots ResourceCaps;
        [JsonProperty] internal ResourceSlots Resources;

        [JsonProperty] internal DataSlots AchievementProgress;
        [JsonProperty] internal UnitSlots Units;
        [JsonProperty] internal UnitSlots Units2;
        [JsonProperty] internal DataSlots Spells;
        [JsonProperty] internal DataSlots UnitUpgrades;
        [JsonProperty] internal AllianceUnitSlots AllianceUnits;
        [JsonProperty] internal DataSlots SpellUpgrades;
        [JsonProperty] internal DataSlots HeroUpgrades;

        [JsonProperty] internal NpcMapSlots NpcMapProgress;
        [JsonProperty] internal DataSlots NpcLootedGold;
        [JsonProperty] internal DataSlots NpcLootedElixir;
        [JsonProperty] internal DataSlots Variables;

        [JsonProperty] internal int TownHallLevel;
        [JsonProperty] internal int TownHallLevel2;

        [JsonProperty] internal int CastleLevel;
        [JsonProperty] internal int CastleTotalCapacity;
        [JsonProperty] internal int CastleUsedCapacity;
        [JsonProperty] internal int CastleTotalSpellCapacity;
        [JsonProperty] internal int CastleUsedSpellCapacity;

        /// <summary>
        /// Gets a value indicating whether the player is in alliance.
        /// </summary>
        internal bool InAlliance
        {
            get
            {
                return this.AllianceID > 0;
            }
        }

        /// <summary>
        /// Gets the map.
        /// </summary>
        internal int Map
        {
            get
            {
                return this.Variables.GetCountByGlobalId(37000012) & 1;
            }
        }

        /// <summary>
        /// Gets or sets the gold count.
        /// </summary>
        internal int Gold
        {
            get
            {
                return this.Resources.GetCountByGlobalId(3000001);
            }
            set
            {
                this.Resources.Set(3000001, value);
            }
        }

        /// <summary>
        /// Gets or sets the elixir count.
        /// </summary>
        internal int Elixir
        {
            get
            {
                return this.Resources.GetCountByGlobalId(3000002);
            }
            set
            {
                this.Resources.Set(3000002, value);
            }
        }

        /// <summary>
        /// Gets or sets the dark elixir count.
        /// </summary>
        internal int DarkElixir
        {
            get
            {
                return this.Resources.GetCountByGlobalId(3000003);
            }
            set
            {
                this.Resources.Set(3000003, value);
            }
        }

        /// <summary>
        /// Gets or sets the gold 2 count.
        /// </summary>
        internal int Gold2
        {
            get
            {
                return this.Resources.GetCountByGlobalId(3000007);
            }
            set
            {
                this.Resources.Set(3000007, value);
            }
        }

        /// <summary>
        /// Gets or sets the elixir 2 count.
        /// </summary>
        internal int Elixir2
        {
            get
            {
                return this.Resources.GetCountByGlobalId(3000008);
            }
            set
            {
                this.Resources.Set(3000008, value);
            }
        }

        /// <summary>
        /// Gets the town hall level data.
        /// </summary>
        internal TownhallLevelData TownhallLevelData
        {
            get
            {
                return (TownhallLevelData) CSV.Tables.Get(Gamefile.TownHall).GetDataWithInstanceID(this.TownHallLevel);
            }
        }

        /// <summary>
        /// Gets the town hall 2 level data.
        /// </summary>
        internal TownhallLevelData TownhallLevel2Data
        {
            get
            {
                return (TownhallLevelData) CSV.Tables.Get(Gamefile.TownHall).GetDataWithInstanceID(this.TownHallLevel2);
            }
        }

        /// <summary>
        /// Gets the clan identifier.
        /// </summary>
        internal long AllianceID
        {
            get
            {
                return (long) this.AllianceHighID << 32 | (uint) this.AllianceLowID;
            }
        }

        /// <summary>
        /// Gets the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return this.Resources.Checksum
                       + this.ResourceCaps.Checksum // OutOfSync
                       + this.Units.Checksum
                       + this.Spells.Checksum
                       + this.AllianceUnits.Checksum
                       + this.UnitUpgrades.Checksum
                       + this.SpellUpgrades.Checksum
                       + this.Units2.Checksum
                       + this.TownHallLevel +
                       + this.TownHallLevel2;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is client player.
        /// </summary>
        internal virtual bool ClientPlayer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is npc player.
        /// </summary>
        internal virtual bool NpcPlayer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBase"/> class.
        /// </summary>
        public PlayerBase()
        {
            this.AchievementProgress    = new DataSlots();
            this.ResourceCaps           = new DataSlots();
            this.Resources              = new ResourceSlots();
            this.Units                  = new UnitSlots();
            this.Units2                 = new UnitSlots();
            this.Spells                 = new DataSlots();
            this.UnitUpgrades           = new DataSlots();
            this.SpellUpgrades          = new DataSlots();
            this.HeroUpgrades           = new DataSlots();
            this.AllianceUnits          = new AllianceUnitSlots();

            this.NpcMapProgress         = new NpcMapSlots();
            this.NpcLootedGold          = new DataSlots();
            this.NpcLootedElixir        = new DataSlots();

            this.Variables              = new DataSlots();
        }

        /// <summary>
        /// Gets the upgrade unit level.
        /// </summary>
        internal int GetUnitUpgradeLevel(Data Data)
        {
            if (Data.GetDataType() == 4)
            {
                return this.UnitUpgrades.GetCountByData(Data);
            }

            return this.SpellUpgrades.GetCountByData(Data);
        }

        /// <summary>
        /// Increases the upgrade unit level.
        /// </summary>
        internal void IncreaseUnitUpgradeLevel(Data Data)
        {
            if (Data.GetDataType() == 4)
            {
                this.UnitUpgrades.Add(Data, 1);
            }
            else
                this.SpellUpgrades.Add(Data, 1);
        }

        /// <summary>
        /// Gets the available resource storage.
        /// </summary>
        internal int GetAvailableResourceStorage(Data Resource)
        {
            return this.ResourceCaps.GetCountByData(Resource) - this.Resources.GetCountByData(Resource);
        }
    }
}