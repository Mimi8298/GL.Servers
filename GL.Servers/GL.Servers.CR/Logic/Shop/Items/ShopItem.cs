namespace GL.Servers.CR.Logic.Items
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using Newtonsoft.Json.Linq;

    internal class ShopItem
    {
        internal int Cost;
        internal int ShopIndex;

        internal ResourceData BuyResourceData;

        /// <summary>
        /// Gets the spell shop item type of this instance.
        /// </summary>
        internal virtual int Type
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopItem"/> class.
        /// </summary>
        public ShopItem()
        {
            // ShopItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopItem"/> class.
        /// </summary>
        public ShopItem(int ShopIndex, int Cost, ResourceData BuyResourceData) : this()
        {
            this.Cost = Cost;
            this.ShopIndex = ShopIndex;
            this.BuyResourceData = BuyResourceData;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(ByteStream Packet)
        {
            Packet.ReadVInt();
            this.ShopIndex = Packet.ReadVInt();
            Packet.ReadVInt();
            this.Cost = Packet.ReadVInt();
            this.BuyResourceData = Packet.DecodeData<ResourceData>();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(ByteStream Packet)
        {
            Packet.AddVInt(0);
            Packet.AddVInt(this.ShopIndex);
            Packet.AddVInt(0);
            Packet.AddVInt(this.Cost);
            Packet.EncodeData(this.BuyResourceData);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal virtual void Load(JToken Json)
        {
            if (JsonHelper.GetJsonObject(Json, "base", out JToken Base))
            {
                JsonHelper.GetJsonNumber(Base, "si", out this.ShopIndex);
                JsonHelper.GetJsonNumber(Base, "cost", out this.Cost);
                JsonHelper.GetJsonData(Base, "bd", out this.BuyResourceData);
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        /// <returns></returns>
        internal virtual JObject Save()
        {
            JObject Base = new JObject();

            Base.Add("si", this.ShopIndex);
            Base.Add("cost", this.Cost);
            Base.Add("bd", this.BuyResourceData.GlobalID);

            return new JObject
            {
                {
                    "base", Base
                }
            };
        }
    }
}