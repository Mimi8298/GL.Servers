namespace GL.Servers.CoC.Logic.Map
{
    using System.Collections.Generic;

    internal class Tile
    {
        internal List<GameObject> GameObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        public Tile()
        {
            this.GameObjects = new List<GameObject>(4);
        }

        /// <summary>
        /// Adds a gameobject on the tile.
        /// </summary>
        internal void AddGameObject(GameObject GameObject)
        {
            if (this.GameObjects.Contains(GameObject))
            {
                return;
            }

            this.GameObjects.Add(GameObject);
        }

        /// <summary>
        /// Gets a value indicating whether the tile is buildable.
        /// </summary>
        internal bool IsBuildable()
        {
            return this.GameObjects.Count == 0;
        }

        /// <summary>
        /// Gets a value indicating whether the tile is buildable.
        /// </summary>
        internal bool IsBuildable(GameObject GameObject)
        {
            if (this.GameObjects.Count > 0)
            {
                if (this.GameObjects.Count == 1)
                {
                    return this.GameObjects[0] == GameObject;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes the gameobject of the tile.
        /// </summary>
        internal void RemoveGameObject(GameObject GameObject)
        {
            this.GameObjects.Remove(GameObject);
        }
    }
}