namespace GL.Servers.CoC.Logic.Clan
{
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.CoC.Logic.Clan.Slots;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Alliance
    {
        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal Members Members;
        [JsonProperty] internal Streams Streams;
        [JsonProperty] internal AllianceHeader Header;

        [JsonProperty] internal string Description;

        /// <summary>
        /// Gets the clan identifier.
        /// </summary>
        internal long AllianceID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan"/> class.
        /// </summary>
        internal Alliance()
        {
            this.Header = new AllianceHeader(this);
            this.Members = new Members(this);
            this.Streams = new Streams(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan"/> class.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Alliance(int HighID, int LowID) : this()
        {
            this.HighID     = HighID;
            this.LowID      = LowID;
        }

        internal void Encode(ByteWriter Packet)
        {
            this.Header.Encode(Packet);

            Packet.AddString(this.Description);
            Packet.AddInt(0);
            Packet.AddBoolean(false);
            Packet.AddInt(0);
            Packet.AddBoolean(false);
            
            this.Members.Encode(Packet);

            Packet.AddInt(0);
            Packet.AddInt(52);
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