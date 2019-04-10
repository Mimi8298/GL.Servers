namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class SpellData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SpellData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SpellData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }
        
        public bool IsHeroAbility
        {
            get; set;
        }

        public int UnlockTownHallLevel
        {
            get; set;
        }

        public int UpgradeHouseLevel
        {
            get; set;
        }

        public int DeployTimeMS
        {
            get; set;
        }

        public int ChargingTimeMS
        {
            get; set;
        }

        public int HitTimeMS
        {
            get; set;
        }

        public int UpgradeTimeH
        {
            get; set;
        }

        public string UpgradeCost
        {
            get; set;
        }

        public int BoostTimeMS
        {
            get; set;
        }

        public int SpeedBoost
        {
            get; set;
        }

        public int ArmorBoost
        {
            get; set;
        }

        public int DamageBoost
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int Shield
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public int RadiusAgainstTroops
        {
            get; set;
        }

        public int NumberOfHits
        {
            get; set;
        }

        public int RandomRadius
        {
            get; set;
        }

        public int TimeBetweenHitsMS
        {
            get; set;
        }

        public bool RandomRadiusAffectsOnlyGfx
        {
            get; set;
        }

        public bool IsStun
        {
            get; set;
        }

        public bool IsFocusFire
        {
            get; set;
        }

        public bool IsSmoke
        {
            get; set;
        }

        public int DecoyCount
        {
            get; set;
        }

        public bool ProjectileForAllHits
        {
            get; set;
        }

        public int Energy
        {
            get; set;
        }

        public int EnergyIncrease
        {
            get; set;
        }

        public string SpawnCharacter
        {
            get; set;
        }

        public int SpawnCount
        {
            get; set;
        }

        public int SpawnLimit
        {
            get; set;
        }

        public int SpawnLifetime
        {
            get; set;
        }

        public int ResurrectHousingSpace
        {
            get; set;
        }

        public string ResurrectProjectile
        {
            get; set;
        }

        public string ResurrectProjectileHitEffect
        {
            get; set;
        }

        public int XpGain
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SubtitleTID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string BigPictureSWF
        {
            get; set;
        }

        public string BigPicture
        {
            get; set;
        }

        public string PreDeployEffect
        {
            get; set;
        }

        public string DeployEffect
        {
            get; set;
        }

        public string DeployEffect2
        {
            get; set;
        }

        public string ChargingEffect
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string HitEffectBuildingSelected
        {
            get; set;
        }

        public string HitEffectBuildingSelected2
        {
            get; set;
        }

        public string DurationTID
        {
            get; set;
        }
    }
}
