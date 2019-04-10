namespace GL.Servers.SL.Logic
{
    using System.Collections.Generic;

    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json;

    internal class Clan
    {
        [JsonProperty("high_id")]   internal int HighID;
        [JsonProperty("low_id")]    internal int LowID;

        [JsonProperty("r_trophi")]  internal int Required_Trophies;

        [JsonProperty("origin")]    internal int Origin;
        [JsonProperty("badge")]     internal int Badge;

        [JsonProperty("name")]      internal string Name;
        [JsonProperty("desc")]      internal string Description;

        [JsonProperty("type")]      internal Hiring Type = Hiring.OPEN;

        /// <summary>
        /// Gets the clan identifier.
        /// </summary>
        internal long ClanID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan"/> class.
        /// </summary>
        internal Clan()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan"/> class.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Clan(int HighID, int LowID) : this()
        {
            this.HighID     = HighID;
            this.LowID      = LowID;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                return Packet.ToArray();
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