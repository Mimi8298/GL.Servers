namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Tiles : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Tiles"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Tiles(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string TileCode
        {
            get; set;
        }

        public bool BlocksMovement
        {
            get; set;
        }

        public bool BlocksProjectiles
        {
            get; set;
        }

        public bool IsDestructible
        {
            get; set;
        }

        public bool IsDestructibleNormalWeapon
        {
            get; set;
        }

        public bool HidesHero
        {
            get; set;
        }

        public int RespawnSeconds
        {
            get; set;
        }

        public int CollisionMargin
        {
            get; set;
        }

        public string BaseExportName
        {
            get; set;
        }

        public string BaseExplosionEffect
        {
            get; set;
        }

        public string BaseHitEffect
        {
            get; set;
        }

        public string BaseWindEffect
        {
            get; set;
        }

        public string BaseBulletHole1
        {
            get; set;
        }

        public string BaseBulletHole2
        {
            get; set;
        }

        public string BaseCrack1
        {
            get; set;
        }

        public string BaseCrack2
        {
            get; set;
        }

        public int SortOffset
        {
            get; set;
        }

        public bool HasHitAnim
        {
            get; set;
        }

        public bool HasWindAnim
        {
            get; set;
        }

        public int ShadowScaleX
        {
            get; set;
        }

        public int ShadowScaleY
        {
            get; set;
        }

        public int ShadowX
        {
            get; set;
        }

        public int ShadowY
        {
            get; set;
        }

        public int ShadowSkew
        {
            get; set;
        }
    }
}
