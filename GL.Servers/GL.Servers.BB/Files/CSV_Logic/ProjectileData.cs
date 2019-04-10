namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ProjectileData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ProjectileData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ProjectileData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameEnemy
        {
            get; set;
        }

        public string ParticleEmitter
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public int StartHeight
        {
            get; set;
        }

        public int StartOffset
        {
            get; set;
        }

        public bool IsBallistic
        {
            get; set;
        }

        public bool DisplayShadow
        {
            get; set;
        }

        public string SpawnProjectile
        {
            get; set;
        }

        public int SpawnCount
        {
            get; set;
        }

        public int SpawnRangeMin
        {
            get; set;
        }

        public int SpawnRangeMax
        {
            get; set;
        }

        public int SpawnRandomAngleOffset
        {
            get; set;
        }

        public int SpawnSpread
        {
            get; set;
        }

        public string RopeShoot
        {
            get; set;
        }

        public string RopePull
        {
            get; set;
        }

        public string PullExportName
        {
            get; set;
        }
    }
}
