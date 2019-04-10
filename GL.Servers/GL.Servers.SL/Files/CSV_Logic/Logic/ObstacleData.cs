namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class ObstacleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ObstacleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ObstacleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Scale
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ShadowSWF
        {
            get; set;
        }

        public string ShadowExportName
        {
            get; set;
        }

        public bool ShadowAlpha
        {
            get; set;
        }

        public int ShadowHeight
        {
            get; set;
        }

        public int DamageOnTouch
        {
            get; set;
        }

        public int VelocityModifierOnCollision
        {
            get; set;
        }

        public int Hitpoints
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public int LifeInTurns
        {
            get; set;
        }

        public bool SpecialAttackCanRemove
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public string SpawnCollectableOnHit
        {
            get; set;
        }

        public string SpawnEnemyOnHit
        {
            get; set;
        }

        public int SpawnEnemyOnHitRank
        {
            get; set;
        }

        public string SpawnObstacleWhenDie
        {
            get; set;
        }

        public bool NoCollisionWhenDies
        {
            get; set;
        }

        public bool Passthrough
        {
            get; set;
        }

        public int SlowdownDuringPassthrough
        {
            get; set;
        }

        public string PassEffect
        {
            get; set;
        }

        public string SpawnEffect
        {
            get; set;
        }

        public string AreaDmgFx1
        {
            get; set;
        }

        public string AreaDmgFx2
        {
            get; set;
        }
    }
}
