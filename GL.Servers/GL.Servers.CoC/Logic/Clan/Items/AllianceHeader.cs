namespace GL.Servers.CoC.Logic.Clan.Items
{
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;
    using Newtonsoft.Json;

    internal class AllianceHeader
    {
        internal Alliance Alliance;

        [JsonProperty] internal string Name;

        [JsonProperty] internal int Locale;

        [JsonProperty] internal int Badge;
        [JsonProperty] internal int ExpLevel  = 1;
        [JsonProperty] internal int ExpPoints;
        [JsonProperty] internal int Origin;
        [JsonProperty] internal int RequiredScore;
        [JsonProperty] internal int NumberOfMembers;

        [JsonProperty] internal int WonWarCount;
        [JsonProperty] internal int LostWarCount;
        [JsonProperty] internal int EqualWarCount;
        [JsonProperty] internal int ConsecutiveWarWinsCount;

        [JsonIgnore] internal bool PublicWarLog;
        [JsonIgnore] internal bool AmicalWar;

        [JsonIgnore] internal Hiring Type;

        internal int DuelScore
        {
            get
            {
                int Score = 0;

                foreach (Member Member in this.Alliance.Members.Slots.Values)
                {
                    Score += Member.DuelScore;
                }

                return Score;
            }
        }

        internal int Score
        {
            get
            {
                int Score = 0;

                foreach (Member Member in this.Alliance.Members.Slots.Values)
                {
                    Score += Member.Score;
                }

                return Score;
            }
        }

        public AllianceHeader()
        {
            // AllianceHeaderItem.
        }

        public AllianceHeader(Alliance Alliance)
        {
            this.Alliance = Alliance;
        }

        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.Alliance.HighID, this.Alliance.LowID);

            Packet.AddString(this.Name);

            Packet.AddInt(this.Badge);
            Packet.AddInt((int) this.Type);
            Packet.AddInt(this.NumberOfMembers);
            Packet.AddInt(this.Score);
            Packet.AddInt(this.DuelScore);
            Packet.AddInt(this.RequiredScore);

            Packet.AddInt(this.WonWarCount);
            Packet.AddInt(this.LostWarCount);
            Packet.AddInt(this.EqualWarCount);

            Packet.AddInt(this.Locale);
            Packet.AddInt(this.ConsecutiveWarWinsCount);
            Packet.AddInt(this.Origin);

            Packet.AddInt(this.ExpPoints);
            Packet.AddInt(this.ExpLevel);
            Packet.AddInt(this.ConsecutiveWarWinsCount);
            Packet.AddBoolean(this.PublicWarLog);
            Packet.AddInt(0);
            Packet.AddBoolean(this.AmicalWar);
        }
    }
}