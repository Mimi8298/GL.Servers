namespace GL.Servers.SP.Logic
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.SP.Logic.Mode;
    using GL.Servers.SP.Logic.Slots;
    using GL.Servers.SP.Extensions.Game;
    using GL.Servers.SP.Logic.Slots.Items;
  
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Files.CSV_Helpers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class  Player
    {
        internal bool IsInitialized;
        internal GameMode GameMode;

        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal int AllianceHighID;
        [JsonProperty] internal int AllianceLowID;

        [JsonProperty] internal int LogSeed;

        [JsonProperty] internal bool NameSetByUser;

        [JsonProperty] internal string PassToken;
        
        [JsonProperty] internal string Name        = string.Empty;
        [JsonProperty] internal string LoginCountry;

        [JsonProperty] internal int ExpLevel       = 1;
        [JsonProperty] internal int ExpPoints;
        [JsonProperty] internal int Diamonds;
        [JsonProperty] internal int FreeDiamonds;
        [JsonProperty] internal int Score;
        
        [JsonProperty] internal Rank Rank          = Rank.Administrator;

        [JsonProperty] internal Facebook Facebook;
        [JsonProperty] internal Google Google;
        [JsonProperty] internal Gamecenter Gamecenter;

        [JsonProperty] internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty] internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty] internal DateTime BanTime   = DateTime.UtcNow;

        [JsonProperty] internal List<Data> AchievementsClaimed;

        [JsonProperty] internal ResourceSlots Resources;     
        [JsonProperty] internal DataSlots AchievementProgresses;

        [JsonProperty] internal Home Home;

        internal int Energy
        {
            get
            {
                return this.Resources.GetCountByGlobalId(9000001);
            }
            set
            {
                this.Resources.Set(9000001, value);
            }
        }
        
        internal bool ChangeNameOnGoing;
        
        /// <summary>
        /// Gets a value indicating whether the player is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.GameMode != null)
                {
                    return this.GameMode.Connected;
                }

                return false;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the player has the max of energy.
        /// </summary>
        internal bool HasMaxEnergy
        {
            get
            {
                return this.Energy >= Globals.MaxEnergy;
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
        internal long AllianceID
        {
            get
            {
                return (long) this.AllianceHighID << 32 | (uint) this.AllianceLowID;
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
        
        internal int Checksum
        {
            get
            {
                int Checksum = 0;

                Checksum += this.Resources.Checksum;
                Checksum += 0; // LevelScores
                Checksum += 0; // LevelArenaScores

                Checksum += this.Diamonds + (this.Diamonds >> 32) + 0;

                return Checksum;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.AchievementsClaimed    = new List<Data>(50);

            this.Resources              = new ResourceSlots();
            this.AchievementProgresses  = new DataSlots();

            this.Facebook               = new Facebook(this);
            this.Google                 = new Google(this);
            this.Gamecenter             = new Gamecenter(this);
            
            this.Home                   = new Home(this);

            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(GameMode GameMode, int HighID, int LowID) : this()
        {
            this.GameMode = GameMode;
            this.HighID = HighID;
            this.LowID  = LowID;

            this.Home.HighID = HighID;
            this.Home.LowID = LowID;
        }

        /// <summary>
        /// Initializes the variables of this instance.
        /// </summary>
        internal void Initialize()
        {
            if (this.IsInitialized)
            {
                this.Resources.Clear();
                this.AchievementsClaimed.Clear();
                this.AchievementProgresses.Clear();
            }

            this.SetDiamonds(Globals.StartingDiamonds);
            this.Resources.Initialize();

            this.IsInitialized = true;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);

            Packet.AddString(this.Name);
            Packet.AddString(this.Facebook.Identifier);

            Packet.AddInt(0); // lastPlayedLevel
            Packet.AddInt(this.Diamonds);
            Packet.AddInt(this.FreeDiamonds);
            Packet.AddBoolean(this.NameSetByUser);
            Packet.AddInt(this.Score);
            
            this.Resources.Encode(Packet);
            
            Packet.AddInt(this.AchievementsClaimed.Count);
            this.AchievementsClaimed.ForEach(Packet.AddData);

            this.AchievementProgresses.Encode(Packet);

            Packet.AddInt(0); // LevelScores (List<DataSlots>)
            Packet.AddInt(0); // LevelAreas (List<DataSlots>)
        }
        
        /// <summary>
        /// Adds diamonds.
        /// </summary>
        internal void AddDiamonds(int Count)
        {
            this.Diamonds += Count;
            this.FreeDiamonds += Count;
        }

        /// <summary>
        /// Gets a value indicating whether has enough diamonds.
        /// </summary>
        internal bool HasEnoughDiamonds(int Count)
        {
            return this.Diamonds >= Count;
        }

        /// <summary>
        /// Sets the diamonds.
        /// </summary>
        internal void SetDiamonds(int Count)
        {
            this.Diamonds = Count;
            this.FreeDiamonds = Count;
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
        /// Saves this instance to json. 
        /// </summary>
        /// <returns></returns>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("resourcecount");
            Json.Add("diamonds");
            Json.Add("levelscore");
            Json.Add("zonescore");

            return Json;
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
