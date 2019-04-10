namespace GL.Servers.CR.Logic.Items
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using Newtonsoft.Json.Linq;

    internal class SpecialChestShopItem : ShopItem
    {
        internal int ChestType;
        internal TreasureChestData SpecialChestData;

        /// <summary>
        /// Gets the spell shop item type of this instance.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialChestShopItem"/> class.
        /// </summary>
        public SpecialChestShopItem() : base()
        {
            // SpecialChestShopItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellShopItem"/> class.
        /// </summary>
        public SpecialChestShopItem(int ShopIndex, int Cost, ResourceData BuyResourceData, TreasureChestData SpecialChestData, int ChestType) : base(ShopIndex, Cost, BuyResourceData)
        {
            this.ChestType = ChestType;
            this.SpecialChestData = SpecialChestData;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.SpecialChestData = Packet.DecodeData<TreasureChestData>();
            this.ChestType = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteStream Packet)
        {
            base.Encode(Packet);

            Packet.EncodeData(this.SpecialChestData);
            Packet.AddVInt(this.ChestType);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            base.Load(Json);

            JsonHelper.GetJsonData(Json, "chest", out this.SpecialChestData);
            JsonHelper.GetJsonNumber(Json, "type", out this.ChestType);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override JObject Save()
        {
            JObject Json = base.Save();

            if (this.SpecialChestData != null)
            {
                Json.Add("chest", this.SpecialChestData.GlobalID);
            }

            return Json;
        }
    }
}