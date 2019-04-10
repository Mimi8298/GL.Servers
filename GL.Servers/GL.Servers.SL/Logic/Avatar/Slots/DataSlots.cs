namespace GL.Servers.SL.Logic.Avatar.Slots
{
    using System.Collections.Generic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    using GL.Servers.SL.Logic.Avatar;
    using GL.Servers.SL.Logic.Avatar.Items;

    internal class DataSlots : Dictionary<int, Item>
    {
        internal Player Player;

        internal int Checksum
        {
            get
            {
                int Checksum = 0;

                foreach (Item Item in this.Values)
                {
                    Checksum += Item.Id + Item.Count;
                }

                return Checksum;
            }
        }

        public DataSlots(int Capacity = 50) : base(Capacity)
        {
            // DataSlots.
        }

        public DataSlots(Player Player, int Capacity = 50) : this(Capacity)
        {
            this.Player = Player;
        }
        
        internal void AddItem(int Data, int Count)
        {
            if (this.TryGetValue(Data, out Item Current))
            {
                Current.Count += Count;
            }
            else
                this.Add(Data, new Item(Data, Count));
        }

        internal void AddItem(Item Item)
        {
            if (this.TryGetValue(Item.Id, out Item Current))
            {
                Current.Count += Item.Count;
            }
            else
                this.Add(Item.Id, Item);
        }

        internal Item Get(int Data)
        {
            return this.TryGetValue(Data, out Item Item) ? Item : null;
        }

        internal int GetCount(int Data)
        {
            return this.TryGetValue(Data, out Item Item) ? Item.Count : 0;
        }

        internal void Remove(int Data, int Count)
        {
            if (this.TryGetValue(Data, out Item Current))
            {
                Current.Count -= Count;
            }
        }

        internal void Remove(Item Item, int Count)
        {
            if (this.TryGetValue(Item.Id, out Item Current))
            {
                Current.Count -= Count;
            }
        }

        internal void Set(int Data, int Count)
        {
            if (this.TryGetValue(Data, out Item Current))
            {
                Current.Count = Count;
            }
            else
                this.Add(Data, new Item(Data, Count));
        }

        internal void Set(Item Item)
        {
            if (this.TryGetValue(Item.Id, out Item Current))
            {
                Current.Count = Item.Count;
            }
            else
                this.Add(Item.Id, new Item(Item.Id, Item.Count));
        }

        internal void Decode(Reader Reader)
        {
            this.Clear();

            int Count = Reader.ReadInt32();

            if (Count > 0)
            {
                do
                {
                    Item Item = new Item();

                    Item.Decode(Reader);

                    if (Item.Id > 0)
                    {
                        this.Add(Item.Id, Item);
                    }
                } while (--Count > 0);
            }
        }

        internal void Encode(List<byte> Packet)
        {
            Packet.AddInt(this.Count);

            foreach (Item Item in this.Values)
            {
                Item.Encode(Packet);
            }
        }
    }
}