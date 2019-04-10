namespace GL.Servers.CoC.Logic.Slots
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Items;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json.Linq;

    internal class DataSlots : List<Item>
    {
        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                int Checksum = 0;

                this.ForEach(Item =>
                {
                    Checksum += Item.Checksum;
                });

                return Checksum;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSlots"/> class.
        /// </summary>
        public DataSlots(int Capacity = 50) : base(Capacity)
        {
            // DataSlots.
        }
        
        internal new void Add(Data Data, int Count)
        {
            if (this.TryGet(T => T.Data == Data, out Item Current))
            {
                Current.Count += Count;
            }
            else
                base.Add(new Item(Data, Count));
        }

        internal void Add(Item Item)
        {
            if (this.TryGet(T => T.Data == Item.Data, out Item Current))
            {
                Current.Count += Item.Count;
            }
            else
                base.Add(Item);
        }

        internal Item GetByData(Data Data)
        {
            return this.Find(T => T.Data == Data);
        }

        internal Item GetByGlobalId(int Id)
        {
            return this.Find(T => T.Data.GlobalID == Id);
        }

        internal int GetCountByData(Data Data)
        {
            return this.Find(T => T.Data == Data)?.Count ?? 0;
        }

        internal int GetCountByGlobalId(int Id)
        {
            return this.Find(T => T.Data.GlobalID == Id)?.Count ?? 0;
        }

        internal void Remove(Data Data, int Count)
        {
            if (this.TryGet(T => T.Data == Data, out Item Current))
            {
                Current.Count -= Count;
            }
#if DEBUG
            else if (Count > 0)
                Logging.Info(this.GetType(), "Remove() - DataSlot doesn't exist. This should never happen, check existing before remove. (" + Data.GlobalID + ")");
#endif
        }

        internal void Remove(Item Item, int Count)
        {
            if (this.TryGet(T => T.Data == Item.Data, out Item Current))
            {
                Current.Count -= Count;

                if (Current.Count < 0)
                {
                    Logging.Info(this.GetType(), "Remove() - Count is inferior at 0. This should never happen, check count before remove.");
                    Current.Count = 0;
                }
            }
#if DEBUG
            else if (Count > 0)
                Logging.Info(this.GetType(), "Remove() - DataSlot doesn't exist. This should never happen, check existing before remove.");
#endif
        }

        internal void Set(int Id, int Count)
        {
            this.Set(CSV.Tables.GetWithGlobalID(Id), Count);
        }

        internal void Set(Data Data, int Count)
        {
            if (this.TryGet(T => T.Data == Data, out Item Current))
            {
                Current.Count = Count;
            }
            else
                base.Add(new Item(Data, Count));
        }

        internal void Set(Item Item)
        {
            if (this.TryGet(T => T.Data == Item.Data, out Item Current))
            {
                Current.Count = Item.Count;
            }
            else
                base.Add(Item);
        }
        
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddInt(this.Count);

            this.ForEach(Item =>
            {
                Item.Encode(Packet);
            });
        }

        internal JArray Save()
        {
            JArray Array = new JArray();

            this.ForEach(Item =>
            {
                Array.Add(Item.Save());
            });

            return Array;
        }
    }
}