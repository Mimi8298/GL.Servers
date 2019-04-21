namespace GL.Servers.CoC.Logic
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Log;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Logic.Apis;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    
    internal class  Player : PlayerBase
    {
        internal Alliance Alliance;
        internal Member AllianceMember;

        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal int LeagueHighID;
        [JsonProperty] internal int LeagueLowID;

        [JsonProperty] internal int LogSeed;

        [JsonProperty] internal bool RedPackageState;
        [JsonProperty] internal bool NameSetByUser;
        
        [JsonProperty] internal int ChangeNameCount;

        [JsonProperty] internal string Name        = string.Empty;

        [JsonProperty("exp_level")] internal int ExpLevel       = 1;
        [JsonProperty] internal int ExpPoints;
        [JsonProperty] internal int Diamonds;
        [JsonProperty] internal int FreeDiamonds;
        [JsonProperty] internal int League;
        [JsonProperty] internal int Score;
        [JsonProperty] internal int DuelScore;
        [JsonProperty] internal int Locale;
        [JsonProperty] internal int ClanWarPreference = 1;

        [JsonProperty] internal int Wins;
        [JsonProperty] internal int Loses;
        [JsonProperty] internal int Games;

        [JsonProperty] internal Rank Rank          = Rank.Administrator;

        [JsonProperty] internal Facebook Facebook;
        [JsonProperty] internal Google Google;
        [JsonProperty] internal Gamecenter Gamecenter;

        [JsonProperty] internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty] internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty] internal DateTime BanTime   = DateTime.UtcNow;
        
        [JsonProperty] internal List<Data> Achievements;
        [JsonProperty] internal List<Data> Missions;

        [JsonProperty] internal List<AvatarStreamEntry> Logs;
        
        /// <summary>
        /// Gets a value indicating whether the player is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.Level != null)
                {
                    return this.Level.GameMode.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long)this.HighID << 32 | (uint)this.LowID;
            }
        }
        
        /// <summary>
        /// Gets the league identifier.
        /// </summary>
        internal long LeagueID
        {
            get
            {
                return (long) this.LeagueHighID << 32 | (uint) this.LeagueLowID;
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
        /// Gets a value indicating whether this player is client player.
        /// </summary>
        internal override bool ClientPlayer
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return this.ExpPoints
                       + this.ExpLevel
                       + this.Diamonds
                       + this.FreeDiamonds
                       + this.Score
                       + this.DuelScore
                       + (this.AllianceLowID > 0 ? 13 : 0)
                       + base.Checksum;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Achievements           = new List<Data>(60);
            this.Missions               = new List<Data>(30);
            this.Logs                   = new List<AvatarStreamEntry>(50);

            this.Facebook               = new Facebook(this);
            this.Google                 = new Google(this);
            this.Gamecenter             = new Gamecenter(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(Level Level, int HighID, int LowID) : this()
        {
            this.Level  = Level;
            this.HighID = HighID;
            this.LowID  = LowID;
            
            this.Diamonds = Globals.StartingDiamonds;
            this.FreeDiamonds = Globals.StartingDiamonds;

            this.Resources.Initialize();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);
            Packet.AddLong(this.HighID, this.LowID);

            if (this.InAlliance)
            {
                Packet.AddBoolean(true);

                Packet.AddLong(this.AllianceHighID, this.AllianceLowID);

                Packet.AddString(this.Alliance.Header.Name);

                Packet.AddInt(this.Alliance.Header.Badge);
                Packet.AddInt((int) this.AllianceMember.Role);
                Packet.AddInt(this.Alliance.Header.ExpLevel);
            }
            else
                Packet.AddBoolean(false);
            
            if (this.LeagueID > 0)
            {
                Packet.AddBoolean(true);
                Packet.AddLong(this.LeagueHighID, this.LeagueLowID);
            }
            else
                Packet.AddBoolean(false);

            Packet.AddInt(0);
            Packet.AddInt(0);

            {
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
            }
            {
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
                Packet.AddInt(0);
            }

            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);

            Packet.AddInt(this.League);

            Packet.AddInt(this.CastleLevel);
            Packet.AddInt(this.CastleTotalCapacity);
            Packet.AddInt(this.CastleUsedCapacity);
            Packet.AddInt(this.CastleTotalSpellCapacity);
            Packet.AddInt(this.CastleUsedSpellCapacity);
            Packet.AddInt(this.TownHallLevel);
            Packet.AddInt(this.TownHallLevel2);

            Packet.AddString(this.Name);
            Packet.AddString(this.Facebook.Identifier);

            Packet.AddInt(this.ExpLevel);
            Packet.AddInt(this.ExpPoints);

            Packet.AddInt(this.Diamonds);
            Packet.AddInt(this.FreeDiamonds);

            Packet.AddInt(60);
            Packet.AddInt(1200);

            Packet.AddInt(this.Score);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);

            if (true)
            {
                Packet.AddBoolean(true);

                Packet.AddInt(220);
                Packet.AddInt(1828055880);
            }
            else
                Packet.AddBoolean(false);

            Packet.AddBoolean(this.NameSetByUser);
            Packet.AddBoolean(false);

            Packet.AddInt(this.ChangeNameCount);
            Packet.AddInt(6900);
            Packet.AddInt(0);
            Packet.AddInt(this.ClanWarPreference);
            Packet.AddInt(0);
            Packet.AddInt(0);

            if (false)
            {
                Packet.AddBoolean(true);

                Packet.AddInt(0);
                Packet.AddLong(0);
            }
            else
                Packet.AddBoolean(false);

            this.ResourceCaps.Encode(Packet);
            this.Resources.Encode(Packet);
            this.Units.Encode(Packet);
            this.Spells.Encode(Packet);

            this.UnitUpgrades.Encode(Packet);
            this.SpellUpgrades.Encode(Packet);
            
            Packet.AddInt(0); // Hero Upgrades
            Packet.AddInt(0); // Hero Health
            Packet.AddInt(0); // Hero State
            
            this.AllianceUnits.Encode(Packet);

            Packet.AddInt(this.Missions.Count);
            this.Missions.ForEach(Packet.AddData);

            Packet.AddInt(this.Achievements.Count);
            this.Achievements.ForEach(Packet.AddData);

            this.AchievementProgress.Encode(Packet);

            this.NpcMapProgress.Encode(Packet);
            this.NpcLootedGold.Encode(Packet);
            this.NpcLootedElixir.Encode(Packet);
            
            this.Variables.Encode(Packet);

            Packet.AddInt(0); // Hero Modes
            Packet.AddInt(0); // UnitPreset1
            Packet.AddInt(0); // UnitPreset2
            Packet.AddInt(0); // UnitPreset3
            Packet.AddInt(0); // PreviousArmySize
            Packet.AddInt(0); // UnitCounterForEvent
            Packet.AddInt(0); // Units Village2
            Packet.AddInt(0); // Units Village2 new
            Packet.AddInt(0); // DataSlots
        }

        /// <summary>
        /// Adds diamonds.
        /// </summary>
        internal void AddDiamonds(int Diamonds)
        {
            this.Diamonds += Diamonds;
            this.FreeDiamonds += Diamonds;
        }

        internal void AddExperience(int ExpPoints)
        {
            ExperienceLevelData ExperienceData = (ExperienceLevelData) CSV.Tables.Get(Gamefile.ExperienceLevel).GetDataWithID(this.ExpLevel - 1);

            this.ExpPoints += ExpPoints;

            if (ExperienceData != null)
            {
                if (this.ExpPoints >= ExperienceData.ExpPoints)
                {
                    this.ExpLevel++;
                    this.ExpPoints -= ExperienceData.ExpPoints;

                    this.AddExperience(0);
                }
            }
        }

        /// <summary>
        /// Sets the alliance.
        /// </summary>
        internal void SetAlliance(Alliance Alliance, Member Member)
        {
            this.AllianceHighID = Alliance.HighID;
            this.AllianceLowID  = Alliance.LowID;
            this.Alliance       = Alliance;
            this.AllianceMember = Member;
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("name", this.Name);   

            if (this.InAlliance)
            {
                Json.Add("alliance_name", this.Alliance.Header.Name);
                Json.Add("badge_id", this.Alliance.Header.Badge);
                Json.Add("alliance_exp_level", this.Alliance.Header.ExpLevel);
            }
            else
                Json.Add("alliance_name", string.Empty);

            Json.Add("league_type", this.League);

            Json.Add("avatar_id_high", this.HighID);
            Json.Add("avatar_id_low", this.LowID);
            
            Json.Add("units", this.Units.Save());
            Json.Add("spells", this.Spells.Save());
            Json.Add("unit_upgrades", this.UnitUpgrades.Save());
            Json.Add("spell_upgrades", this.SpellUpgrades.Save());
            Json.Add("resources", this.Resources.Save());
            
            Json.Add("alliance_units", this.AllianceUnits.Save());

            Json.Add("hero_states", new JArray()); // DataSlots
            Json.Add("hero_health", new JArray());
            Json.Add("hero_modes", new JArray());
            Json.Add("variables", this.Variables.Save());
            Json.Add("units2", this.Units2.Save());

            Json.Add("castle_lvl", this.CastleLevel);

            Json.Add("castle_total", this.CastleTotalCapacity);
            Json.Add("castle_used", this.CastleUsedCapacity);
            Json.Add("castle_total_sp", this.CastleTotalSpellCapacity);
            Json.Add("castle_used_sp", this.CastleUsedSpellCapacity);

            Json.Add("town_hall_lvl", this.TownHallLevel);
            Json.Add("th_v2_lvl", this.TownHallLevel2);

            Json.Add("score", this.Score);
            Json.Add("duel_score", this.DuelScore);

            if (this.RedPackageState)
            {
                Json.Add("red_package_state", this.RedPackageState);
            }

            return Json;
        }

        /// <summary>
        /// Gets a value indicating whether has enough diamonds.
        /// </summary>
        internal bool HasEnoughDiamonds(int Count)
        {
            return this.Diamonds >= Count;
        }

        /// <summary>
        /// Uses the specified diamonds.
        /// </summary>
        internal void UseDiamonds(int Count)
        {
            this.Diamonds -= Count;
            this.FreeDiamonds -= Count;

            if (this.FreeDiamonds < 0)
            {
                this.FreeDiamonds = 0;
            }
        }
        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.HighID + "-" + this.LowID;
        }
    }
}
