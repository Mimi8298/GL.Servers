namespace GL.Servers.SP.Logic.Items
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Files.CSV_Helpers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class DataSlot
    {
        [JsonProperty] internal Data Data;
        [JsonProperty] internal int Count;
        [JsonProperty] internal int Count2;

        internal virtual int Checksum
        {
            get
            {
                return (this.Data != null ? this.Data.GlobalID : 0) + this.Count;
            }
        }

        public DataSlot()
        {
            
        }

        public DataSlot(Data Data, int Count)
        {
            this.Data  = Data;
            this.Count = Count;
        }

        internal virtual void Decode(Reader Reader)
        {
            this.Data  = Reader.ReadData();
            this.Count = Reader.ReadInt32();
            this.Count2 = Reader.ReadInt32();
        }

        internal virtual void Encode(ByteWriter Packet)
        {
            Packet.AddData(this.Data);
            Packet.AddInt(this.Count);
            Packet.AddInt(this.Count2);
        }

        internal virtual void Load(JToken Token)
        {
            JsonHelper.GetJsonData(Token, "id", out this.Data);
            JsonHelper.GetJsonNumber(Token, "cnt", out this.Count);
            JsonHelper.GetJsonNumber(Token, "cnt2", out this.Count2);
        }

        internal virtual JObject Save()
        {
            JObject Json = new JObject();

            if (this.Data != null)
            {
                Json.Add("id", this.Data.GlobalID);
            }

            Json.Add("cnt", this.Count);
            Json.Add("cnt2", this.Count2);

            return Json;
        }

        public override bool Equals(object obj)
        {
            if (obj is DataSlot Item)
            {
                return Item.Data == this.Data;
            }

            return false;
        }
    }
}