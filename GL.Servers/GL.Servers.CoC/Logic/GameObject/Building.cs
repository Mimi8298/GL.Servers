namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Logic.Mode.Enums;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using Newtonsoft.Json.Linq;

    internal class Building : GameObject
    {
        private int UpgradeLevel;

        internal bool Locked;
        internal bool BoostPause;

        internal Timer BoostTimer;
        internal Timer ConstructionTimer;

        /// <summary>
        /// Gets a value indicating the building data.
        /// </summary>
        internal BuildingData BuildingData
        {
            get
            {
                return (BuildingData) this.Data;
            }
        }


        internal override int HeightInTiles
        {
            get
            {
                return this.BuildingData.Height;
            }
        }

        internal override int WidthInTiles
        {
            get
            {
                return this.BuildingData.Width;
            }
        }

        internal override int Type
        {
            get
            {
                return 0;
            }
        }

        internal override int VillageType
        {
            get
            {
                return this.BuildingData.VillageType;
            }
        }

        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal override int Checksum
        {
            get
            {
                int Checksum = 0;
                
                Checksum += base.Checksum;

                ResourceProductionComponent ResourceProductionComponent = this.ResourceProductionComponent;

                if (ResourceProductionComponent != null)
                {
                    Checksum += ResourceProductionComponent.Checksum;
                }

                ResourceStorageComponent ResourceStorageComponent = this.ResourceStorageComponent;

                if (ResourceStorageComponent != null)
                {
                    Checksum += ResourceStorageComponent.Checksum;
                }

                return Checksum;
            }
        }

        internal int RemainingConstructionTime
        {
            get
            {
                if (this.ConstructionTimer != null)
                {
                    return this.ConstructionTimer.GetRemainingSeconds(this.Level.Time);
                }

                return 0;
            }
        }

        internal bool Boosted
        {
            get
            {
                return this.BoostTimer != null;
            }
        }

        internal bool Constructing
        {
            get
            {
                return this.ConstructionTimer != null;
            }
        }
        
        internal bool UpgradeAvailable
        {
            get
            {
                if (!this.Constructing)
                {
                    BuildingData Data = this.BuildingData;

                    if (Data.MaxLevel > this.UpgradeLevel)
                    {
                        if (this.Level.Player.Map == 0)
                        {
                            return this.Level.GameObjectManager.TownHall.UpgradeLevel + 1 >= Data.TownHallLevel[this.UpgradeLevel + 1];
                        }

                        if (this.Level.Player.Map == 1)
                        {
                            return Data.TownHallLevel2[this.UpgradeLevel + 1] <= this.Level.GameObjectManager.TownHall2.UpgradeLevel + 1;
                        }
                    }
                }

                return false;
            }
        }

        internal ResourceProductionComponent ResourceProductionComponent
        {
            get
            {
                return this.TryGetComponent(5, out Component Component) ? (ResourceProductionComponent) Component : null;
            }
        }

        internal ResourceStorageComponent ResourceStorageComponent
        {
            get
            {
                return this.TryGetComponent(6, out Component Component) ? (ResourceStorageComponent) Component : null;
            }
        }

        internal UnitStorageComponent UnitStorageComponent
        {
            get
            {
                return this.TryGetComponent(0, out Component Component) ? (UnitStorageComponent) Component : null;
            }
        }

        internal BunkerComponent BunkerComponent
        {
            get
            {
                return this.TryGetComponent(7, out Component Component) ? (BunkerComponent) Component : null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        public Building(Data Data, Level Level) : base(Data, Level)
        {
            BuildingData BuildingData = this.BuildingData;

            if (!string.IsNullOrEmpty(BuildingData.ProducesResource))
            {
                this.AddComponent(new ResourceProductionComponent(this));
            }

            if (BuildingData.CanStoreResources)
            {
                this.AddComponent(new ResourceStorageComponent(this)
                {
                    MaxArray = BuildingData.GetResourceMaxArray(0)
                });
            }

            if (BuildingData.IsTrainingHousing)
            {
                this.AddComponent(new UnitStorageComponent(this));
            }

            if (BuildingData.UpgradesUnits)
            {
                this.AddComponent(new UnitUpgradeComponent(this));
            }

            if (BuildingData.Bunker)
            {
                this.AddComponent(new BunkerComponent(this));
            }
        }

        /// <summary>
        /// Finishes the construction of building.
        /// </summary>
        internal void FinishConstruction()
        {
            if (this.Level.GameMode.State == State.Home)
            {
                BuildingData Data = this.BuildingData;

                if (this.UpgradeLevel + 1 > Data.MaxLevel)
                {
                    Logging.Error(this.GetType(), "Unable to upgrade the building because the level is out of range! - " + Data.Name + ".");
                    this.SetUpgradeLevel(Data.MaxLevel);
                }
                else
                    this.SetUpgradeLevel(this.UpgradeLevel + 1);

                this.Level.WorkerManager.DeallocateWorker(this);
                this.Level.Player.AddExperience(GamePlayUtil.TimeToXp(Data.GetBuildTime(this.UpgradeLevel)));

                this.ConstructionTimer = null;
            }
        }

        /// <summary>
        /// Gets the building upgrade level.
        /// </summary>
        internal int GetUpgradeLevel()
        {
            return this.UpgradeLevel;
        }

        /// <summary>
        /// Starts upgrade of building.
        /// </summary>
        internal void StartUpgrade()
        {
            int Time = this.BuildingData.GetBuildTime(this.UpgradeLevel + 1);

            if (!this.Constructing)
            {
                this.Level.WorkerManager.AllocateWorker(this);

                if (Time <= 0)
                {
                    this.FinishConstruction();
                }
                else
                {
                    this.ResourceProductionComponent?.CollectResources();

                    this.ConstructionTimer = new Timer();
                    this.ConstructionTimer.StartTimer(this.Level.Time, Time);
                }
            }
        }

        /// <summary>
        /// Speeds up the construction.
        /// </summary>
        internal void SpeedUpConstruction()
        {
            if (this.Level.Player != null)
            {
                if (this.Constructing)
                {
                    int Cost = GamePlayUtil.GetSpeedUpCost(this.RemainingConstructionTime, this.BuildingData.VillageType, 100);

                    if (this.Level.Player.HasEnoughDiamonds(Cost))
                    {
                        this.Level.Player.UseDiamonds(Cost);
                        this.FinishConstruction();
                    }
                }
            }
        }

        /// <summary>
        /// Sets the building upgrade level.
        /// </summary>
        internal void SetUpgradeLevel(int UpgradeLevel)
        {
            this.UpgradeLevel = UpgradeLevel;
            
            ResourceProductionComponent ResourceProductionComponent = this.ResourceProductionComponent;

            if (ResourceProductionComponent != null)
            {
                ResourceProductionComponent.SetProduction();
            }

            ResourceStorageComponent ResourceStorageComponent = this.ResourceStorageComponent;

            if (ResourceStorageComponent != null)
            {
                ResourceStorageComponent.SetMaxArray(this.BuildingData.GetResourceMaxArray(UpgradeLevel));
                this.Level.ComponentManager.RefreshResourceCaps();
            }

            UnitStorageComponent UnitStorageComponent = this.UnitStorageComponent;

            if (UnitStorageComponent != null)
            {
                UnitStorageComponent.SetStorage();
            }
        }
        
        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal override void FastForwardTime(int Seconds)
        {
            if (this.Constructing)
            {
                this.ConstructionTimer.FastForward(Seconds);
            }

            if (this.Boosted)
            {
                this.BoostTimer.FastForward(Seconds);
            }

            base.FastForwardTime(Seconds);
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal override void Tick()
        {
            if (this.Constructing)
            {
                if (this.ConstructionTimer.GetRemainingSeconds(this.Level.Time) <= 0)
                {
                    this.FinishConstruction();
                }
            }

            if (this.Boosted)
            {
                if (this.BoostTimer.GetRemainingSeconds(this.Level.Time) <= 0)
                {
                    this.BoostTimer = null;
                }
            }

            base.Tick();
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            BuildingData Data = this.BuildingData;

            if (Data.Locked)
            {
                JsonHelper.GetJsonBoolean(Json, "locked", out this.Locked);
            }

            if (JsonHelper.GetJsonNumber(Json, "const_t", out int ConstructionTime))
            {
                if (ConstructionTime > -1)
                {
                    ConstructionTime = Math.Min(ConstructionTime, Data.GetBuildTime(this.UpgradeLevel + 1));

                    this.ConstructionTimer = new Timer();
                    this.ConstructionTimer.StartTimer(this.Level.Time, ConstructionTime);
                    this.Level.WorkerManager.AllocateWorker(this);
                }
            }

            if (JsonHelper.GetJsonNumber(Json, "boost_t", out int BoostTime))
            {
                if (BoostTime > -1)
                {
                    this.BoostTimer = new Timer();
                    this.BoostTimer.StartTimer(this.Level.Time, BoostTime);
                }
            }

            JsonHelper.GetJsonBoolean(Json, "boost_pause", out this.BoostPause);

            if (JsonHelper.GetJsonNumber(Json, "lvl", out int Level))
            {
                if (Level < -1)
                {
                    Logging.Error(this.GetType(), "An error has been throwed when the loading of building - Load an illegal upgrade level. Level : " + Level);
                    this.SetUpgradeLevel(0);
                }

                if (Level > Data.MaxLevel)
                {
                    Logging.Error(this.GetType(), $"An error has been throwed when the loading of building - Loaded upgrade level {Level + 1} is over max! (max = {Data.MaxLevel + 1}) id {this.Id} data id {Data.GlobalID}");
                    this.SetUpgradeLevel(Data.MaxLevel);
                }

                this.SetUpgradeLevel(Level);
            }

            base.Load(Json);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override void Save(JObject Json)
        {
            Json.Add("lvl", this.UpgradeLevel);

            if (this.Locked)
                Json.Add("locked", this.Locked);

            if (this.ConstructionTimer != null)
                Json.Add("const_t", this.ConstructionTimer.GetRemainingSeconds(this.Level.Time));

            if (this.BoostTimer != null)
                Json.Add("boost_t", this.BoostTimer.GetRemainingSeconds(this.Level.Time));

            if (this.BoostPause)
                Json.Add("boost_pause", this.BoostPause);

            base.Save(Json);
        }
    }
}