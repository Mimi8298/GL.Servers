namespace GL.Servers.CR.Logic.Items
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using Newtonsoft.Json.Linq;

    internal class DiamondShopItem : ShopItem
    {
        internal bool Free;
        internal int Amount;

        /// <summary>
        /// Gets the spell shop item type of this instance.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiamondShopItem"/> class.
        /// </summary>
        public DiamondShopItem() : base()
        {
            // DiamondShopItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiamondShopItem"/> class.
        /// </summary>
        public DiamondShopItem(int ShopIndex, int Cost, ResourceData BuyResourceData, int Amount, bool Free) : base(ShopIndex, Cost, BuyResourceData)
        {
            this.Free = Free;
            this.Amount = Amount;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.Amount = Packet.ReadVInt();
            this.Free = Packet.ReadBoolean();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteStream Packet)
        {
            base.Encode(Packet);

            Packet.AddVInt(this.Amount);
            Packet.AddBoolean(this.Free);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            base.Load(Json);

            JsonHelper.GetJsonNumber(Json, "amount", out this.Amount);
            JsonHelper.GetJsonBoolean(Json, "free", out this.Free);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override JObject Save()
        {
            JObject Json = base.Save();

            Json.Add("amount", this.Amount);
            Json.Add("free", this.Free);

            return Json;
        }
    }
}