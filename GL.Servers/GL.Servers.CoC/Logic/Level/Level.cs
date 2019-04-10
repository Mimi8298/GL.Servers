namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Map;
    using GL.Servers.CoC.Logic.Mode;
    using GL.Servers.CoC.Logic.Worker;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Logic.Mode.Enums;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Logic.Production.Manager;

    using Newtonsoft.Json.Linq;

    internal class Level
    {
        internal PlayerBase PlayerBase;
        internal PlayerBase VisitorPlayerBase;
        internal Home Home;

        internal GameMode GameMode;
        internal TileMap TileMap;

        internal WorkerManager WorkerManager;
        internal CooldownManager CooldownManager;
        internal ComponentManager ComponentManager;
        internal GameObjectManager GameObjectManager;
        internal UnitProductionManager UnitProductionManager;

        internal MissionManager MissionManager;

        internal int WidthInTiles
        {
            get
            {
                return 50;
            }
        }

        internal int HeightInTiles
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Gets a value indicating the count of tombstone on map.
        /// </summary>
        internal int TombStoneCount
        {
            get
            {
                int Count = 0;

                this.GameObjectManager.GameObjects[3][0].ForEach(GameObject =>
                {
                    Obstacle Obstacle = (Obstacle) GameObject;

                    if (Obstacle.ObstacleData.IsTombstone)
                    {
                        ++Count;
                    }
                });

                return Count;
            }
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        internal Player Player
        {
            get
            {
                if (this.PlayerBase.ClientPlayer)
                {
                    return (Player) this.PlayerBase;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the npc player.
        /// </summary>
        internal NpcPlayer NpcPlayer
        {
            get
            {
                if (this.PlayerBase.NpcPlayer)
                {
                    return (NpcPlayer) this.PlayerBase;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the visitor player.
        /// </summary>
        internal Player VisitorPlayer
        {
            get
            {
                if (this.VisitorPlayerBase.ClientPlayer)
                {
                    return (Player) this.VisitorPlayerBase;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the visitor player.
        /// </summary>
        internal NpcPlayer VisitorNpcPlayer
        {
            get
            {
                if (this.VisitorPlayerBase.NpcPlayer)
                {
                    return (NpcPlayer) this.VisitorPlayerBase;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating the game time.
        /// </summary>
        internal Time Time
        {
            get
            {
                return this.GameMode.Time;
            }
        }

        /// <summary>
        /// Gets a value indicating the state.
        /// </summary>
        internal State State
        {
            get
            {
                return this.GameMode.State;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        public Level(GameMode GameMode)
        {
            this.GameMode = GameMode;
            this.WorkerManager = new WorkerManager();
            this.CooldownManager = new CooldownManager();
            this.ComponentManager = new ComponentManager(this);
            this.GameObjectManager = new GameObjectManager(this);
            this.UnitProductionManager = new UnitProductionManager(this);

            this.MissionManager = new MissionManager(this);

            this.TileMap = new TileMap(50, 50);
        }

        /// <summary>
        /// Create a fast forward of time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            this.GameObjectManager.FastForwardTime(Seconds);
            this.CooldownManager.FastForwardTime(Seconds);
        }

        /// <summary>
        /// Initializes the managers.
        /// </summary>
        internal void LoadingFinished()
        {
            this.GameObjectManager.LoadingFinished();
            this.MissionManager.LoadingFinished();
            this.ComponentManager.RefreshResourceCaps();
        }

        /// <summary>
        /// Called after method GameMode::startDefenseState().
        /// </summary>
        internal void DefenseStateStarted(Player Attacker)
        {
            this.VisitorPlayerBase = Attacker;
            // TODO Implement DefenseStateStarted().
            this.GameMode.EndDefendState();
        }

        /// <summary>
        /// Called after method GameMode::endDefenseState().
        /// </summary>
        internal void DefenseStateEnded()
        {
            this.VisitorPlayerBase = null;
        }

        /// <summary>
        /// Sets the player.
        /// </summary>
        internal void SetPlayer(PlayerBase Player)
        {
            this.PlayerBase = Player;
            this.PlayerBase.Level = this;
        }

        /// <summary>
        /// Sets the visitor player.
        /// </summary>
        internal void SetVistorPlayer(PlayerBase Player)
        {
            this.VisitorPlayerBase = Player;
            this.VisitorPlayerBase.Level = this;
        }

        internal void SetHome(Home Home)
        {
            this.Home = Home;
            this.Home.Level = this;

            JToken Token = Home.LastSave;

            this.GameObjectManager.Load(Token);
            this.CooldownManager.Load(Token);
            this.UnitProductionManager.Load(Home.HomeJSON["units"]?["unit_prod"]);
        }

        /// <summary>
        /// Gets a value indicating whether the place is valid for a obstacle.
        /// </summary>
        internal bool IsValidPlaceForObstacle(ObstacleData Data, int X, int Y, int Width, int Height, bool Edge)
        {
            bool Valid = false;

            if (X >= 0 && Y >= 0)
            {
                if (Width + X <= 50 && Height + Y <= 50)
                {
                    if (Edge)
                    {
                        Width += 2;
                        Height += 2;
                        X--;
                        Y--;
                    }

                    Valid = true;

                    if (Width > 0 && Height > 0)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            for (int j = 0; j < Height; j++)
                            {
                                Tile Tile = this.TileMap[X + i, Y + j, Data.VillageType];

                                if (Tile != null)
                                {
                                    if (!Tile.IsBuildable())
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Valid;
        }


        /// <summary>
        /// Gets a value indicating whether the building cap is reached.
        /// </summary>
        internal bool IsBuildingCapReached(BuildingData Data)
        {
            TownhallLevelData LevelData = (TownhallLevelData) CSV.Tables.Get(Gamefile.TownHall).GetDataWithInstanceID(this.GameObjectManager.TownHall.GetUpgradeLevel());

            if (LevelData != null)
            {
                return this.GameObjectManager.Filter.GetGameObjectCount(Data, -1) >= LevelData.Caps[Data];
            }

            return false;
        }
        
        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("exp_ver", 1);

            this.GameObjectManager.Save(Json);
            this.CooldownManager.Save(Json);

            Json.Add("units", new JObject
            {
                {
                    "unit_prod",
                    this.UnitProductionManager.Save()
                }
            });
            
            return Json;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.GameObjectManager.Tick();
            this.MissionManager.Tick();

            this.CooldownManager.Update(this.Time);
        }
    }
}