namespace GL.Servers.SP.Logic.Slots
{
    using System.Collections.Generic;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Logic.Items;
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Files.CSV_Helpers;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json.Linq;

    internal class DataSlots : List<DataSlot>
    {
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
            if (this.TryGet(T => T.Data == Data, out DataSlot Current))
            {
                Current.Count += Count;
            }
            else
                base.Add(new DataSlot(Data, Count));
        }

        internal void Add(DataSlot Item)
        {
            if (this.TryGet(T => T.Data == Item.Data, out DataSlot Current))
            {
                Current.Count += Item.Count;
            }
            else
                base.Add(Item);
        }

        internal DataSlot GetByData(Data Data)
        {
            return this.Find(T => T.Data == Data);
        }

        internal DataSlot GetByGlobalId(int Id)
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
            if (this.TryGet(T => T.Data == Data, out DataSlot Current))
            {
                Current.Count -= Count;
            }
        }

        internal void Remove(DataSlot Item, int Count)
        {
            if (this.TryGet(T => T.Data == Item.Data, out DataSlot Current))
            {
                Current.Count -= Count;

                if (Current.Count < 0)
                {
                    Logging.Info(this.GetType(), "Remove() - Count is inferior at 0. This should never happen, check count before remove.");
                    Current.Count = 0;
                }
            }
            else
                Logging.Info(this.GetType(), "Remove() - DataSlot doesn't exist. This should never happen, check existing before remove.");
        }

        internal void Set(int Id, int Count)
        {
            this.Set(CSV.Tables.GetWithGlobalID(Id), Count);
        }

        internal void Set(Data Data, int Count)
        {
            if (this.TryGet(T => T.Data == Data, out DataSlot Current))
            {
                Current.Count = Count;
            }
            else
                this.Add(new DataSlot(Data, Count));
        }

        internal void Set(DataSlot Item)
        {
            if (this.TryGet(T => T.Data == Item.Data, out DataSlot Current))
            {
                Current.Count = Item.Count;
            }
            else
                this.Add(Item);
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