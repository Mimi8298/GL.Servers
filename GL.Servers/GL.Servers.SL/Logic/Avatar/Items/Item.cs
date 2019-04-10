namespace GL.Servers.SL.Logic.Avatar.Items
{
    using System.Collections.Generic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Files.CSV_Helpers;
    using Newtonsoft.Json;

    internal class Item
    {
        [JsonProperty("id")]  internal int Id;
        [JsonProperty("cnt")] internal int Count;

        internal Data Data
        {
            get
            {
                return CSV.Tables.GetWithGlobalID(this.Id);
            }
        }

        public Item()
        {
            
        }

        public Item(int Data, int Count)
        {
            this.Id    = Data;
            this.Count = Count;
        }

        internal void Decode(Reader Reader)
        {
            this.Id    = Reader.ReadInt32();
            this.Count = Reader.ReadInt32();
        }

        internal void Encode(List<byte> Packet)
        {
            Packet.AddInt(this.Id);
            Packet.AddInt(this.Count);
        }
    }
}