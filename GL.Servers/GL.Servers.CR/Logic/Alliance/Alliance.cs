namespace GL.Servers.CR.Logic
{
    using GL.Servers.CR.Logic.Entries;
    using GL.Servers.CR.Logic.Slots;
    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json;

    internal class Alliance
    {
        [JsonProperty] internal int HighID;
        [JsonProperty] internal int LowID;

        [JsonProperty] internal AllianceHeaderEntry HeaderEntry;

        [JsonProperty] internal Hiring Type = Hiring.OPEN;

        [JsonProperty] internal AllianceMemberEntries Members;
        [JsonProperty] internal AllianceStreamEntries Messages;

        /// <summary>
        /// Gets the alliance id.
        /// </summary>
        internal long AllianceID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance"/> class.
        /// </summary>
        internal Alliance()
        {
            this.HeaderEntry = new AllianceHeaderEntry();
            this.Members    = new AllianceMemberEntries(this);
            this.Messages   = new AllianceStreamEntries(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance"/> class.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Alliance(int HighID, int LowID) : this()
        {
            this.HighID = HighID;
            this.LowID = LowID;
            this.HeaderEntry.HighID = HighID;
            this.HeaderEntry.LowID = LowID;
        }

        /// <summary>
        /// Called when this alliance has finished being charged.
        /// </summary>
        internal void LoadingFinished()
        {
            this.HeaderEntry.Score = 0;
            this.HeaderEntry.Donations = 0;

            int NumberOfMembers = 0;

            this.Members.ForEach(Member =>
            {
                this.HeaderEntry.Score += Member.Score;
                this.HeaderEntry.Donations += Member.Donations;

                NumberOfMembers++;
            });

            this.HeaderEntry.NumberOfMembers = NumberOfMembers;
        }
        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.HeaderEntry.HighID + "-" + this.HeaderEntry.LowID;
        }
    }
}