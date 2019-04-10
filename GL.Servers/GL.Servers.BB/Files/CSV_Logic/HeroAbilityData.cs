namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class HeroAbilityData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HeroAbilityData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HeroAbilityData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string FlavorTID
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

        public bool ActiveAbility
        {
            get; set;
        }

        public int HeroLevel
        {
            get; set;
        }

        public int UpgradeTimeH
        {
            get; set;
        }

        public int UpgradeTimeM
        {
            get; set;
        }

        public string UpgradeCost
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public int DurationMS
        {
            get; set;
        }

        public string TargetBuildingClass
        {
            get; set;
        }

        public int HealPerSecond
        {
            get; set;
        }

        public int ChargeSpeedBoost
        {
            get; set;
        }

        public string ChargeStatusEffect
        {
            get; set;
        }

        public int ChargeDuration
        {
            get; set;
        }

        public string EffectBuildingSelected
        {
            get; set;
        }

        public int DamageBoost
        {
            get; set;
        }

        public int StunTimeMS
        {
            get; set;
        }

        public string Spell
        {
            get; set;
        }

        public string SpellSecondary
        {
            get; set;
        }

        public int AttackDamage
        {
            get; set;
        }

        public int AttackDamageSecondary
        {
            get; set;
        }

        public int AttackRange
        {
            get; set;
        }

        public int AttackRate
        {
            get; set;
        }

        public int AttackSpread
        {
            get; set;
        }

        public int DamageRadius
        {
            get; set;
        }

        public bool FriendlyFire
        {
            get; set;
        }

        public string AttackEffect
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string HitEffect
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

        public int AttackLength
        {
            get; set;
        }
    }
}
