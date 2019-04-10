namespace GL.Servers.BB.Logic.GameObject
{
    using System;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic.Enums;

    using Newtonsoft.Json.Linq;

    using Math = GL.Servers.BB.Logic.Math;

    internal class UnitProductionComponent : Component
    {
        internal Timer ProductionTimer;
        internal CharacterData UnitData;

        internal int UnitCount;
        internal int TotalTime;

        /// <inheritdoc />
        internal override int Type
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitProductionComponent"/> class.
        /// </summary>
        public UnitProductionComponent(GameObject GameObject) : base(GameObject)
        {
            // UnitProductionComponent.
        }

        /// <summary>
        /// Adds unit to production queue.
        /// </summary>
        /// <param name="Data">The character data.</param>
        /// <param name="Count">The count.</param>
        internal void AddUnitToProductionQueue(CharacterData Data, int Count)
        {
            if (Data != null)
            {
                if (Count > 0)
                {
                    if (this.CanAddUnitToQueue(Data, Count))
                    {
                        if (this.ProductionTimer == null)
                        {
                            this.ProductionTimer = new Timer();

                            if (this.Parent.Home.Player.Level.IsTutorialOngoing)
                            {
                                this.ProductionTimer.StartTimer(1, this.Parent.Home.Time);
                            }
                            else if (false) // LogicUnitProductionComponent::isInstantTraining()
                            {
                                this.ProductionTimer.StartTimer(0, this.Parent.Home.Time);
                            }
                            else
                                this.ProductionTimer.StartTimer(Data.TrainingTime, this.Parent.Home.Time);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether can add unit to queue.
        /// </summary>
        /// <param name="Data"></param>
        internal bool CanAddUnitToQueue(CharacterData Data, int Count)
        {
            if (Data != null)
            {
                Building Building = (Building) this.Parent;

                if (!Building.IsConstructing)
                {
                    UnitStorageComponent UnitStorageComponent = (UnitStorageComponent) this.Parent.GetComponent(0);

                    if (UnitStorageComponent.MaxCapacity >= UnitStorageComponent.UsedCapacity + Data.HousingSpace * Count)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the production completed in storage and starts the next production if possible.
        /// </summary>
        internal void ProductionCompleted()
        {
            if (this.Parent.Home.Player.Level.State == State.Home || this.Parent.Home.Player.Level.State == State.Defend)
            {
                if (this.UnitCount > 0)
                {
                    UnitStorageComponent UnitStorageComponent = (UnitStorageComponent) this.Parent.GetComponent(0);

                    if (UnitStorageComponent.CanAddUnit(this.UnitData, 1))
                    {
                        UnitStorageComponent.AddUnit(this.UnitData, 1);
                        this.Parent.Home.Player.Units.Add(this.UnitData, 1);
                    }
                    else
                    {
                        CharacterData UnitData = this.UnitData;
                        int UnitCount = this.UnitCount;

                        this.UnitData = null;
                        this.UnitCount = 0;

                        this.ProductionTimer = null;

                        throw new Exception($"Bug in production completed: unit still in training!!! avatarId: {this.Parent.Home.Player.HighID}-{this.Parent.Home.Player.LowID}, count: {UnitCount} troop: {UnitData.GlobalID} gamemode: {(int) this.Parent.Home.Level.State}");
                    }
                }
            }
        }

        /// <inheritdoc />
        internal override void Load(JToken Token)
        {
            JToken UnitProduction = Token["unit_prod"];

            if (UnitProduction != null)
            {
                JArray Slots = (JArray) UnitProduction["slots"];

                if (Slots != null && Slots.Count > 0)
                {
                    if (JsonHelper.GetData(Slots[0]["id"], out Data Data) && JsonHelper.GetInt(Slots[0]["cnt"], out int Count))
                    {
                        if (Count > 0)
                        {
                            this.UnitData = (CharacterData) Data;
                            this.UnitCount = Count;
                            this.ProductionTimer = new Timer();

                            if (JsonHelper.GetInt(UnitProduction["t"], out int Time) && Time >= 0)
                            {
                                this.ProductionTimer.StartTimer(Math.Min(Time, this.UnitData.TrainingTime), this.Parent.Home.Time);
                            }
                            else
                                this.ProductionTimer.StartTimer(this.UnitData.TrainingTime, this.Parent.Home.Time);
                        }
                    }
                }
            }
        }

        internal override void Save(JObject Json)
        {
            if (this.ProductionTimer != null && this.UnitData != null)
            {
                JObject UnitProduction = new JObject();
                JArray Slots = new JArray();

                Slots.Add(new JObject
                {
                    {
                        "id", this.UnitData.GlobalID
                    },
                    {
                        "cnt", this.UnitCount
                    }
                });

                UnitProduction.Add("t", this.ProductionTimer.GetRemainingSeconds(this.Parent.Home.Time));
                UnitProduction.Add("slots", Slots);
                UnitProduction.Add("total_time", this.TotalTime);

                Json.Add("unit_prod", UnitProduction);
            }
        }

        /// <inheritdoc />
        internal override void Tick()
        {
            if (this.UnitCount > 0)
            {
                if (this.Parent.Home.Level.State == State.Home)
                {
                    bool IsTutorialOngoing = this.Parent.Home.Level.IsTutorialOngoing;
                    UnitStorageComponent UnitStorageComponent = (UnitStorageComponent) this.Parent.GetComponent(0);

                    while (this.ProductionTimer.GetRemainingSeconds(this.Parent.Home.Time) <= 0)
                    {
                        if (UnitStorageComponent.CanAddUnit(this.UnitData, 1))
                        {
                            UnitStorageComponent.AddUnit(this.UnitData, 1);
  
                            if (--this.UnitCount > 0)
                            {
                                if (IsTutorialOngoing)
                                {
                                    this.ProductionTimer.IncreaseTimer(1);
                                }
                                else if (false) // LogicUnitProductionComponent::isInstantTraining()
                                {
                                    this.ProductionTimer.IncreaseTimer(0);
                                }
                                else
                                    this.ProductionTimer.IncreaseTimer(this.UnitData.TrainingTime);
                            }
                            else
                            {
                                this.ProductionTimer = null;
                                this.UnitData = null;
                                this.UnitCount = 0;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}