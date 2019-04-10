namespace GL.Servers.BB.Logic.Manager
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Logic.Factory;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic.GameObject;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;
    using Newtonsoft.Json.Linq;

    using GameObject = GL.Servers.BB.Logic.GameObject.GameObject;

    internal class GameObjectManager
    {
        internal Home Home;
        internal List<GameObject> ResourceShips;
        internal List<GameObject>[] GameObjects;

        internal GameObject DeployedHero;

        internal Building TownHall;
        internal Building MapRoom;
        internal Building Laboratory;
        internal Building ArtifactProduction;
        internal Building ArtifactStorage;
        internal Building Deepsea;
        internal Building GunBoat;
        internal Building HeroBoat;

        /// <summary>
        /// Returns the checksum of the <see cref="GameObjectManager"/>.
        /// </summary>
        internal int Checksum
        {
            get
            {
                int Checksum = 0;

                for (int i = 0; i < this.GameObjects.Length; i++)
                {
                    for (int j = 0; j < this.GameObjects[i].Count; j++)
                    {
                        Checksum += this.GameObjects[i][j].Checksum;
                    }
                }

                return Checksum;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectManager"/> class.
        /// </summary>
        public GameObjectManager()
        {
            this.GameObjects = new List<GameObject>[13];

            for (int i = 0; i < 13; i++)
            {
                this.GameObjects[i] = new List<GameObject>(256);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectManager"/> class.
        /// </summary>
        /// <param name="Home">The player home.</param>
        public GameObjectManager(Home Home) : this()
        {
            this.Home = Home;
        }

        /// <summary>
        /// Adds gameObject in list.
        /// </summary>
        /// <param name="GameObject">The gameObject.</param>
        internal void AddGameObject(GameObject GameObject)
        {
            if (GameObject.Type > 0)
            {
                if (GameObject.Type == 1)
                {
                    CharacterData Character = (CharacterData)GameObject.Data;

                    if (Character.IsHero())
                    {
                        if (this.DeployedHero != null)
                        {
                            Logging.Error(this.GetType(), "AddGameObject() - Added another hero! There can be only one!");
                        }

                        this.DeployedHero = GameObject;
                    }
                }
                else if (GameObject.Type == 9)
                {
                    this.ResourceShips.Add(GameObject);
                }
            }
            else
            {
                Building Building = (Building)GameObject;

                if (Building.BuildingData.IsTownHallOrCommandCenter())
                {
                    this.TownHall = Building;
                }

                if (Building.BuildingData.IsMapRoom())
                {
                    this.MapRoom = Building;
                }

                if (Building.BuildingData.UpgradesUnits)
                {
                    if (!Building.BuildingData.IsHeroBoat)
                    {
                        this.Laboratory = Building;
                    }
                }

                if (Building.BuildingData.CreatesArtifacts)
                {
                    this.ArtifactProduction = Building;
                }

                if (Building.BuildingData.StoresArtifactes())
                {
                    this.ArtifactStorage = Building;
                }

                if (Building.BuildingData.IsDeepsea())
                {
                    this.Deepsea = Building;
                }

                if (Building.BuildingData.IsGunBoat())
                {
                    this.GunBoat = Building;
                }

                if (Building.BuildingData.IsHeroBoat)
                {
                    this.HeroBoat = Building;
                }
            }

            this.GameObjects[GameObject.Type].Add(GameObject);
        }
        
        /// <summary>
        /// Creates a random obstacle.
        /// </summary>
        internal void CreateObstacle()
        {
            // TODO Implement CreateObstacle().
        }

        /// <summary>
        /// Skips the specified time.
        /// </summary>
        /// <param name="Seconds">The time in seconds.</param>
        internal void FastForwardTime(int Seconds)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < this.GameObjects[i].Count; j++)
                {
                    this.GameObjects[i][j].FastForwardTime(Seconds);
                }
            }
        }

        /// <summary>
        /// Returns a generate global id.
        /// </summary>
        /// <param name="GameObject"></param>
        /// <returns></returns>
        internal int GenerateGameObjectGlobalId(GameObject GameObject)
        {
            if (GameObject.Type < 13)
            {
                return (500 + GameObject.Type) * 1000000 + this.GameObjects[GameObject.Type].Count;
            }

            Logging.Error(this.GetType(), "GenerateGameObjectGlobalID() : Index is out of bounds.");

            return 0;
        }

        /// <summary>
        /// Returns the gameObject corresponding at the id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns>The gameObject.</returns>
        internal GameObject GetGameObjectById(int Id)
        {
            int Type = Id / 1000000 - 500;

            if (Type < 13)
            {
                int Index = Id % 1000000;

                if (this.GameObjects[Type].Count > Index)
                {
                    return this.GameObjects[Type][Index];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the max of building level.
        /// </summary>
        /// <param name="Data">The building data.</param>
        /// <returns></returns>
        internal int GetHighestBuildingLevel(BuildingData Data)
        {
            int MaxLevel = -1;

            foreach (Building Building in this.GameObjects[0])
            {
                if (Building.UpgradeLevel > MaxLevel)
                {
                    MaxLevel = Building.UpgradeLevel;
                }
            }

            return MaxLevel;
        }

        /// <summary>
        /// Returns the count of gameObjects.
        /// </summary>
        /// <returns></returns>
        internal int GetNumGameObjects()
        {
            int Count = 0;

            for (int i = 0; i < 14; i++)
            {
                Count += this.GameObjects[i].Count;
            }

            return Count;
        }

        /// <summary>
        /// To call after initialization or after deserialization.
        /// </summary>
        internal void LoadingFinished()
        {
            foreach (GameObject GameObject in this.GameObjects[0])
            {
                GameObject.LoadingFinished();
            }

            foreach (GameObject GameObject in this.GameObjects[4])
            {
                GameObject.LoadingFinished();
            }

            foreach (GameObject GameObject in this.GameObjects[10])
            {
                GameObject.LoadingFinished();
            }
        }

        /// <summary>
        /// Loads this instance from JSON.
        /// </summary>
        /// <param name="Token">The Json object.</param>
        internal void Load(JToken Token)
        {
            if (JsonHelper.GetArray(Token["buildings"], out JArray Buildings))
            {
                this.LoadGameObjectsFromJsonArray(Buildings);
            }
            else
            {
                Logging.Error(this.GetType(), "Load - Building array is NULL!");
                throw new Exception("Load - Building array is NULL!");
            }

            if (JsonHelper.GetArray(Token["obstacles"], out JArray Obstacles))
            {
                this.LoadGameObjectsFromJsonArray(Obstacles);
            }

            if (JsonHelper.GetArray(Token["traps"], out JArray Traps))
            {
                this.LoadGameObjectsFromJsonArray(Traps);
            }

            if (JsonHelper.GetArray(Token["decos"], out JArray Decos))
            {
                this.LoadGameObjectsFromJsonArray(Decos);
            }

            if (JsonHelper.GetArray(Token["resource_ships"], out JArray ResourceShips))
            {
                this.LoadGameObjectsFromJsonArray(ResourceShips);
            }

            if (this.GunBoat == null)
            {
                this.AddGameObject(new Building(CSV.Tables.Get(Gamefile.Building).GetData("Gunboat"), this.Home)
                {
                    X = 58 << 9,
                    Y = 38 << 9
                });
            }

            if (this.HeroBoat == null)
            {
                this.AddGameObject(new Building(CSV.Tables.Get(Gamefile.Building).GetData("Hero Boat"), this.Home)
                {
                    X = 54 << 9,
                    Y = 8 << 9
                });
            }
        }

        /// <summary>
        /// Saves this instance to JSON.
        /// </summary>
        /// <param name="Json">The Json object.</param>
        internal void Save(JObject Json)
        {
            JArray Buildings = new JArray();

            foreach (Building GameObject in this.GameObjects[0])
            {
                JObject Item = new JObject();

                Item.Add("data", GameObject.Data.GlobalID);
                GameObject.Save(Item);

                Buildings.Add(Item);
            }

            Json.Add("buildings", Buildings);

            JArray Obstacles = new JArray();

            foreach (GameObject GameObject in this.GameObjects[3])
            {
                if (!GameObject.ShouldDestruct)
                {
                    JObject Item = new JObject();

                    Item.Add("data", GameObject.Data.GlobalID);
                    GameObject.Save(Item);

                    Obstacles.Add(Item);
                }
            }

            Json.Add("obstacles", Obstacles);

            JArray Traps = new JArray();

            foreach (GameObject GameObject in this.GameObjects[4])
            {
                JObject Item = new JObject();

                Item.Add("data", GameObject.Data.GlobalID);
                GameObject.Save(Item);

                Traps.Add(Item);
            }

            Json.Add("traps", Traps);


            JArray Decos = new JArray();

            foreach (GameObject GameObject in this.GameObjects[6])
            {
                JObject Item = new JObject();

                Item.Add("data", GameObject.Data.GlobalID);
                GameObject.Save(Item);

                Decos.Add(Item);
            }

            Json.Add("decos", Decos);

            JArray ResourceShips = new JArray();

            foreach (GameObject GameObject in this.GameObjects[9])
            {
                JObject Item = new JObject();

                Item.Add("data", GameObject.Data.GlobalID);
                GameObject.Save(Item);

                ResourceShips.Add(Item);
            }

            Json.Add("resource_ships", ResourceShips);
        }

        /// <summary>
        /// Loads gameObjects from Json array (<see cref="JArray"/>).
        /// </summary>
        /// <param name="Array">The Json array.</param>
        internal void LoadGameObjectsFromJsonArray(JArray Array)
        {
            foreach (JToken Token in Array)
            {
                if (JsonHelper.GetInt(Token["data"], out int Id))
                {
                    Data Data = CSV.Tables.GetDataById(Id);

                    if (Data != null)
                    {
                        GameObject GameObject = GameObjectFactory.CreateGameObject(Data, this.Home);

                        if (GameObject != null)
                        {
                            GameObject.Load(Token);
                            this.AddGameObject(GameObject);
                        }
                    }
                    else
                        Logging.Error(this.GetType(), "Load - Data is NULL for Global ID:" + Id);
                }
                else
                    Logging.Error(this.GetType(), "Load - Data id was not found!");
            }
        }

        /// <summary>
        /// Places the obstacle to the random position generated by <see cref="Logic.Random"/> class.
        /// </summary>
        /// <param name="Data"></param>
        internal void RandomlyPlaceObstacle(ObstacleData Data)
        {
            // TODO Implement RandomlyPlaceObstacle().
        }

        /// <summary>
        /// Removes the specified gameObject to the array.
        /// </summary>
        /// <param name="GameObject">The gameObject.</param>
        internal void RemoveGameObject(GameObject gameObject)
        {
            if (this.GameObjects[gameObject.Type].Remove(gameObject))
            {
                if (this.TownHall == gameObject)
                {
                    this.TownHall = null;
                }

                if (this.MapRoom == gameObject)
                {
                    this.MapRoom = null;
                }

                if (this.Laboratory == gameObject)
                {
                    this.Laboratory = null;
                }

                if (this.ArtifactProduction == gameObject)
                {
                    this.ArtifactProduction = null;
                }

                if (this.ArtifactStorage == gameObject)
                {
                    this.ArtifactStorage = null;
                }

                if (this.Deepsea == gameObject)
                {
                    this.Deepsea = null;
                }

                if (this.GunBoat == gameObject)
                {
                    this.GunBoat = null;
                }

                if (this.HeroBoat == gameObject)
                {
                    this.HeroBoat = null;
                }
            }
        }

        /// <summary>
        /// Respawn obstacles.
        /// </summary>
        internal void RespawnObstacles()
        {
            // TODO Implement RespawnObstacles().
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < this.GameObjects[i].Count; j++)
                {
                    this.GameObjects[i][j].Tick();
                }
            }
        }
    }
}