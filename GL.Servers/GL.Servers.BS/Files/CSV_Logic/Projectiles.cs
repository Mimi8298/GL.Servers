namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Projectiles : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Projectiles"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Projectiles(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string BlueExportName
        {
            get; set;
        }

        public string RedExportName
        {
            get; set;
        }

        public string ShadowExportName
        {
            get; set;
        }

        public string BlueGroundGlowExportName
        {
            get; set;
        }

        public string RedGroundGlowExportName
        {
            get; set;
        }

        public string PreExplosionBlueExportName
        {
            get; set;
        }

        public string PreExplosionRedExportName
        {
            get; set;
        }

        public int PreExplosionTimeMs
        {
            get; set;
        }

        public string HitEffectEnv
        {
            get; set;
        }

        public string HitEffectChar
        {
            get; set;
        }

        public string MaxRangeReachedEffect
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public bool Indirect
        {
            get; set;
        }

        public bool ConstantFlyTime
        {
            get; set;
        }

        public int TriggerWithDelayMs
        {
            get; set;
        }

        public int BouncePercent
        {
            get; set;
        }

        public int Gravity
        {
            get; set;
        }

        public int EarlyTicks
        {
            get; set;
        }

        public int HideTime
        {
            get; set;
        }

        public int Scale
        {
            get; set;
        }

        public bool RandomStartFrame
        {
            get; set;
        }

        public string SpawnAreaEffectObject
        {
            get; set;
        }

        public string SpawnAreaEffectObject2
        {
            get; set;
        }

        public string SpawnCharacter
        {
            get; set;
        }

        public string SpawnItem
        {
            get; set;
        }

        public string TrailEffect
        {
            get; set;
        }

        public bool ShotByHero
        {
            get; set;
        }

        public bool IsBeam
        {
            get; set;
        }

        public bool IsBouncing
        {
            get; set;
        }

        public string Rendering
        {
            get; set;
        }

        public bool PiercesCharacters
        {
            get; set;
        }

        public bool PiercesEnvironment
        {
            get; set;
        }

        public bool PiercesEnvironmentLikeButter
        {
            get; set;
        }

        public int PushbackStrength
        {
            get; set;
        }

        public int ChainsToEnemies
        {
            get; set;
        }

        public string ChainBullet
        {
            get; set;
        }

        public int DamagePercentStart
        {
            get; set;
        }

        public int DamagePercentEnd
        {
            get; set;
        }

        public bool DamagesConstantly
        {
            get; set;
        }

        public int FreezeStrength
        {
            get; set;
        }

        public int StunLengthMS
        {
            get; set;
        }

        public int ScaleStart
        {
            get; set;
        }

        public int ScaleEnd
        {
            get; set;
        }

        public bool AttractsPet
        {
            get; set;
        }

        public int LifeStealPercent
        {
            get; set;
        }

        public bool PassesEnvironment
        {
            get; set;
        }

        public int PoisonDamagePercent
        {
            get; set;
        }

        public int SameProjectileCanNotDamageMS
        {
            get; set;
        }

        public int HealOwnPercent
        {
            get; set;
        }
    }
}
