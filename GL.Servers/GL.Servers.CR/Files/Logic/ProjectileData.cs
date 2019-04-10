namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ProjectileData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ProjectileData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ProjectileData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ProjectileData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Rarity
        {
            get; set;
        }

        internal int Speed
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string RedExportName
        {
            get; set;
        }

        internal string ShadowExportName
        {
            get; set;
        }

        internal string RedShadowExportName
        {
            get; set;
        }

        internal bool ShadowDisableRotate
        {
            get; set;
        }

        internal int Scale
        {
            get; set;
        }

        internal bool Homing
        {
            get; set;
        }

        internal string HitEffect
        {
            get; set;
        }

        internal string DeathEffect
        {
            get; set;
        }

        internal int Damage
        {
            get; set;
        }

        internal int CrownTowerDamagePercent
        {
            get; set;
        }

        internal int Pushback
        {
            get; set;
        }

        internal bool PushbackAll
        {
            get; set;
        }

        internal int Radius
        {
            get; set;
        }

        internal int RadiusY
        {
            get; set;
        }

        internal bool AoeToAir
        {
            get; set;
        }

        internal bool AoeToGround
        {
            get; set;
        }

        internal bool OnlyEnemies
        {
            get; set;
        }

        internal bool OnlyOwnTroops
        {
            get; set;
        }

        internal int MaximumTargets
        {
            get; set;
        }

        internal int Gravity
        {
            get; set;
        }

        internal string SpawnAreaEffectObject
        {
            get; set;
        }

        internal int SpawnCharacterLevelIndex
        {
            get; set;
        }

        internal int SpawnCharacterDeployTime
        {
            get; set;
        }

        internal string SpawnDeployBaseAnim
        {
            get; set;
        }

        internal string SpawnCharacter
        {
            get; set;
        }

        internal bool SpawnConstPriority
        {
            get; set;
        }

        internal int SpawnCharacterCount
        {
            get; set;
        }

        internal string TargetBuff
        {
            get; set;
        }

        internal int BuffTime
        {
            get; set;
        }

        internal int BuffTimeIncreasePerLevel
        {
            get; set;
        }

        internal string TrailEffect
        {
            get; set;
        }

        internal int ProjectileRadius
        {
            get; set;
        }

        internal int ProjectileRadiusY
        {
            get; set;
        }

        internal int ProjectileRange
        {
            get; set;
        }

        internal bool use360Frames
        {
            get; set;
        }

        internal string HitSoundWhenParentAlive
        {
            get; set;
        }

        internal string SpawnProjectile
        {
            get; set;
        }

        internal int MinDistance
        {
            get; set;
        }

        internal int MaxDistance
        {
            get; set;
        }

        internal int ConstantHeight
        {
            get; set;
        }

        internal bool HeightFromTargetRadius
        {
            get; set;
        }

        internal int Heal
        {
            get; set;
        }

        internal int CrownTowerHealPercent
        {
            get; set;
        }

        internal bool TargetToEdge
        {
            get; set;
        }

        internal int ChainedHitRadius
        {
            get; set;
        }

        internal string ChainedHitEndEffect
        {
            get; set;
        }

        internal string PingpongDeathEffect
        {
            get; set;
        }

        internal bool ShakesTargets
        {
            get; set;
        }

        internal int PingpongVisualTime
        {
            get; set;
        }

        internal int RandomAngle
        {
            get; set;
        }

        internal int RandomDistance
        {
            get; set;
        }

        internal string Scatter
        {
            get; set;
        }

        internal int DragBackSpeed
        {
            get; set;
        }

        internal int DragMargin
        {
            get; set;
        }

        internal string TargettedEffect
        {
            get; set;
        }

    }
}