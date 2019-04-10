namespace GL.Servers.BB.Logic.GameObject
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using Newtonsoft.Json.Linq;

    internal class UnitStorageComponent : Component
    {
        internal CharacterData Unit;
        internal CharacterData LastUnit;

        internal int StorageType;
        internal int BoatIndex;
        internal int MaxCapacity;
        internal int UnitCount;

        /// <summary>
        /// Gets a value corresponding whether the storage contains units.
        /// </summary>
        internal bool HasUnits
        {
            get
            {
                return this.UnitCount > 0;
            }
        }

        /// <summary>
        /// Gets a value corresponding whether the storage is emptry.
        /// </summary>
        internal bool IsEmpty
        {
            get
            {
                return this.UnitCount <= 0;
            }
        }

        /// <summary>
        /// Gets a value corresponding to the used capacity. 
        /// </summary>
        internal int UsedCapacity
        {
            get
            {
                return this.Unit != null ? this.Unit.HousingSpace * this.UnitCount : 0;
            }
        }

        /// <inheritdoc />
        internal override int Type
        {
            get
            {
                return 0;
            }
        }

        /// <inheritdoc />
        public UnitStorageComponent(GameObject GameObject, int Capacity, int BoatIndex) : base(GameObject)
        {
            this.MaxCapacity = Capacity;
            this.BoatIndex = BoatIndex;
        }

        /// <summary>
        /// Adds the unit in storage.
        /// </summary>
        /// <param name="UnitData">The unit data.</param>
        /// <param name="Count">The count.</param>
        internal void AddUnit(CharacterData UnitData, int Count)
        {
            if (UnitData != null)
            {
                if (this.CanAddUnit(UnitData, Count))
                {
                    this.Unit = UnitData;
                    this.UnitCount += Count;

                    this.Parent.Home.Player.Units.AddUnits(this.BoatIndex, UnitData, Count);
                }
                else 
                    Logging.Info(this.GetType(), "AddUnit() called and storage is full.");
            }
            else
                Logging.Error(this.GetType(), "AddUnit() called with CharacterData NULL.");
        }

        /// <summary>
        /// Gets a value indicating whether you can add unit.
        /// </summary>
        /// <returns></returns>
        internal bool CanAddUnit(CharacterData Character, int Count)
        {
            if (this.Unit == null || this.Unit == Character)
            {
                return this.MaxCapacity >= this.UsedCapacity + Character.HousingSpace * Count;
            }

            return false;
        }

        /// <summary>
        /// Removes all units stored.
        /// </summary>
        internal void RemoveAllUnits()
        {
            this.Unit = null;
            this.UnitCount = 0;
        }

        /// <summary>
        /// Removes the specified unit count.
        /// </summary>
        /// <param name="Count">The count.</param>
        internal void RemoveUnits(int Count)
        {
            this.UnitCount -= Count;
            this.Parent.Home.Player.Units.RemoveUnits(this.BoatIndex, this.Unit, Count);

            if (this.UnitCount <= 0)
            {
                this.Unit = null;
                this.UnitCount = 0;
            }
        }

        /// <inheritdoc />
        internal override void Load(JToken Token)
        {
            if (JsonHelper.GetArray(Token["units"], out JArray Array))
            {
                foreach (JToken Item in Array)
                {
                    int[] Slot = Item.ToObject<int[]>();

                    if (Slot.Length == 2)
                    {
                        this.Unit = (CharacterData) CSV.Tables.Get(Gamefile.Character).GetDataWithID(Slot[0]);

                        if (this.Unit != null)
                        {
                            this.UnitCount = Slot[1];
                            break;
                        }
                    }
                }
            }

            JsonHelper.GetInt(Token["storage_type"], out this.StorageType);
            JsonHelper.GetInt(Token["boat_index"], out this.StorageType);

            JsonHelper.GetData(Token["last_unit"], out this.LastUnit);
        }

        /// <inheritdoc />
        internal override void Save(JObject Json)
        {
            if (this.Unit != null)
            {
                Json.Add("units", new JArray
                {
                    {
                        new JArray
                        {
                            this.Unit.GlobalID,
                            this.UnitCount
                        }
                    }
                });
            }

            Json.Add("storage_type", this.StorageType);
            Json.Add("boat_index", this.BoatIndex);

            if (this.LastUnit != null)
            {
                Json.Add("last_unit", this.LastUnit.GlobalID);
            }
        }
    }
}