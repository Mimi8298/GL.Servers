namespace GL.Servers.BB.Logic
{
    using System;
    using System.Collections.Generic;
    
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.BB.Logic.Items;
    using GL.Servers.BB.Logic.Map;
    using GL.Servers.BB.Logic.Slots;
    using GL.Servers.BB.Logic.Slots.Items;
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json;
    
    internal class Player
    {
        internal Level Level;

        // Id

        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal int AllianceHighID;
        [JsonProperty] internal int AllianceLowID;
        
        // Login

        [JsonProperty] internal string PassToken;

        // Bool Members

        [JsonProperty] internal bool NameSetByUser;

        // Int Members

        [JsonProperty] internal int TownHallLevel;

        [JsonProperty] internal int Diamonds;
        [JsonProperty] internal int GainIntel;
        [JsonProperty] internal int AttackKFactor;
        [JsonProperty] internal int Score;
        [JsonProperty] internal int ExpLevel          = 1;
        [JsonProperty] internal int ExpPoints;
        [JsonProperty] internal int NumberOnePosCounter;
        [JsonProperty] internal int CumulativePurchasedDiamonds;

        // String Members

        [JsonProperty] internal string Name           = "Commandant";

        // Slot Members

        [JsonProperty] internal List<int> MissionCompleted; // Managed by MissionManager
        [JsonProperty] internal List<int> AchievementClaimed; // Managed by AchievementManager

        [JsonProperty] internal DataSlots<Item> Resources;
        [JsonProperty] internal DataSlots<Item> ResourceCaps;
        [JsonProperty] internal DataSlots<Item> ResourcesConvertedToSupplies;

        [JsonProperty] internal DataSlots<Item> UnitUpgrades;
        [JsonProperty] internal DataSlots<Item> TrapUpgrades;
        [JsonProperty] internal DataSlots<Item> HeroUpgrades;
        [JsonProperty] internal DataSlots<Item> SpellUpgrades;

        [JsonProperty] internal DataSlots<Item> HeroStatus;
        [JsonProperty] internal DataSlots<Item> HeroAbilitySeens;
        [JsonProperty] internal DataSlots<Item> HeroSelectedAbilities;

        [JsonProperty] internal DataSlots<Item> BuildingLevels;
        [JsonProperty] internal DataSlots<Item> LandingBoatLevels;
        [JsonProperty] internal DataSlots<Item> SectorBoostLevels;

        [JsonProperty] internal UnitList Units;

        [JsonProperty] internal DataSlots<Item> SectorLevels;
        [JsonProperty] internal DataSlots<Item> SectorBonuses;
        [JsonProperty] internal DataSlots<Item> ArtifactBonuses;
        [JsonProperty] internal DataSlots<Item> SectorBonusFilter;

        [JsonProperty] internal DataSlots<Item> NpcSeens;
        [JsonProperty] internal DataSlots<Item> HeroSeens;

        [JsonProperty] internal DataSlots<Item> AchievementProgresses;
        [JsonProperty] internal DataSlots<Item> BottleChainProgresses;

        [JsonProperty] internal DataSlots<Item> SubscriptionExpirationTimes;

        // Class Members

        [JsonProperty] internal Home Home;
        [JsonProperty] internal PlayerMap PlayerMap;

        // API Classes
        
        [JsonProperty] internal Facebook Facebook;
        [JsonProperty] internal Google Google;
        [JsonProperty] internal Gamecenter Gamecenter;

        // Debug Members

        [JsonProperty] internal Rank Rank = Rank.Player;

        // DateTime Members

        [JsonProperty] internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty] internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty] internal DateTime BanTime   = DateTime.UtcNow;

        // Property Members

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        /// <summary>
        /// Gets the alliance identifier.
        /// </summary>
        internal long AllianceID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Player"/> is banned.
        /// </summary>
        internal bool Banned
        {
            get
            {
                return this.BanTime > DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Player"/> is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                return this.Level?.GameMode?.Device?.Connected ?? false;
            }
        }

        /// <summary>
        /// Returns total units in slots.
        /// </summary>
        internal int UnitsTotal
        {
            get
            {
                int Count = 0;

                foreach (Item Item in this.Units)
                {
                    Count += Item.Count;
                }

                return Count;
            }
        }

        internal int UnitsTotalCapacity
        {
            get
            {
                int Count = 0;

                foreach (Item Item in this.Units)
                {
                    if (Item.Count > 0)
                    {
                        Count += ((CharacterData) Item.Data).HousingSpace * Item.Count;
                    }
                }

                return Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Home                         = new Home(this, this.HighID, this.LowID);

            this.Units                        = new UnitList(this);
            this.NpcSeens                     = new DataSlots<Item>(this);
            this.Resources                    = new ResourceSlots(this);
            this.HeroSeens                    = new DataSlots<Item>(this);
            this.HeroStatus                   = new DataSlots<Item>(this);
            this.SectorBonuses                = new DataSlots<Item>(this);
            this.SpellUpgrades                = new DataSlots<Item>(this);
            this.UnitUpgrades                 = new DataSlots<Item>(this);
            this.TrapUpgrades                 = new DataSlots<Item>(this);
            this.SectorLevels                 = new DataSlots<Item>(this);
            this.ResourceCaps                 = new DataSlots<Item>(this);
            this.HeroUpgrades                 = new DataSlots<Item>(this);
            this.BuildingLevels               = new DataSlots<Item>(this);
            this.ArtifactBonuses              = new DataSlots<Item>(this);
            this.HeroAbilitySeens             = new DataSlots<Item>(this);
            this.SectorBoostLevels            = new DataSlots<Item>(this);
            this.LandingBoatLevels            = new DataSlots<Item>(this);
            this.SectorBonusFilter            = new DataSlots<Item>(this);
            this.HeroSelectedAbilities        = new DataSlots<Item>(this);
            this.AchievementProgresses        = new DataSlots<Item>(this);
            this.BottleChainProgresses        = new DataSlots<Item>(this);
            this.SubscriptionExpirationTimes  = new DataSlots<Item>(this);
            this.ResourcesConvertedToSupplies = new DataSlots<Item>(this);

            this.MissionCompleted             = new List<int>(64);
            this.AchievementClaimed           = new List<int>(64);

            this.PlayerMap                    = new PlayerMap(this);

            this.Facebook       = new Facebook(this);
            this.Google         = new Google(this);
            this.Gamecenter     = new Gamecenter(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(Level Level, int HighID, int LowID) : this()
        {
            this.Level          = Level;
            this.HighID         = HighID;
            this.LowID          = LowID;

            this.Home.HighID    = HighID;
            this.Home.LowID     = LowID;

            this.Diamonds       = Core.Resources.GameSettings.StartingDiamonds;
            
            this.Resources.Initialize();
        }
        
        /// <summary>
        /// Encodes this instance.
        /// </summary>
        /// <param name="Packet">The byte stream.</param>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID); // AccountID
            Packet.AddLong(this.HighID, this.LowID); // ID

            if (this.AllianceID > 0)
            {
                Packet.AddBoolean(true);

                Packet.AddLong(this.AllianceHighID, this.AllianceLowID);

                Packet.AddString("TID_ALLIANCE_NAME");
                Packet.AddInt(0); // Badge
                Packet.AddInt(0); // Role
            }
            else
                Packet.AddBoolean(false);

            Packet.AddInt(this.TownHallLevel);

            Packet.AddString(this.Name);
            Packet.AddString(this.Facebook.Identifier);

            Packet.AddInt(this.ExpLevel);
            Packet.AddInt(this.ExpPoints);
            Packet.AddInt(this.Diamonds);
            Packet.AddInt(this.Diamonds);
            Packet.AddInt(this.GainIntel);
            Packet.AddInt(this.AttackKFactor);
            Packet.AddInt(this.Score);
            Packet.AddBoolean(this.NameSetByUser);
            Packet.AddInt(this.CumulativePurchasedDiamonds);
            Packet.AddInt(this.NumberOnePosCounter);

            this.ResourceCaps.Encode(Packet);
            this.Resources.Encode(Packet);
            this.ResourcesConvertedToSupplies.Encode(Packet);
            this.Units.Encode(Packet);
            this.UnitUpgrades.Encode(Packet);
            this.SpellUpgrades.Encode(Packet);
            this.HeroUpgrades.Encode(Packet);
            this.NpcSeens.Encode(Packet);
            this.TrapUpgrades.Encode(Packet);
            this.BuildingLevels.Encode(Packet);
            this.ArtifactBonuses.Encode(Packet);
            this.LandingBoatLevels.Encode(Packet);

            Packet.AddInt(0); // Unknown DataSlots

            Packet.AddInt(this.MissionCompleted.Count);

            for (int i = 0; i < this.MissionCompleted.Count; i++)
            {
                Packet.AddInt(this.MissionCompleted[i]);
            }

            Packet.AddInt(this.AchievementClaimed.Count);

            for (int i = 0; i < this.AchievementClaimed.Count; i++)
            {
                Packet.AddInt(this.AchievementClaimed[i]);
            }

            this.AchievementProgresses.Encode(Packet);
            this.BottleChainProgresses.Encode(Packet);
            this.SubscriptionExpirationTimes.Encode(Packet);
            this.HeroStatus.Encode(Packet);
            this.HeroSelectedAbilities.Encode(Packet);
            this.HeroSeens.Encode(Packet);
            this.HeroAbilitySeens.Encode(Packet);
            this.SectorLevels.Encode(Packet);
            this.SectorBoostLevels.Encode(Packet);
            this.SectorBonuses.Encode(Packet);
            this.SectorBonusFilter.Encode(Packet);

            Packet.AddInt(0); // Unknown DataSlots

            this.PlayerMap.Encode(Packet);
        }

        /// <summary>
        /// Skips specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        internal void FastForwardTime(int seconds)
        {
            this.PlayerMap.FastForwardTime(seconds);
        }

        /// <summary>
        /// Returns if the player has enough resources.
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        internal bool HasEnoughResources(ResourceBundle bundle)
        {
            if (this.Diamonds >= bundle.Resources[0].Count)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (bundle.Resources[i].Count > this.Resources.Get(bundle.Resources[i].Data)?.Count) return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns if the player has enough resources and if a worker is available.
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        internal bool HasEnoughWorkersAndResources(ResourceBundle bundle)
        {
            if (this.HasEnoughResources(bundle))
            {
                return this.Home.WorkerManager.FreeWorkers > 0;
            }

            return false;
        }
        
        /// <summary>
        /// Add points to LogicPlayer::expPoints and increase level if is available.
        /// </summary>
        /// <param name="expPoints">The experience points.</param>
        internal void XpGainHelper(int expPoints)
        {
            if (expPoints > 0)
            {
                ExperienceLevelData Data = (ExperienceLevelData) CSV.Tables.Get(Gamefile.ExperienceLevel).GetDataWithInstanceID(this.ExpLevel - 1);

                this.ExpPoints += expPoints;

                if (this.ExpPoints >= Data.ExpPoints)
                {
                    if (this.ExpPoints + expPoints >= Data.ExpPoints)
                    {
                        if (CSV.Tables.Get(Gamefile.ExperienceLevel).Datas.Count > this.ExpLevel)
                        {
                            this.ExpPoints -= Data.ExpPoints;
                            this.ExpLevel++;
                        }
                    }
                    else
                        this.ExpPoints = Data.ExpPoints;
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.Update = DateTime.UtcNow;
        }
    }
}
