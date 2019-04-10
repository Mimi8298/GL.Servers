namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class WorldData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="WorldData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public WorldData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoUIExportName
        {
            get; set;
        }

        public string LevelBackgroundSWF
        {
            get; set;
        }

        public string LevelBackgroundExportNames
        {
            get; set;
        }

        public string LevelEndBackgroundExportNames
        {
            get; set;
        }

        public string LevelForegroundExportNames
        {
            get; set;
        }

        public string LevelCloudSWF
        {
            get; set;
        }

        public string LevelCloudExportNames
        {
            get; set;
        }

        public int ShadowColorMulR
        {
            get; set;
        }

        public int ShadowColorMulG
        {
            get; set;
        }

        public int ShadowColorMulB
        {
            get; set;
        }

        public int ShadowAlpha
        {
            get; set;
        }

        public string ProfileBackgroundExportName
        {
            get; set;
        }

        public string BarrelEnemies
        {
            get; set;
        }

        public int BarrelEnemyChance
        {
            get; set;
        }
    }
}
