namespace GL.Servers.CoC.Logic.Manager
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using Newtonsoft.Json.Linq;

    using Random = GL.Servers.CoC.Logic.Random;

    internal class GameObjectManager
    {
        internal Level Level;

        internal List<GameObject>[][] GameObjects;

        internal Building Bunker;
        internal Building TownHall;
        internal Building TownHall2;
        internal Building Laboratory;

        internal Filter Filter;
        internal Random Random;

        internal int SecondsFromLastRespawn;
        internal int ObstacleClearCounter;

        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                int Checksum = 0;

                for (int i = 0; i < 9; i++)
                {
                    Checksum += this.GameObjects[i][0].Count;
                    Checksum += this.GameObjects[i][1].Count;
                }

                for (int i = 0; i < this.GameObjects.Length; i++)
                {
                    for (int j = 0; j < this.GameObjects[i][0].Count; j++)
                    {
                        Checksum += this.GameObjects[i][1][j].Checksum;
                    }
                    
                    for (int j = 0; j < this.GameObjects[i][1].Count; j++)
                    {
                        Checksum += this.GameObjects[i][1][j].Checksum;
                    }
                }

                return Checksum;
            }
        }
        
        internal int Map
        {
            get
            {
                if (this.Level.Player != null)
                {
                    return this.Level.Player.Map;
                }

                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectManager"/> class.
        /// </summary>
        public GameObjectManager()
        {
            this.GameObjects      = new List<GameObject>[10][];

            for (int i = 0; i < this.GameObjects.Length; i++)
            {
                this.GameObjects[i] = new List<GameObject>[2];

                for (int j = 0; j < this.GameObjects[i].Length; j++)
                {
                    this.GameObjects[i][j] = new List<GameObject>();
                }
            }
            
            this.Filter = new Filter(this);
            this.Random = new Random();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectManager"/> class.
        /// </summary>
        public GameObjectManager(Level Level) : this()
        {
            this.Level = Level;
        }

        /// <summary>
        /// Adds a gameobject in list.
        /// </summary>
        internal void AddGameObject(GameObject GameObject, int Map)
        {
            int GType = GameObject.Type;

            if (GType > 0)
            {

            }
            else
            {
                Building Building = (Building) GameObject;
                BuildingData Data = Building.BuildingData;

                if (Data.Bunker)
                {
                    this.Bunker = Building;
                }

                if (Data.IsTownHall)
                {
                    this.TownHall = Building;
                }

                if (Data.IsTownHall2)
                {
                    this.TownHall2 = Building;
                }

                if (Data.IsLaboratory)
                {
                    this.Laboratory = Building;
                }

                if (Data.IsWorker)
                {
                    this.Level.WorkerManager.WorkerCount++;
                }
            }

            GameObject.Id = GlobalID.Create(500 + GType, this.GameObjects[GType][Map].Count);

            this.GameObjects[GType][Map].Add(GameObject);
            this.Level.TileMap.AddGameObject(GameObject);
        }
        
        /// <summary>
        /// Creates a random obstacle.
        /// </summary>
        internal void CreateObstacle()
        {
            List<Data> Tables = CSV.Tables.Get(Gamefile.Obstacle).Datas;
            
            if (Tables.Count < 1)
            {
                return;
            }

            int Weight = 0;

            foreach (ObstacleData Data in Tables)
            {
                if (Data.VillageType == this.Map)
                {
                    Weight += Data.RespawnWeight;
                }
            }
            
            int RandomWeight = this.Random.Rand(Weight);

            Weight = 0;
            
            foreach (ObstacleData Data in Tables)
            {
                if (Data.VillageType == this.Map)
                {
                    Weight += Data.RespawnWeight;

                    if (Weight > RandomWeight)
                    {
                        this.RandomlyPlaceObstacle(Data);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Randomly place the obstacle.
        /// </summary>
        internal void RandomlyPlaceObstacle(ObstacleData Data)
        {
            if (Data.VillageType == this.Map)
            {
                int WidthInTiles = this.Level.WidthInTiles;
                int HeightInTiles = this.Level.HeightInTiles;

                for (int i = 0; i <= 20; i++)
                {
                    int X = this.Random.Rand(WidthInTiles + 1 - Data.Width);
                    int Y = this.Random.Rand(HeightInTiles + 1 - Data.Height);

                    if (this.Level.IsValidPlaceForObstacle(Data, X, Y, Data.Width, Data.Height, true))
                    {
                        Obstacle Obstacle = (Obstacle) GameObjectFactory.CreateGameObject(Data, this.Level);
                        Obstacle.SetPositionXY(X, Y);
                        this.AddGameObject(Obstacle, this.Map);

                        Logging.Info(this.GetType(), "X:" + X + "   Y:" + Y);

                        break;
                    }
                }
            }
            else 
                Logging.Error(this.GetType(), "RandomlyPlaceObstacle() - Trying to place obstacle in wrong village");
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForwardTime(int Secs)
        {
            for (int i = 0; i < this.GameObjects.Length; i++)
            {
                for (int j = 0; j < this.GameObjects[i][0].Count; j++)
                {
                    this.GameObjects[i][0][j].FastForwardTime(Secs);
                }

                for (int j = 0; j < this.GameObjects[i][1].Count; j++)
                {
                    this.GameObjects[i][1][j].FastForwardTime(Secs);
                }
            }

            this.SecondsFromLastRespawn += Secs;

            this.Random.Rand(1);
            this.RespawnObstacles();
        }

        /// <summary>
        /// Initializes the gameobjects.
        /// </summary>
        internal void LoadingFinished()
        {
            this.GameObjects[0][0].ForEach(GameObject =>
            {
                GameObject.LoadingFinished();
            });

            this.GameObjects[0][1].ForEach(GameObject =>
            {
                GameObject.LoadingFinished();
            });

            this.GameObjects[3][0].ForEach(GameObject =>
            {
                GameObject.LoadingFinished();
            });

            this.GameObjects[4][1].ForEach(GameObject =>
            {
                GameObject.LoadingFinished();
            });
        }

        /// <summary>
        /// Recalculates all gameobject identifiers
        /// </summary>
        internal void RecalculateIds()
        {
            for (int i = 0; i < this.GameObjects[3][0].Count; i++)
            {
                Obstacle Obstacle = (Obstacle) this.GameObjects[3][0][i];

                if (Obstacle.Destructed)
                {
                    this.GameObjects[3][0].Remove(Obstacle);
                }
            }

            for (int i = 0; i < this.GameObjects[3][1].Count; i++)
            {
                Obstacle Obstacle = (Obstacle) this.GameObjects[3][1][i];

                if (Obstacle.Destructed)
                {
                    this.GameObjects[3][1].Remove(Obstacle);
                }
            }
        }
        
        internal void RespawnObstacles()
        {
            int TombStoneCount = this.Level.TombStoneCount;

            while (this.SecondsFromLastRespawn > Globals.ObstacleRespawnSeconds)
            {
                if (this.GameObjects[3][0].Count - TombStoneCount >= Globals.ObstacleCountMax)
                {
                    this.SecondsFromLastRespawn = 0;
                    break;
                }

                this.CreateObstacle();
                this.SecondsFromLastRespawn -= Globals.ObstacleRespawnSeconds;
            }
        }

        /// <summary>
        /// Loads ths instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            #region Village 1

            JArray Buildings = (JArray) Json["buildings"];

            if (Buildings != null)
            {
                foreach (JToken Token in Buildings)
                {
                    this.LoadGameObject(Token, 0);
                }
            }
            else
                Logging.Error(this.GetType(), "An error has been throwed the load of the game objects. Building array is NULL!");

            JArray Obstacles = (JArray) Json["obstacles"];

            if (Obstacles != null)
            {
                foreach (JToken Token in Obstacles)
                {
                    this.LoadGameObject(Token, 0);
                }
            }

            JArray Traps = (JArray)Json["traps"];

            if (Traps != null)
            {
                foreach (JToken Token in Traps)
                {
                    this.LoadGameObject(Token, 0);
                }
            }

            JArray Decos = (JArray)Json["decos"];

            if (Decos != null)
            {
                foreach (JToken Token in Decos)
                {
                    this.LoadGameObject(Token, 0);
                }
            }

            #endregion
            #region Village 2

            JArray Buildings2 = (JArray) Json["buildings2"];

            if (Buildings2 == null)
            {
                Buildings2 = (JArray) Files.LevelFile.StartingHome["buildings2"];
            }

            foreach (JToken Token in Buildings2)
            {
                this.LoadGameObject(Token, 1);
            }

            JArray Obstacles2 = (JArray) Json["obstacles2"];

            if (Obstacles2 == null)
            {
                Obstacles2 = (JArray)Files.LevelFile.StartingHome["obstacles2"];
            }

            foreach (JToken Token in Obstacles2)
            {
                this.LoadGameObject(Token, 1);
            }

            JArray Traps2 = (JArray) Json["traps2"];

            if (Traps2 == null)
            {
                Traps2 = (JArray) Files.LevelFile.StartingHome["traps2"];
            }

            foreach (JToken Token in Traps2)
            {
                this.LoadGameObject(Token, 1);
            }

            JArray Decos2 = (JArray) Json["decos2"];

            if (Decos2 == null)
            {
                Decos2 = (JArray) Files.LevelFile.StartingHome["decos2"];
            }

            foreach (JToken Token in Decos2)
            {
                this.LoadGameObject(Token, 1);
            }

            #endregion

            if (JsonHelper.GetJsonObject(Json, "respawnVars", out JToken RespawnToken))
            {
                JsonHelper.GetJsonNumber(RespawnToken, "secondsFromLastRespawn", out this.SecondsFromLastRespawn);
                JsonHelper.GetJsonNumber(RespawnToken, "obstacleClearCounter", out this.ObstacleClearCounter);

                if (JsonHelper.GetJsonNumber(RespawnToken, "respawnSeed", out int RandomSeed))
                {
                    this.Random.Seed = RandomSeed;
                }
                else
                    this.Random.Seed = 112;
            }
            else
            {
                Logging.Info(this.GetType(), "Load() - Can't find respawn variables.");
                this.Random.Seed = 112;
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal void Save(JObject Json)
        {
            #region Village 1

            JArray Buildings = new JArray();

            foreach (GameObject GameObject in this.GameObjects[0][0])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Buildings.Add(Token);
                }
            }

            JArray Obstacles = new JArray();

            foreach (GameObject GameObject in this.GameObjects[3][0])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Obstacles.Add(Token);
                }
            }

            JArray Traps = new JArray();

            foreach (GameObject GameObject in this.GameObjects[4][0])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Traps.Add(Token);
                }
            }

            JArray Decos = new JArray();

            foreach (GameObject GameObject in this.GameObjects[6][0])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Decos.Add(Token);
                }
            }

            Json.Add("buildings", Buildings);
            Json.Add("obstacles", Obstacles);
            Json.Add("traps", Traps);
            Json.Add("decos", Decos);

            #endregion
            #region Village 2

            JArray Buildings2 = new JArray();

            foreach (GameObject GameObject in this.GameObjects[0][1])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Buildings2.Add(Token);
                }
            }

            JArray Obstacles2 = new JArray();

            foreach (GameObject GameObject in this.GameObjects[3][1])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Obstacles2.Add(Token);
                }
            }

            JArray Traps2 = new JArray();

            foreach (GameObject GameObject in this.GameObjects[4][1])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Traps2.Add(Token);
                }
            }

            JArray Decos2 = new JArray();

            foreach (GameObject GameObject in this.GameObjects[6][1])
            {
                JObject Token = new JObject();

                if (GameObject.Data != null)
                {
                    Token.Add("data", GameObject.Data.GlobalID);

                    GameObject.Save(Token);
                    Decos2.Add(Token);
                }
            }

            Json.Add("buildings2", Buildings2);
            Json.Add("obstacles2", Obstacles2);
            Json.Add("traps2", Traps2);
            Json.Add("decos2", Decos2);

            #endregion

            JObject RespawnVars = new JObject();

            RespawnVars.Add("secondsFromLastRespawn", this.SecondsFromLastRespawn);
            RespawnVars.Add("obstacleClearCounter", this.ObstacleClearCounter);
            RespawnVars.Add("respawnSeed", this.Random.Seed);
            RespawnVars.Add("time_to_gembox_drop", 999999999);

            Json.Add("respawnVars", RespawnVars);
        }

        /// <summary>
        /// Loads gameobject from json.
        /// </summary>
        internal void LoadGameObject(JToken Token, int Map)
        {
            if (JsonHelper.GetJsonNumber(Token, "data", out int DataID))
            {
                Data Data = CSV.Tables.GetWithGlobalID(DataID);

                if (Data == null)
                {
                    Logging.Error(this.GetType(), "An error has been throwed when the load of GameObject. data is not valid.");
                    return;
                }

                GameObject GameObject = GameObjectFactory.CreateGameObject(Data, this.Level);

                if (GameObject != null)
                {
                    GameObject.Load(Token);
                    this.AddGameObject(GameObject, Map);
                }
            }
            else Logging.Error(this.GetType(), "An error has been throwed when the load of GameObject. data not exist.");
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            for (int i = 0; i < this.GameObjects.Length; i++)
            {
                for (int j = 0; j < this.GameObjects[i][0].Count; j++)
                {
                    this.GameObjects[i][0][j].Tick();
                }

                for (int j = 0; j < this.GameObjects[i][1].Count; j++)
                {
                    this.GameObjects[i][1][j].Tick();
                }
            }
        }
    }
}