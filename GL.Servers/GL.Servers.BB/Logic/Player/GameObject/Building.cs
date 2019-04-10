namespace GL.Servers.BB.Logic.GameObject
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Extensions.Utils;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Logic.Enums;
    using Newtonsoft.Json.Linq;

    internal class Building : GameObject
    {
        internal Timer PrototypeTimer;
        internal Timer ConstructionTimer;
        internal BuildingData BuildingData;

        internal int UpgradeLevel;

        /// <summary>
        /// Gets a value indicading whether the building can be sell.
        /// </summary>
        internal bool CanSell
        {
            get
            {
                return this.BuildingData.IsArtifact() || this.BuildingData.IsPrototype();
            }
        }

        /// <summary>
        /// Gets a value indicading whether the building can be upgraded.
        /// </summary>
        internal bool CanUpgrade
        {
            get
            {
                if (this.BuildingData.GetUpgradeLevelCount() - 1 > this.UpgradeLevel)
                {
                    if (this.BuildingData.GetBuildingClassData() != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicading the remaining construction time in seconds.
        /// </summary>
        internal int RemainingConstructionTime
        {
            get
            {
                return this.ConstructionTimer != null ? this.ConstructionTimer.GetRemainingSeconds(this.Home.Time) : 0;
            }
        }

        /// <summary>
        /// Gets a value indicading the remaining construction time in ms.
        /// </summary>
        internal int RemainingConstructionTimeMS
        {
            get
            {
                return this.ConstructionTimer != null ? this.ConstructionTimer.GetRemainingMS(this.Home.Time) : 0;
            }
        }

        /// <summary>
        /// Gets a value indicading the remaining prototype time before the destruction.
        /// </summary>
        internal int RemainingPrototypeTime
        {
            get
            {
                return this.PrototypeTimer != null ? this.PrototypeTimer.GetRemainingSeconds(this.Home.Time) : 0;
            }
        }

        /// <summary>
        /// Gets a value indicading whether the building is in construction.
        /// </summary>
        internal bool IsConstructing
        {
            get
            {
                return this.ConstructionTimer != null;
            }
        }

        /// <summary>
        /// Gets a value indicading whether the building is max upgraded.
        /// </summary>
        internal bool IsMaxUpgrading
        {
            get
            {
                return this.UpgradeLevel >= this.BuildingData.GetUpgradeLevelCount();
            }
        }
        
        internal bool IsUpgrading
        {
            get
            {
                return this.RemainingConstructionTime > 0;
            }
        }

        /// <inheritdoc />
        internal override int Checksum
        {
            get
            {
                int Checksum = 0;

                // TODO Implement LogicBuilding Checksum.

                return Checksum;
            }
        }

        /// <inheritdoc />
        internal override int HeightInTiles
        {
            get
            {
                return this.BuildingData.Height;
            }
        }

        /// <inheritdoc />
        internal override int WidthInTiles
        {
            get
            {
                return this.BuildingData.Width;
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
        internal override bool IsBuilding
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        internal override bool IsStaticObject
        {
            get
            {
                return !(this.BuildingData.IsHeroBoat || this.BuildingData.IsLandingBoat() || this.BuildingData.IsDeepsea() || this.BuildingData.IsDeepsea());
            }
        }

        /// <inheritdoc />
        public Building(Data Data, Home Home) : base(Data, Home)
        {
            this.BuildingData = (BuildingData) Data;

            if (this.BuildingData.Hitpoints[0] > 0)
            {
                this.AddComponent(new HitpointComponent(this, this.BuildingData.Hitpoints[0]));
            }

            if (this.BuildingData.GetUnitProduction(0) > 0)
            {
                this.AddComponent(new UnitProductionComponent(this));
            }

            if (this.BuildingData.GetUnitStorageCapacity(0) > 0)
            {
                int BoatIndex = 0;

                if (this.BuildingData.GetUnitProduction(0) < 1)
                {
                    BoatIndex = 0xFF;
                }
                else
                {
                    foreach (Building GameObject in this.Home.GameObjectManager.GameObjects[0])
                    {
                        if (GameObject.BuildingData.GetUnitStorageCapacity(0) > 1 && GameObject.BuildingData.GetUnitProduction(0) > 0)
                        {
                            ++BoatIndex;
                        }
                    }
                }

                this.AddComponent(new UnitStorageComponent(this, this.BuildingData.GetUnitStorageCapacity(0), BoatIndex));
            }
        }

        /// <summary>
        /// Finishes the building construction.
        /// </summary>
        internal void FinishConstruction(bool force = false)
        {
            if (this.Home.Player.Level.GameMode.State == State.Home || force)
            {
                this.Home.WorkerManager.DeallocateWorker(this);
                this.ConstructionTimer = null;

                int MaxLevel = this.BuildingData.GetUpgradeLevelCount();

                if (this.UpgradeLevel >= MaxLevel - 1)
                {
                    Logging.Error(this.GetType(), "FinishConstruction() - Trying to upgrade to level that doesn't exist!");
                    this.SetUpgradeLevel(MaxLevel - 1);
                }
                else 
                    this.SetUpgradeLevel(this.UpgradeLevel + 1);

                this.Home.Player.XpGainHelper(GamePlayUtil.GetXpGain(this.BuildingData, this.UpgradeLevel));

                if (this.BuildingData.IsDeepsea())
                {
                    this.Home.Player.PlayerMap.DeepseaLevel = this.UpgradeLevel;
                }
            }
        }

        /// <summary>
        /// Sets the building upgrade level. Check before if the level is valid.
        /// </summary>
        /// <param name="Level">The level.</param>
        internal void SetUpgradeLevel(int Level)
        {
            this.UpgradeLevel = Level;

            if (this.BuildingData.IsTownHallOrCommandCenter())
            {
                this.Home.Player.TownHallLevel = Level;
            }

            HitpointComponent HitpointComponent = this.HitpointComponent;

            if (HitpointComponent != null)
            {
                HitpointComponent.SetHitpoints(this.BuildingData.Hitpoints[Level]);
                HitpointComponent.SetMaxHitpoints(this.BuildingData.Hitpoints[Level]);
            }

            UnitStorageComponent UnitStorageComponent = (UnitStorageComponent) this.GetComponent(0);

            if (UnitStorageComponent != null)
            {
                UnitStorageComponent.MaxCapacity = this.BuildingData.GetUnitProduction(Level);
            }

            // TODO Implement LogicBuilding Component Update.
        }

        /// <summary>
        /// Start the construction of the building.
        /// </summary>
        internal void StartConstructing()
        {
            if (this.ConstructionTimer != null)
            {
                this.ConstructionTimer = null;
            }

            int ConstructionTime = this.BuildingData.GetConstructionTime(this.UpgradeLevel + 1);

            if (ConstructionTime <= 0)
            {
                this.FinishConstruction();
            }
            else
            {
                this.ConstructionTimer = new Timer();
                this.ConstructionTimer.StartTimer(ConstructionTime, this.Home.Time);
                this.Home.WorkerManager.AllocateWorker(this);
            }

            if (this.BuildingData.IsPrototype())
            {
                this.PrototypeTimer = new Timer();
                this.PrototypeTimer.StartTimer(Resources.GameSettings.PrototypeTimeHours, this.Home.Time);
            }
        }

        internal override void FastForwardTime(int Seconds)
        {
            if (this.ConstructionTimer != null)
            {
                this.ConstructionTimer.FastForward(Seconds);
            }

            base.FastForwardTime(Seconds);
        }

        /// <inheritdoc />
        internal override void Load(JToken Token)
        {
            if (JsonHelper.GetInt(Token["lvl"], out int Level))
            {
                int MaxLevel = this.BuildingData.GetUpgradeLevelCount();

                if (Level >= MaxLevel)
                {
                    Logging.Error(this.GetType(), "Load() - Loaded upgrade level " + this.UpgradeLevel + " is over max! (max = " + MaxLevel + "), globalID is " + this.BuildingData.GlobalID);
                    Level = MaxLevel - 1;
                }
                else if (Level < -1)
                {
                    Logging.Error(this.GetType(), "Load() - Loaded an illegal upgrade level!");
                    Level = -1;
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Load() - Upgrade level was not found!");
            }

            if (JsonHelper.GetInt(Token["const_t"], out int ConstructionTime))
            {
                this.ConstructionTimer = new Timer();
                this.ConstructionTimer.StartTimer(ConstructionTime, this.Home.Time);
                this.Home.WorkerManager.AllocateWorker(this);
            }

            if (JsonHelper.GetInt(Token["prot_t"], out int PrototypeTime))
            {
                this.ConstructionTimer = new Timer();
                this.ConstructionTimer.StartTimer(PrototypeTime, this.Home.Time);
            }

            this.SetUpgradeLevel(Level);

            base.Load(Token);
        }

        /// <inheritdoc />
        internal override void Save(JObject Json)
        {
            Json.Add("lvl", this.UpgradeLevel);

            if (this.ConstructionTimer != null)
            {
                Json.Add("const_t", this.ConstructionTimer.GetRemainingSeconds(this.Home.Time));
            }

            if (this.PrototypeTimer != null)
            {
                Json.Add("prot_t", this.PrototypeTimer.GetRemainingSeconds(this.Home.Time));
            }

            base.Save(Json);
        }

        /// <inheritdoc />
        internal override void Tick()
        {
            base.Tick();

            if (this.IsConstructing)
            {
                if (this.RemainingConstructionTime <= 0)
                {
                    this.FinishConstruction();
                }
            }
        }
    }
}