namespace GL.Servers.CR.Logic.Slots
{
    using System.Collections.Generic;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Game;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Items;
    using GL.Servers.DataStream;
    using Newtonsoft.Json;

    internal class CommoditySlots
    {
        [JsonProperty] private List<DataSlot>[] Slots;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommoditySlots"/> class.
        /// </summary>
        internal CommoditySlots()
        {
            this.Slots = new List<DataSlot>[8];

            this.Slots[0] = new List<DataSlot>(16);
            this.Slots[1] = new List<DataSlot>(16);
            this.Slots[2] = new List<DataSlot>(16);
            this.Slots[3] = new List<DataSlot>(16);
            this.Slots[4] = new List<DataSlot>(16);
            this.Slots[5] = new List<DataSlot>(16);
            this.Slots[6] = new List<DataSlot>(16);
            this.Slots[7] = new List<DataSlot>(16);
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            int Count = Packet.ReadVInt();

            if (Count != 8)
            {
                Logging.Error(this.GetType(), "Invalid commodity count. Received commodity count:" + Count + ", server commodity count:" + 8);
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = Packet.ReadVInt(); j > 0; j--)
                {
                    DataSlot DataSlot = new DataSlot();
                    DataSlot.Decode(Packet);
                    this.Slots[i].Add(DataSlot);
                }
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddVInt(8);

            for (int i = 0; i < 8; i++)
            {
                Packet.AddVInt(this.Slots[i].Count);

                this.Slots[i].ForEach(Slot =>
                {
                    Slot.Encode(Packet);
                });
            }
        }

        /// <summary>
        /// Adds the commodity count.
        /// </summary>
        internal void AddCommodityCount(CommodityType Type, Data Data, int Count)
        {
            this.AddCommodityCount((int) Type, Data, Count);
        }

        /// <summary>
        /// Adds the commodity count.
        /// </summary>
        internal void AddCommodityCount(int CommodityType, Data Data, int Count)
        {
            if (CommodityType >= 8)
            {
                Logging.Error(this.GetType(), "AddCommodityCount() - Commodity Type is not valid. (" + CommodityType + ")");
                return;
            }

            DataSlot Slot = this.Slots[CommodityType].Find(T => T.Data == Data);

            if (Slot == null)
            {
                this.Slots[CommodityType].Add(new DataSlot(Data, Count));
            }
            else
                Slot.Count += Count;
        }

        /// <summary>
        /// Gets if the collection has the specified data.
        /// </summary>
        internal bool Exists(int CommodityType, Data Data)
        {
            if (CommodityType >= 8)
            {
                Logging.Error(this.GetType(), "Exists() - Commodity Type is not valid. (" + CommodityType + ")");
                return false;
            }

            return this.Slots[CommodityType].Exists(T => T.Data.Equals(Data));
        }

        /// <summary>
        /// Gets the commodity count.
        /// </summary>
        internal int GetCommodityCount(CommodityType Type, Data Data)
        {
            return this.GetCommodityCount((int) Type, Data);
        }

        /// <summary>
        /// Gets the commodity count.
        /// </summary>
        internal int GetCommodityCount(int CommodityType, Data Data)
        {
            if (CommodityType >= 8)
            {
                Logging.Error(this.GetType(), "GetCommodityCount() - Commodity Type is not valid. (" + CommodityType + ")");
                return 0;
            }

            DataSlot Slot = this.Slots[CommodityType].Find(T => T.Data == Data);

            if (Slot != null)
            {
                return Slot.Count;
            }

            return 0;
        }

        /// <summary>
        /// Sets the commodity count.
        /// </summary>
        internal void SetCommodityCount(CommodityType Type, Data Data, int Count)
        {
            this.SetCommodityCount((int) Type, Data, Count);
        }

        /// <summary>
        /// Sets the commodity count.
        /// </summary>
        internal void SetCommodityCount(int CommodityType, Data Data, int Count)
        {
            if (CommodityType >= 8)
            {
                Logging.Error(this.GetType(), "SetCommodityCount() - Commodity Type is not valid. (" + CommodityType + ")");
                return;
            }

            DataSlot Slot = this.Slots[CommodityType].Find(T => T.Data == Data);

            if (Slot == null)
            {
                this.Slots[CommodityType].Add(new DataSlot(Data, Count));
            }
            else
                Slot.Count = Count;
        }

        /// <summary>
        /// Uses the specified commodity count.
        /// </summary>
        internal void UseCommodity(CommodityType CommodityType, Data Data, int Count)
        {
            this.UseCommodity((int) CommodityType, Data, Count);
        }

        /// <summary>
        /// Uses the specified commodity count.
        /// </summary>
        internal void UseCommodity(int CommodityType, Data Data, int Count)
        {
            if (CommodityType >= 8)
            {
                Logging.Error(this.GetType(), "UseCommodity() - Commodity Type is not valid. (" + CommodityType + ")");
                return;
            }

            DataSlot Slot = this.Slots[CommodityType].Find(T => T.Data == Data);

            if (Slot != null)
            {
                Slot.Count -= Count;
            }
        }

        /// <summary>
        /// Initializes commodities.
        /// </summary>
        internal void Initialize()
        {
            // Resources
            {
                this.AddCommodityCount(CommodityType.Resource, CSV.Tables.GoldData, Globals.StartingGold);
                this.AddCommodityCount(CommodityType.Resource, CSV.Tables.FreeGoldData, Globals.StartingGold);
            }
            // ProfileResource
            {
                this.AddCommodityCount(CommodityType.ProfileResource, CSV.Tables.GetWithGlobalID(5000008), 0);
                this.AddCommodityCount(CommodityType.ProfileResource, CSV.Tables.GetWithGlobalID(5000009), 0);
                this.AddCommodityCount(CommodityType.ProfileResource, CSV.Tables.GetWithGlobalID(5000011), 0);
                this.AddCommodityCount(CommodityType.ProfileResource, CSV.Tables.GetWithGlobalID(5000027), 0);
            }
        }
    }
}