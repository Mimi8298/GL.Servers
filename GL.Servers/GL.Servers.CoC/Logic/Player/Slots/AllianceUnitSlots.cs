namespace GL.Servers.CoC.Logic.Slots
{
    using System.Collections.Generic;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Logic.Items;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json.Linq;

    internal class AllianceUnitSlots : List<UnitItem>
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
        /// Initializes a new instance of the <see cref="AllianceUnitSlots"/> class.
        /// </summary>
        public AllianceUnitSlots(int Capacity = 50) : base(Capacity)
        {
            // AllianceUnitSlots.
        }

        internal new void Add(Data Data, int Count, int Level)
        {
            if (this.TryGet(T => T.Data == Data && T.Level == Level, out UnitItem Current))
            {
                Current.Count += Count;
            }
            else
                base.Add(new UnitItem(Data, Count, Level));
        }

        internal void Add(UnitItem Item)
        {
            if (this.TryGet(T => T.Data == Item.Data && T.Level == Item.Level, out UnitItem Current))
            {
                Current.Count += Item.Count;
            }
            else
                base.Add(Item);
        }

        internal Item GetByData(Data Data, int Level)
        {
            return this.Find(T => T.Data == Data && T.Level == Level);
        }

        internal Item GetByGlobalId(int Id, int Level)
        {
            return this.Find(T => T.Data.GlobalID == Id && T.Level == Level);
        }

        internal int GetCountByData(Data Data, int Level)
        {
            return this.Find(T => T.Data == Data && T.Level == Level)?.Count ?? 0;
        }

        internal int GetCountByGlobalId(int Id, int Level)
        {
            return this.Find(T => T.Data.GlobalID == Id && T.Level == Level)?.Count ?? 0;
        }

        internal void Remove(Data Data, int Count, int Level)
        {
            if (this.TryGet(T => T.Data == Data && T.Level == Level, out UnitItem Current))
            {
                Current.Count -= Count;
            }
        }

        internal void Remove(Item Item, int Count, int Level)
        {
            if (this.TryGet(T => T.Data == Item.Data && T.Level == Level, out UnitItem Current))
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

        internal void Set(int Id, int Count, int Level)
        {
            this.Set(CSV.Tables.GetWithGlobalID(Id), Count, Level);
        }

        internal void Set(Data Data, int Count, int Level)
        {
            if (this.TryGet(T => T.Data == Data && T.Level == Level, out UnitItem Current))
            {
                Current.Count = Count;
            }
            else
                this.Add(new UnitItem(Data, Count, Level));
        }

        internal void Set(UnitItem Item)
        {
            if (this.TryGet(T => T.Data == Item.Data && T.Level == Item.Level, out UnitItem Current))
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