namespace GL.Servers.CoC.Logic.Clan.Items
{
    using System;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json;

    internal class Member
    {
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

        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal string Name;

        [JsonProperty] internal int ExpLevel;
        [JsonProperty] internal int Score;
        [JsonProperty] internal int DuelScore;
        [JsonProperty] internal int Donations;
        [JsonProperty] internal int League;
        [JsonProperty] internal int TroopReceived;
        [JsonProperty] internal int TroopSended;

        [JsonProperty] internal Role Role;
        [JsonProperty] internal DateTime Joined;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// </summary>
        internal Member()
        {
            this.Joined     = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Member(Player Player) : this()
        {
            this.HighID     = Player.HighID;
            this.LowID      = Player.LowID;
            this.Name       = Player.Name;
            this.Score      = Player.Score;
            this.DuelScore  = Player.DuelScore;
            this.ExpLevel   = Player.ExpLevel;
            this.League     = Player.League;
        }

        /// <summary>
        /// Updates the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Update(Player Player)
        {
            this.Name       = Player.Name;
            this.Score      = Player.Score;
            this.DuelScore  = Player.DuelScore;
            this.ExpLevel   = Player.ExpLevel;
            this.League     = Player.League;
        }

        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);

            Packet.AddString(this.Name);

            Packet.AddInt((int) this.Role);
            Packet.AddInt(this.ExpLevel);
            Packet.AddInt(this.League);
            Packet.AddInt(this.Score);
            Packet.AddInt(this.DuelScore);
            Packet.AddInt(this.TroopSended);
            Packet.AddInt(this.TroopReceived);
            Packet.AddInt(6);
            Packet.AddInt(0);
            Packet.AddInt(42);
            Packet.AddInt(0);
            Packet.AddInt(957071);
            Packet.AddInt(0);
            Packet.AddInt(1);

            Packet.AddBoolean(true);
            {
                Packet.AddLong(this.HighID, this.LowID);
            }
        }
    }
}