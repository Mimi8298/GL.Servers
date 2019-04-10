namespace GL.Servers.CR.Logic.Items
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using Newtonsoft.Json.Linq;

    internal class SpellShopItem : ShopItem
    {
        internal int Amount;
        internal int RarityIndex;
        internal SpellData SpellData;

        /// <summary>
        /// Gets the spell shop item type of this instance.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellShopItem"/> class.
        /// </summary>
        public SpellShopItem() : base()
        {
            // SpellShopItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellShopItem"/> class.
        /// </summary>
        public SpellShopItem(int ShopIndex, int Cost, ResourceData BuyResourceData, SpellData SpellData, int Amount, int RarityIndex) : base(ShopIndex, Cost, BuyResourceData)
        {
            this.Amount = Amount;
            this.RarityIndex = RarityIndex;
            this.SpellData = SpellData;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.SpellData   = Packet.DecodeData<SpellData>();
            this.Amount      = Packet.ReadVInt();
            this.RarityIndex = Packet.ReadVInt();
            Packet.ReadBoolean();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteStream Packet)
        {
            base.Encode(Packet);

            Packet.EncodeData(this.SpellData);
            Packet.AddVInt(this.Amount);
            Packet.AddVInt(this.RarityIndex);
            Packet.AddBoolean(false);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            base.Load(Json);
            
            JsonHelper.GetJsonData(Json, "spell", out this.SpellData);
            JsonHelper.GetJsonNumber(Json, "amount", out this.Amount);
            JsonHelper.GetJsonNumber(Json, "rarity", out this.RarityIndex);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override JObject Save()
        {
            JObject Json = base.Save();

            if (this.SpellData != null)
            {
                Json.Add("spell", this.SpellData.GlobalID);
            }

            Json.Add("amount", this.Amount);
            Json.Add("rarity", this.RarityIndex);

            return Json;
        }
    }
}