namespace GL.Servers.CoC.Logic.Map
{
    internal class TileMap
    {
        private Tile[][] Tiles;

        internal Tile this[int X, int Y, int Map]
        {
            get
            {
                int Index = 50 * X + Y;

                if (50 * 50 > Index && Index >= 0)
                {
                    return this.Tiles[Map][Index];
                }
                
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> class.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public TileMap(int Width, int Height)
        {
            this.Tiles = new Tile[2][];

            this.Tiles[0] = new Tile[Width * Height];
            this.Tiles[1] = new Tile[Width * Height];

            for (int i = 0; i < Width * Height; i++)
            {
                this.Tiles[0][i] = new Tile();
                this.Tiles[1][i] = new Tile();
            }
        }

        /// <summary>
        /// Adds a gameobject on the map.
        /// </summary>
        internal void AddGameObject(GameObject GameObject)
        {
            for (int i = 0; i < GameObject.WidthInTiles; i++)
            {
                for (int j = 0; j < GameObject.HeightInTiles; j++)
                {
                    this.Tiles[GameObject.VillageType][(50 * (i + GameObject.TileX)) + (j + GameObject.TileY)].AddGameObject(GameObject);
                }   
            }
        }

        /// <summary>
        /// Applises the change of the position of gameobject on the map. 
        /// </summary>
        internal void GameObjectMoved(GameObject GameObject, int OldX, int OldY)
        {
            for (int i = 0; i < GameObject.WidthInTiles; i++)
            {
                for (int j = 0; j < GameObject.HeightInTiles; j++)
                {
                    this.Tiles[GameObject.VillageType][(50 * (i + OldX)) + (j + OldY)].RemoveGameObject(GameObject);
                }
            }

            for (int i = 0; i < GameObject.WidthInTiles; i++)
            {
                for (int j = 0; j < GameObject.HeightInTiles; j++)
                {
                    this.Tiles[GameObject.VillageType][(50 * (i + GameObject.TileX)) + (j + GameObject.TileY)].AddGameObject(GameObject);
                }
            }
        }
    }
}