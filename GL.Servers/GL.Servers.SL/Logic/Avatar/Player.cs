  namespace GL.Servers.SL.Logic.Avatar
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.SL.Logic.Enums;
    using GL.Servers.SL.Logic.Slots.Items;
    using GL.Servers.SL.Logic.Avatar.Items;
    using GL.Servers.SL.Logic.Avatar.Slots;

    using GL.Servers.SL.Core;
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Extensions.Game;
    using GL.Servers.SL.Files.CSV_Logic.Logic;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Player
    {
        internal Device Device;

        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal int ClanHighID;
        [JsonProperty] internal int ClanLowID;

        [JsonProperty] internal bool NameSetByUser;

        [JsonProperty] internal string Token;
        [JsonProperty] internal string Password;

        [JsonProperty] internal string Name = string.Empty;
        [JsonProperty] internal string Region;

        [JsonProperty] internal int ExpLevel = 1;
        [JsonProperty] internal int ExpPoints;
        [JsonProperty] internal int Diamonds;
        [JsonProperty] internal int Score;

        [JsonProperty] internal Rank Rank = Rank.Administrator;

        [JsonProperty] internal Facebook Facebook;
        [JsonProperty] internal Google Google;
        [JsonProperty] internal Gamecenter Gamecenter;

        [JsonProperty] internal DateTime Update = DateTime.UtcNow;
        [JsonProperty] internal DateTime Created = DateTime.UtcNow;
        [JsonProperty] internal DateTime BanTime = DateTime.UtcNow;

        [JsonProperty] internal int OngoingQuestData;

        [JsonProperty] internal List<int> Achievements;

        [JsonProperty] internal DataSlots AchievementProgress;
        [JsonProperty] internal DataSlots ItemInventories;
        [JsonProperty] internal ResourceSlots Resources;
        [JsonProperty] internal DataSlots Spells;
        [JsonProperty] internal HeroLevelSlots HeroLevels;
        [JsonProperty] internal DataSlots NpcProgress;
        [JsonProperty] internal DataSlots Variables;
        [JsonProperty] internal DataSlots EnergyPackages;
        [JsonProperty] internal DataSlots HeroUnlockSeens;
        [JsonProperty] internal DataSlots QuestUnlockSeens;
        [JsonProperty] internal DataSlots Extras;

        [JsonProperty] internal HeroUpgrade HeroUpgrade;

        [JsonProperty] internal Timer EnergyTimer;

        internal Time Time;

        internal int Energy
        {
            get
            {
                return this.Resources.GetCount(2000002);
            }
            set
            {
                this.Resources.Set(2000002, value);
            }
        }

        internal int Gold
        {
            get
            {
                return this.Resources.GetCount(2000001);
            }
            set
            {
                this.Resources.Set(2000001, value);
            }
        }

        internal int Orb1
        {
            get
            {
                return this.Resources.GetCount(2000003);
            }
            set
            {
                this.Resources.Set(2000003, value);
            }
        }

        internal int Orb2
        {
            get
            {
                return this.Resources.GetCount(2000003);
            }
            set
            {
                this.Resources.Set(2000003, value);
            }
        }

        internal int Orb3
        {
            get
            {
                return this.Resources.GetCount(2000003);
            }
            set
            {
                this.Resources.Set(2000003, value);
            }
        }

        internal int Orb4
        {
            get
            {
                return this.Resources.GetCount(2000003);
            }
            set
            {
                this.Resources.Set(2000003, value);
            }
        }

        internal int MaxEnergy
        {
            get
            {
                int Count = 15;

                foreach (Item Item in this.EnergyPackages.Values)
                {
                    EnergyPackageData Package = (EnergyPackageData) Item.Data;

                    for (int j = 0; j < Item.Count; j++)
                    {
                        Count += Package.IncreaseInMaxEnergy[j];
                    }
                }

                return Count;
            }
        }

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
        /// Gets the clan identifier.
        /// </summary>
        internal long ClanID
        {
            get
            {
                return (long) this.ClanHighID << 32 | (uint) this.ClanLowID;
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
        /// Gets the player's clan.
        /// </summary>
        internal Clan Clan
        {
            get
            {
                return Core.Resources.Clans.Get(this.ClanHighID, this.ClanLowID, Store: false);
            }
        }

        internal int Checksum
        {
            get
            {
                return this.ExpLevel
                       + this.ExpPoints
                       + this.Diamonds
                       + this.Diamonds
                       + this.Score
                       + this.Resources.Checksum
                       + this.AchievementProgress.Checksum
                       + this.Spells.Checksum
                       + this.HeroLevels.Checksum
                       + this.NpcProgress.Checksum;
            }
        }

        internal JObject JSON
        {
            get
            {
                JObject Json = new JObject();
                JArray Team = new JArray();

                Json.Add("prev_spells", new JArray());
                Json.Add("troop_req_msg", "");
                Json.Add("team", Team);
                Json.Add("flicks_out_char", 0);
                Json.Add("fast_flicks", 0);
                Json.Add("ongoing_quest_data", this.OngoingQuestData);
                Json.Add("ongoing_level_idx", 0);
                Json.Add("ongoing_event_data", 0);
                //Json.Add("IgnoreOngoingQuest", new );
                Json.Add("shareC", 0);
                Json.Add("mailC", 0);
                Json.Add("challengeC", 0);
                Json.Add("troop_req_t", 0);
                Json.Add("ship_lvl", 0);
                Json.Add("launched_event", 0);
                Json.Add("league", 0); // InChecksum
                Json.Add("help_opened", true);

                JObject Energy = new JObject();

                Energy.Add("enemyK", 0); // InChecksum
                Energy.Add("given", 0);
                Energy.Add("time", this.EnergyTimer.RemainingSecs);

                Json.Add("energyVars", Energy);
                Json.Add("map_chest", 0); // InChecksum

                this.HeroUpgrade.Save(Json);

                return Json;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Time = new Time();

            this.EnergyTimer = new Timer(this.Time);

            this.Achievements = new List<int>(50);

            this.AchievementProgress = new DataSlots(this);
            this.ItemInventories = new DataSlots(this);
            this.Resources = new ResourceSlots(this);
            this.Spells = new DataSlots(this);
            this.HeroLevels = new HeroLevelSlots(this);
            this.NpcProgress = new DataSlots(this);
            this.Variables = new DataSlots(this);
            this.EnergyPackages = new DataSlots(this, 2);
            this.HeroUnlockSeens = new DataSlots(this);
            this.QuestUnlockSeens = new DataSlots(this);
            this.Extras = new DataSlots(this);

            this.Facebook = new Facebook(this);
            this.Google = new Google(this);
            this.Gamecenter = new Gamecenter(this);

            this.HeroUpgrade = new HeroUpgrade(this);

            // Initialize.

            this.Diamonds = GameSettings.StartingDiamonds;
            this.OngoingQuestData = GameSettings.StartingQuest.GlobalID;

            this.HeroLevels.Initialize();
            this.Resources.Initialize();

            // DEBUG

            this.Resources.Set(2000003, 1000);
            this.Resources.Set(2000004, 1000);
            this.Resources.Set(2000005, 1000);

            foreach (QuestData Data in CSV.Tables.Get(Gamefile.Quests).Datas)
            {
                if (Data.QuestType == "Unlock")
                {
                    this.NpcProgress.AddItem(Data.GlobalID, 1);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(Device Device, int HighID, int LowID) : this()
        {
            this.Device = Device;
            this.HighID = HighID;
            this.LowID = LowID;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                // sub_19AAC4
                {
                    Packet.AddInt(0);
                }

                Packet.AddInt(this.HighID);
                Packet.AddInt(this.LowID);

                Packet.AddString(this.Name);
                Packet.AddString(string.Empty);

                Packet.AddInt(this.Diamonds);
                Packet.AddInt(this.Diamonds);
                Packet.AddInt(this.ExpLevel); // ExpLvl
                Packet.AddInt(this.ExpPoints); // ExpPoints
                Packet.AddInt(this.Score);
                Packet.AddInt(0);

                Packet.AddBools(this.ClanID > 0, this.NameSetByUser);

                if (this.ClanID > 0)
                {
                    Packet.AddLong(this.ClanID);
                    Packet.AddString("Alliance Name");
                    Packet.AddInt(0);
                    Packet.AddInt(0);
                }

                Packet.AddInt(0);

                this.Resources.Encode(Packet);

                Packet.AddInt(this.Achievements.Count);

                for (int i = 0; i < this.Achievements.Count; i++)
                {
                    Packet.AddInt(this.Achievements[i]);
                }

                Packet.AddInt(0); // Achievement XP Reward

                this.AchievementProgress.Encode(Packet);
                this.ItemInventories.Encode(Packet);
                this.NpcProgress.Encode(Packet);
                this.QuestUnlockSeens.Encode(Packet);

                Packet.AddInt(0); // Quest Unlocks seen

                this.HeroLevels.Encode(Packet);

                Packet.AddInt(0); // Player Hero Kills
                Packet.AddInt(0); // Player Hero Quests
                Packet.AddInt(0); // Player Hero Matches

                this.HeroUnlockSeens.Encode(Packet);

                Packet.AddInt(0); // Player Sailing
                Packet.AddInt(0); // Player Sailing Levels

                this.Extras.Encode(Packet);
                this.EnergyPackages.Encode(Packet);

                Packet.AddInt(0); // EventState

                this.Variables.Encode(Packet);

                Packet.AddInt(0); // Borrowed Out Hero
                Packet.AddInt(0); // Borrowed In Hero
                Packet.AddInt(0); // Quest Moves

                this.Spells.Encode(Packet);

                Packet.AddInt(0); // SpellsReadyCount
                Packet.AddInt(0); // HeroTired
                Packet.AddInt(0); // ItemLevel
                Packet.AddInt(0); // Item Attached
                Packet.AddInt(0); // Item UnavailTimer

                Packet.AddInt(0);

                Packet.AddCompressableString(this.JSON.ToString());

                return Packet.ToArray();
            }
        }

        internal void AdjustSubTick()
        {
            this.EnergyTimer.AdjustSubTick();
            this.HeroUpgrade.AdjustSubTick();
        }

        internal void FastForward(int Seconds)
        {
            this.EnergyTimer.FastForward(Seconds);
            this.HeroUpgrade.FastForward(Seconds);
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (!this.EnergyTimer.Started)
            {
                if (this.Energy < this.MaxEnergy)
                {
                    this.EnergyTimer.StartTimer(300);
                }
            }
            else
            {
                if (this.EnergyTimer.Started)
                {
                    while (this.EnergyTimer.RemainingSecs <= 0)
                    {
                        if (++this.Energy < this.MaxEnergy)
                        {
                            this.EnergyTimer.StartTimer(300);
                        }
                        else this.EnergyTimer.StopTimer();
                    }
                }
            }

            this.HeroUpgrade.Tick();

            Logging.Info(this.GetType(), "Energy Timer : Started:" + this.EnergyTimer.Started + ", RemainingSecs:" + this.EnergyTimer.RemainingSecs + ".");

            this.Update = DateTime.UtcNow;
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
