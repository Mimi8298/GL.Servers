namespace GL.Servers.CoC.Logic
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;

    using Newtonsoft.Json.Linq;

    internal class GameObject
    {
        internal Data Data;
        internal Level Level;
        internal Vector2 Position;

        internal int Id;
        
        internal int TileX
        {
            get
            {
                return this.Position.X >> 9;
            }
        }

        internal int TileY
        {
            get
            {
                return this.Position.Y >> 9;
            }
        }

        internal int MidX
        {
            get
            {
                return this.Position.X >> 8;
            }
        }

        internal int MidY
        {
            get
            {
                return this.Position.Y >> 8;
            }
        }
        
        internal virtual int Checksum
        {
            get
            {
                int Checksum = 0;

                Checksum += this.Type;

                return Checksum;
            }
        }

        internal virtual int HeightInTiles
        {
            get
            {
                return 1;
            }
        }

        internal virtual int WidthInTiles
        {
            get
            {
                return 1;
            }
        }

        internal virtual int VillageType
        {
            get
            {
                return 0;
            }
        }

        internal virtual int Type
        {
            get
            {
                return 0;
            }
        }

        internal List<Component> Components;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject(Data Data, Level Level)
        {
            this.Level = Level;
            this.Data  = Data;
            this.Position = new Vector2(0, 0);
            this.Components = new List<Component>(16);
        }

        /// <summary>
        /// Adds component to the gameobject.
        /// </summary>
        internal void AddComponent(Component Component)
        {
            if (this.Components.Contains(Component))
            {
                Logging.Error(this.GetType(), "AddComponent() : Trying to add a component already added. Type : " + Component.Type + ".");
                return;
            }
            
            this.Components.Add(Component);
            this.Level.ComponentManager.AddComponent(Component);
        }

        /// <summary>
        /// Sets position of the gameobject.
        /// </summary>
        internal void SetPositionXY(int TileX, int TileY)
        {
            int OldX = this.Position.X >> 9;
            int OldY = this.Position.Y >> 9;

            if (OldX == TileX && OldY == TileX)
            {
                return;
            }

            this.Position.X = TileX << 9;
            this.Position.Y = TileY << 9;

            this.Level.TileMap.GameObjectMoved(this, OldX, OldY);
        }
        
        /// <summary>
        /// Try gets a component.
        /// </summary>
        internal bool TryGetComponent(int Type, out Component Component)
        {
            Component = this.Components.Find(T => T.Type == Type);
            return Component != null;
        }
        
        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal virtual void FastForwardTime(int Secs)
        {
            this.Components.ForEach(Component =>
            {
                Component.FastForwardTime(Secs);
            });
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal virtual void Tick()
        {
            this.Components.ForEach(Component =>
            {
                Component.Tick();
            });
        }

        internal virtual void LoadingFinished()
        {
            // LoadingFinished.
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal virtual void Load(JToken Json)
        {
            if (JsonHelper.GetJsonNumber(Json, "x", out int X) && JsonHelper.GetJsonNumber(Json, "y", out int Y))
            {
                this.Position.X = X << 9;
                this.Position.Y = Y << 9;
            }
            else
                Logging.Error(this.GetType(), "An error has been throwed when the load of game object. Position X or Y is null!");

            this.Components.ForEach(Component =>
            {
                Component.Load(Json);
            });
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal virtual void Save(JObject Json)
        {
            Json.Add("x", this.TileX);
            Json.Add("y", this.TileY);

            this.Components.ForEach(Component =>
            {
                Component.Save(Json);
            });
        }
    }
}