namespace GL.Servers.CR.Logic.Entries
{
    using System;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using Newtonsoft.Json;

    internal class AllianceMemberEntry
    {
        private Alliance Alliance;

        [JsonProperty] private int _PlayerHighId;
        [JsonProperty] private int _PlayerLowId;
        [JsonProperty] private int _Donations;
        [JsonProperty] private int _Level;
        [JsonProperty] private int _Score;
        [JsonProperty] private int _Role;

        [JsonProperty] private string _Name;

        [JsonProperty] private ArenaData _Arena;

        /// <summary>
        /// Gets the player id.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long) this._PlayerHighId << 32 | (uint) this._PlayerLowId;
            }
        }

        /// <summary>
        /// Gets the given spell capacity.
        /// </summary>
        internal int Donations
        {
            get
            {
                return this._Donations;
            }
        }

        /// <summary>
        /// Gets the score of the player.
        /// </summary>
        internal int Score
        {
            get
            {
                return this._Score;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberEntry"/> class.
        /// </summary>
        public AllianceMemberEntry()
        {
            // AllianceMemberEntry.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberEntry"/> class.
        /// </summary>
        public AllianceMemberEntry(Alliance Alliance)
        {
            this.Alliance = Alliance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberEntry"/> class.
        /// </summary>
        public AllianceMemberEntry(Alliance Alliance, Player Player) : this(Alliance)
        {
            this.Update(Player);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        internal AllianceMemberEntry Clone()
        {
            ByteStream Stream = new ByteStream();
            AllianceMemberEntry MemberEntry = new AllianceMemberEntry(this.Alliance);

            this.Encode(Stream);
            Stream.SetOffset(0);
            MemberEntry.Decode(Stream);

            return MemberEntry;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            this._PlayerHighId = Packet.ReadVInt();
            this._PlayerLowId = Packet.ReadVInt();
            this._Name = Packet.ReadStringReference();
            this._Arena = Packet.DecodeData<ArenaData>();
            this._Role = Packet.ReadVInt();
            this._Level = Packet.ReadVInt();
            this._Score = Packet.ReadVInt();
            this._Donations = Packet.ReadVInt();

            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();
            Packet.ReadVInt();

            Packet.ReadBoolean();
            Packet.ReadBoolean();

            if (Packet.ReadBoolean())
            {
                Packet.ReadVInt(); // HomeID
                Packet.ReadVInt();
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddLogicLong(this._PlayerHighId, this._PlayerLowId);
            Packet.AddString(this._Name);
            Packet.EncodeData(this._Arena);
            Packet.AddVInt(this._Role);
            Packet.AddVInt(this._Level);
            Packet.AddVInt(this._Score);
            Packet.AddVInt(this._Donations);

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);

            Packet.AddBoolean(false);
            Packet.AddBoolean(false);

            if (true)
            {
                Packet.AddBoolean(true);
                Packet.AddLogicLong(this._PlayerHighId, this._PlayerLowId);
            }
            else
                Packet.AddBoolean(false);
        }
        
        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(Player Player)
        {
            this._PlayerHighId = Player.HighID;
            this._PlayerLowId = Player.LowID;
            this._Level = Player.ExpLevel;
            this._Arena = Player.Arena;
            this._Name = Player.Name;

            Player.SetAllianceRole(this._Role);

            this.Alliance.HeaderEntry.Score -= this._Score;
            this.Alliance.HeaderEntry.Score += this._Score = Player.Score;
        }
    }

    internal class AllianceMemberEntryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}