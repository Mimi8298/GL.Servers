namespace GL.Servers.BB.Logic.Slots
{
    using System.Linq;
    using System.Collections.Generic;

    using GL.Servers.BB.Logic.Items;
    using GL.Servers.BB.Files.CSV_Helpers;

    using GL.Servers.Extensions.List;

    internal class DataList<T> : List<T> where T : Item, new()
    {
        internal Player Player;

        /// <summary>
        /// Gets a value indicating the checksum of the <see cref="DataList{T}"/> class.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return this.Sum(T => T.Checksum);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataList{T}"/> class.
        /// </summary>
        public DataList() : base(50)
        {
            // DataList.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataList{T}"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        public DataList(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// Adds the item in dictionary.
        /// </summary>
        internal void Add(T Item)
        {
            Item Existing = this.Find(T => T.Equals(Item));

            if (Existing != null)
                Existing.Count += Item.Count;
            else
                this.Add(Item);
        }

        /// <summary>
        /// Adds the item in dictionary.
        /// </summary>
        internal void Add(Data Data, int Count)
        {
            Item Existing = this.Find(T => T.Data == Data);

            if (Existing != null)
                Existing.Count += Count;
            else
                this.Add(new T
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
            return this.Find(T => T.Data == Data);
        }

        /// <summary>
        /// Gets the specified item count. Returns 0 if item not exist.
        /// </summary>
        internal int GetCount(Data Data)
        {
            Item Existing = this.Find(T => T.Data == Data);

            if (Existing != null)
            {
                return Existing.Count;
            }

            return 0;
        }

        /// <summary>
        /// Sets the specified item. Adds item if not exist.
        /// </summary>
        internal void Set(Data Data, int Count)
        {
            Item Existing = this.Find(T => T.Data == Data);

            if (Existing != null)
                Existing.Count = Count;
            else
                this.Add(new T
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
            Item Existing = this.Find(T => T.Id == Id);

            if (Existing != null)
                Existing.Count += Count;
            else
                this.Add(new T
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
            Item Existing = this.Find(T => T.Data == Data);

            if (Existing != null)
            {
                Existing.Count -= Count;

                if (Existing.Count < 0)
                    Existing.Count = 0;
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            int Count = this.Count;

            Packet.AddInt(Count);

            for (int i = 0; i < Count; i++)
            {
                this[i].Encode(Packet);
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