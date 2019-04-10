namespace GL.Servers.BB.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BB.Logic.Slots;
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

        [JsonProperty("members")]   internal Members Members;
        [JsonProperty("messages")]  internal Messages Messages;

        /// <summary>
        /// Gets the count of donations.
        /// </summary>
        internal int Donations
        {
            get
            {
                return this.Members.Entries.Values.ToArray().Sum(Member => Member.Donations);
            }
        }

        /// <summary>
        /// Gets the total of trophies.
        /// </summary>
        internal int Trophies
        {
            get
            {
                return this.Members.Entries.Values.ToArray().Sum(Member => Member.Trophies) / 2;
            }
        }

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
            this.Members    = new Members(this);
            // this.Messages   = new Messages(this);
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