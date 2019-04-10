namespace GL.Servers.BB.Logic.Slots.Items
{
    using System;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Enums;

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
                return (long) this.HighID << 32 | (long)(uint) this.LowID;
            }
        }

        [JsonProperty("high_id")]       internal int HighID;
        [JsonProperty("low_id")]        internal int LowID;

        [JsonProperty("username")]      internal string Username;

        [JsonProperty("donations")]     internal int Donations;
        [JsonProperty("trophies")]      internal int Trophies;
        [JsonProperty("level")]         internal int Level;
        [JsonProperty("thumbnail")]     internal int Thumbnail;
        
        [JsonProperty("joined")]        internal DateTime Joined;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Member"/> is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                return Resources.Players.ContainsKey(this.LowID);
            }
        }

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
            this.Username   = Player.Name;
        }

        /// <summary>
        /// Updates the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Update(Player Player)
        {
            this.Username   = Player.Name;
        }
    }
}