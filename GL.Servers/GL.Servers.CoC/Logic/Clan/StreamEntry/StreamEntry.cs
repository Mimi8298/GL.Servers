namespace GL.Servers.CoC.Logic.Clan.StreamEntry
{
    using System;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Extensions.Helper;

    using GL.Servers.CoC.Logic.Clan.Enums;
    using GL.Servers.CoC.Logic.Clan.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(StreamEntryConverter))]
    internal class StreamEntry
    {
        internal int HighId;
        internal int LowId;
        
        internal int SenderHighId;
        internal int SenderLowId;
        internal int SenderLevel;
        internal int SenderLeague;

        internal string SenderName;

        internal Role SenderRole;

        internal DateTime Created = DateTime.UtcNow;

        internal int Age
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(this.Created).TotalSeconds;
            }
        }

        internal long StreamID
        {
            get
            {
                return (long) this.HighId | (uint) this.LowId;
            }
        }

        internal virtual StreamType StreamType
        {
            get
            {
                return 0;
            }
        }

        public StreamEntry()
        {
            // StreamEntry.
        }

        public StreamEntry(Member Member)
        {
            this.SenderHighId = Member.HighID;
            this.SenderLowId  = Member.LowID;

            this.SenderName   = Member.Name;

            this.SenderLevel  = Member.ExpLevel;
            this.SenderRole   = Member.Role;
            this.SenderLeague = Member.League;
        }
        
        internal virtual void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighId, this.LowId);

            Packet.AddBoolean(this.SenderLowId > 0);
            Packet.AddBoolean(this.SenderLowId > 0); // Home
            Packet.AddBoolean(false); // ?

            if (this.SenderLowId > 0)
            {
                Packet.AddLong(this.SenderHighId, this.SenderLowId);
            }

            if (this.SenderLowId > 0)
            {
                Packet.AddLong(this.SenderHighId, this.SenderLowId); // Home
            }

            Packet.AddString(this.SenderName);

            Packet.AddInt(this.SenderLevel);
            Packet.AddInt(this.SenderLeague);
            Packet.AddInt((int) this.SenderRole);

            Packet.AddInt(this.Age);
        }

        internal virtual void Load(JToken Json)
        {
            JToken Base = Json["base"];

            if (Base != null)
            {
                JsonHelper.GetJsonNumber(Base, "high_id", out this.HighId);
                JsonHelper.GetJsonNumber(Base, "low_id", out this.LowId);

                JsonHelper.GetJsonNumber(Base, "sender_high_id", out this.SenderHighId);
                JsonHelper.GetJsonNumber(Base, "sender_low_id", out this.SenderLowId);
                JsonHelper.GetJsonNumber(Base, "sender_lvl", out this.SenderLevel);

                JsonHelper.GetJsonString(Base, "sender_name", out this.SenderName);

                if (this.SenderName == null)
                {
                    Logging.Error(this.GetType(), "Load() - SenderName is NULL.");
                    this.SenderName = string.Empty;
                }

                JsonHelper.GetJsonNumber(Base, "sender_role", out int Role);
                JsonHelper.GetJsonDateTime(Base, "date", out this.Created);

                this.SenderRole = (Role) Role;
            }
            else 
                Logging.Error(this.GetType(), "Json doesn't contains base object!");
        }

        internal virtual JObject Save()
        {
            JObject Json = new JObject();
            JObject Base = new JObject();

            Base.Add("high_id", this.HighId);
            Base.Add("low_id", this.LowId);
            
            Base.Add("sender_high_id", this.SenderHighId);
            Base.Add("sender_low_id", this.SenderLowId);
            Base.Add("sender_lvl", this.SenderLevel);

            Base.Add("sender_name", this.SenderName);

            Base.Add("sender_role", (int) this.SenderRole);
            
            Base.Add("date", this.Created);

            Json.Add("type", (int) this.StreamType);
            Json.Add("base", Base);

            return Json;
        }
    }

    internal class StreamEntryConverter : JsonConverter
    {
        public override bool CanConvert(Type Type)
        {
            return Type.BaseType == typeof(StreamEntry) || Type == typeof(StreamEntry);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject Token = JObject.Load(reader);

            if (JsonHelper.GetJsonNumber(Token, "type", out int Type))
            {
                StreamEntry Entry;

                switch (Type)
                {
                    case 2:
                        Entry = new ChatStreamEntry();
                        break;
                    default:
                        Entry = new StreamEntry();
                        break;
                }

                Entry.Load(Token);

                return Entry;
            }
            else
                Logging.Info(this.GetType(), "ReadJson() - JsonObject doesn't contains 'type' key.");

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            StreamEntry StreamEntry = (StreamEntry) value;

            if (StreamEntry != null)
            {
                StreamEntry.Save().WriteTo(writer);
            }
            else
                writer.WriteNull();
        }
    }
}