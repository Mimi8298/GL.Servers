namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AmbientAnimalSpawnerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AmbientAnimalSpawnerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AmbientAnimalSpawnerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int EdgeSpawnTileX
        {
            get; set;
        }

        public int EdgeSpawnTileY
        {
            get; set;
        }

        public int BirdExtraTiles
        {
            get; set;
        }
    }
}
