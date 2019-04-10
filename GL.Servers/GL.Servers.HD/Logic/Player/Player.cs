namespace GL.Servers.HD.Logic
{
    using System;

    using GL.Servers.HD.Logic.Apis;

    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json;

    internal class Player : PlayerBase
    {
        internal Device Device;

        [JsonProperty("acc_hi")]                internal int HighID;
        [JsonProperty("acc_lo")]                internal int LowID;

        [JsonProperty("alli_hi")]               internal int ClanHighID;
        [JsonProperty("alli_lo")]               internal int ClanLowID;

        [JsonProperty("battle_hi")]             internal int BattleHighID;
        [JsonProperty("battle_lo")]             internal int BattleLowID;

        [JsonProperty("name_setByUsr")]         internal bool NameSetByUser;

        [JsonProperty("token")]                 internal string Token;
        [JsonProperty("password")]              internal string Password;

        [JsonProperty("name")]                  internal string Name        = string.Empty;
        [JsonProperty("region")]                internal string Region;

        [JsonProperty("level")]                 internal int ExpLevel       = 1;
        [JsonProperty("exp")]                   internal int ExpPoints;
        [JsonProperty("diamonds")]              internal int Diamonds;
        [JsonProperty("score")]                 internal int Score;

        [JsonProperty("wins")]                  internal int Wins;
        [JsonProperty("loses")]                 internal int Loses;
        [JsonProperty("games")]                 internal int Games;

        [JsonProperty("rank")]                  internal Rank Rank          = Rank.Administrator;
        
        [JsonProperty("facebook")]              internal Facebook Facebook;
        [JsonProperty("google")]                internal Google Google;
        [JsonProperty("gamecenter")]            internal Gamecenter Gamecenter;

        [JsonProperty("update_date")]           internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty("creation_date")]         internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty("ban_date")]              internal DateTime BanTime   = DateTime.UtcNow;

        internal Time Time;

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
        /// Gets the battle identifier.
        /// </summary>
        internal long BattleID
        {
            get
            {
                return (long) this.BattleHighID << 32 | (uint) this.BattleLowID;
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
        /// Gets the checksum of this player.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Time                   = new Time();
            
            this.Facebook               = new Facebook(this);
            this.Google                 = new Google(this);
            this.Gamecenter             = new Gamecenter(this);
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
            this.LowID  = LowID;
        }


        internal void AdjustSubTick()
        {
        }

        internal void FastForward(int Seconds)
        {
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
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
