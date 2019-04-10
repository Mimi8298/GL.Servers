namespace GL.Servers.CR.Logic
{
    using System.Collections.Generic;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Logic.Items;
    using GL.Servers.DataStream;

    internal class ShopCycle
    {
        internal List<ShopItem> Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopCycle"/> class.
        /// </summary>
        public ShopCycle()
        {
            this.Items = new List<ShopItem>(8);

#if DEBUG
            this.Items.Add(new DiamondShopItem(0,0, CSV.Tables.GoldData, 100000, false));
            this.Items.Add(new SpellShopItem(1, 0, CSV.Tables.GoldData, CSV.Tables.Spells[0], 1000, 1));
            this.Items.Add(new SpellShopItem(1, 0, CSV.Tables.GoldData, CSV.Tables.Spells[1], 1000, 1));
#endif
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            for (int i = Packet.ReadVInt(); i > 0; i--)
            {
                ShopItem ShopItem = ShopItemFactory.CreateShopItem(Packet.ReadVInt());

                if (ShopItem != null)
                {
                    ShopItem.Decode(Packet);
                    this.Items.Add(ShopItem);
                }
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddVInt(this.Items.Count);

            this.Items.ForEach(ShopItem =>
            {
                Packet.AddVInt(ShopItem.Type);
                ShopItem.Encode(Packet);
            });
        }
    }
}