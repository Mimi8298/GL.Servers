namespace GL.Servers.BB.Logic.GameObject
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Helpers;
    using Newtonsoft.Json.Linq;

    internal class GameObject
    {
        internal Data Data;
        internal Home Home;

        internal int X;
        internal int Y;
        
        internal int MidX
        {
            get
            {
                return this.X >> 8;
            }
        }
        
        internal int MidY
        {
            get
            {
                return this.Y >> 8;
            }
        }

        /// <summary>
        /// Gets a value indicating the 'x' position of the tile.
        /// </summary>
        internal int TileX
        {
            get
            {
                return this.X >> 9;
            }
        }

        /// <summary>
        /// Gets a value indicating the 'y' position of the tile.
        /// </summary>
        internal int TileY
        {
            get
            {
                return this.Y >> 9;
            }
        }

        /// <summary>
        /// Gets a value corresponding of Hitpoint Component.
        /// </summary>
        internal HitpointComponent HitpointComponent
        {
            get
            {
                return (HitpointComponent) this.GetComponent(2);
            }
        }
        
        /// <summary>
        /// Gets a value indicating the ckecksum of the <see cref="GameObject"/>.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating the type of the <see cref="GameObject"/>.
        /// </summary>
        internal virtual int Type
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating the height in tiles.
        /// </summary>
        internal virtual int HeightInTiles
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets a value indicating the width in tiles.
        /// </summary>
        internal virtual int WidthInTiles
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject is a building.
        /// </summary>
        internal virtual bool IsBuilding
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject is passable.
        /// </summary>
        internal virtual bool IsPassable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject is a static object.
        /// </summary>
        internal virtual bool IsStaticObject
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject is unbuildable.
        /// </summary>
        internal virtual bool IsUnbuildable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject should destruct.
        /// </summary>
        internal virtual bool ShouldDestruct
        {
            get
            {
                return false;
            }
        }

        internal Dictionary<int, Component> Components;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="Home">The player home.</param>
        public GameObject(Data Data, Home Home)
        {
            this.Data = Data;
            this.Home = Home;
            this.Components = new Dictionary<int, Component>(25);
        }

        /// <summary>
        /// Adds the specified component. Check before if is not already added.
        /// </summary>
        /// <param name="Component">The component.</param>
        internal void AddComponent(Component Component)
        {
            if (this.Components.ContainsKey(Component.Type))
            {
                Logging.Error(this.GetType(), "AddComponent() - Component is already added.");
                return;
            }

            this.Components.Add(Component.Type, Component);
        }

        /// <summary>
        /// Gets the specified component. Returns null if not exist.
        /// </summary>
        /// <param name="Type">The component type.</param>
        /// <returns>The component.</returns>
        internal Component GetComponent(int Type)
        {
            if (this.Components.TryGetValue(Type, out Component Component))
            {
                return Component;
            }

            return null;
        }

        /// <summary>
        /// Sets the position XY.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        internal void SetPositionXY(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Skips the specified time.
        /// </summary>
        /// <param name="Seconds">The time in seconds.</param>
        internal virtual void FastForwardTime(int Seconds)
        {
            // FastForwardTime.
        }

        /// <summary>
        /// Called after the loading.
        /// </summary>
        internal virtual void LoadingFinished()
        {

        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        /// <param name="Token">The token.</param>
        internal virtual void Load(JToken Token)
        {
            if (!JsonHelper.GetInt(Token["x"], out this.X) || !JsonHelper.GetInt(Token["y"], out this.Y))
            {
                Logging.Error(this.GetType(), "Load() - x or y is null!");
                return;
            }

            this.X <<= 9;
            this.Y <<= 9;

            foreach (Component Component in this.Components.Values)
            {
                Component.Load(Token);
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        /// <param name="Json">The json.</param>
        internal virtual void Save(JObject Json)
        {
            Json.Add("x", this.TileX);
            Json.Add("y", this.TileY);

            foreach (Component Component in this.Components.Values)
            {
                Component.Save(Json);
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal virtual void Tick()
        {
            // Tick.
        }
    }
}