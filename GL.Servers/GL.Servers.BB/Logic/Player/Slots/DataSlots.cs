namespace GL.Servers.BB.Logic.Slots
{
    using System.Linq;
    using System.Collections.Generic;

    using GL.Servers.BB.Logic.Items;
    using GL.Servers.BB.Files.CSV_Helpers;

    using GL.Servers.Extensions.List;

    internal class DataSlots<T> : Dictionary<int, T> where T : Item, new()
    {
        internal Player Player;

        /// <summary>
        /// Returns the checksum of the <see cref="DataSlots{T}"/> class.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return this.Values.Sum(T => T.Checksum);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSlots{T}"/> class.
        /// </summary>
        public DataSlots() : base(50)
        {
            // DataSlots.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSlots{T}"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        public DataSlots(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// Adds the item in dictionary.
        /// </summary>
        internal void Add(T Item)
        {
            if (this.TryGetValue(Item.Data.GlobalID, out T Existing))
                Existing.Count += Item.Count;
            else 
                this.Add(Item.Data.GlobalID, Item);
        }

        /// <summary>
        /// Adds the item in dictionary.
        /// </summary>
        internal void Add(Data Data, int Count)
        {
            if (this.TryGetValue(Data.GlobalID, out T Item))
                Item.Count += Count;
            else
                this.Add(Data.GlobalID, new T
                {
                    Data = Data,
                    Count = Count
                });
        }

        /// <summary>
        /// Gets the specified item. Returns null if item not exist.
        /// </summary>
        internal Item Get(Data Data)
        {
            if (this.TryGetValue(Data.GlobalID, out T Item))
            {
                return Item;
            }

            return null;
        }

        /// <summary>
        /// Gets the specified item count. Returns 0 if item not exist.
        /// </summary>
        internal int GetCount(Data Data)
        {
            if (this.TryGetValue(Data.GlobalID, out T Item))
            {
                return Item.Count;
            }

            return 0;
        }

        /// <summary>
        /// Sets the specified item. Adds item if not exist.
        /// </summary>
        internal void Set(Data Data, int Count)
        {
            if (this.TryGetValue(Data.GlobalID, out T Item))
                Item.Count = Count;
            else
                this.Add(Data.GlobalID, new T
                {
                    Data = Data,
                    Count = Count
                });
        }

        /// <summary>
        /// Sets the specified item. Adds item if not exist.
        /// </summary>
        internal void Set(int Id, int Count)
        {
            if (this.TryGetValue(Id, out T Item))
                Item.Count = Count;
            else
                this.Add(Id, new T
                {
                    Id = Id,
                    Count = Count
                });
        }

        /// <summary>
        /// Decreases to item the specified count.
        /// </summary>
        internal void Remove(Data Data, int Count)
        {
            if (this.TryGetValue(Data.GlobalID, out T Item))
            {
                Item.Count -= Count;

                if (Item.Count < 0)
                    Item.Count = 0;
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            T[] Items = this.Values.ToArray();
            int Count = Items.Length;

            Packet.AddInt(Count);

            if (Count > 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    Items[i].Encode(Packet);
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal virtual void Initialize()
        {
            // Initialize.
        }
    }
}