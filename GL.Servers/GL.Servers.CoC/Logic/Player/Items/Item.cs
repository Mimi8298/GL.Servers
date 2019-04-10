namespace GL.Servers.CoC.Logic.Items
{
    using GL.Servers.CoC.Extensions;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Extensions.Helper;

    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Item
    {
        [JsonProperty] internal Data Data;
        [JsonProperty] internal int Count;
        
        internal virtual int Checksum
        {
            get
            {
                return (this.Data != null ? this.Data.GlobalID : 0) + this.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item(Data Data, int Count)
        {
            this.Data  = Data;
            this.Count = Count;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(Reader Reader)
        {
            this.Data  = Reader.ReadData();
            this.Count = Reader.ReadInt32();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(ByteWriter Packet)
        {
            Packet.AddData(this.Data);
            Packet.AddInt(this.Count);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        /// <param name="Token"></param>
        internal virtual void Load(JToken Token)
        {
            JsonHelper.GetJsonData(Token, "id", out this.Data);
            JsonHelper.GetJsonNumber(Token, "cnt", out this.Count);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal virtual JObject Save()
        {
            JObject Json = new JObject();

            if (this.Data != null)
            {
                Json.Add("id", this.Data.GlobalID);
            }

            Json.Add("cnt", this.Count);

            return Json;
        }

        public static Item operator +(Item Item, Item Item2)
        {
            if (Item.Data == Item2.Data)
            {
                return new Item(Item.Data, Item.Count + Item2.Count);
            }

            return null;
        }

        public static Item operator -(Item Item, Item Item2)
        {
            if (Item.Data == Item2.Data)
            {
                return new Item(Item.Data, Item.Count + Item2.Count);
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Item Item)
            {
                return Item.Data == this.Data;
            }

            return false;
        }
    }
}