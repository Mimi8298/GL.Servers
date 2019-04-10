namespace GL.Servers.BB.Files.CSV_Logic
{
    using System.Collections.Generic;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class CharacterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CharacterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CharacterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int HousingSpace
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

        public int Speed
        {
            get; set;
        }

        public int Hitpoints
        {
            get; set;
        }

        public int TrainingTime
        {
            get; set;
        }

        public List<string> TrainingCost
        {
            get; set;
        }

        public List<int> UpgradeTimeH
        {
            get; set;
        }

        public List<int> UpgradeCost
        {
            get; set;
        }

        public int MaxAttackRange
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

        public int ReloadTime
        {
            get; set;
        }

        public int ShotsBeforeReload
        {
            get; set;
        }

        public int LaserDistance
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int LifeLeech
        {
            get; set;
        }

        public int AttackSpread
        {
            get; set;
        }

        public int AttackDistanceOffsetPercent
        {
            get; set;
        }

        public int DamageOverFiveSeconds
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

        public int BoostTimeMs
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

        public int Energy
        {
            get; set;
        }

        public bool IsNpc
        {
            get; set;
        }

        public bool CanAttackWhileWalking
        {
            get; set;
        }

        public bool TurnTowardsTarget
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

        public string FlavorTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string SpeedTID
        {
            get; set;
        }

        public string AttackRangeTID
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

        public string Projectile
        {
            get; set;
        }

        public string DeployEffect
        {
            get; set;
        }

        public string AttackEffect
        {
            get; set;
        }

        public string AttackEffect2
        {
            get; set;
        }

        public string AttackEffect3
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public string HitEffect2
        {
            get; set;
        }

        public string DisembarkEffect
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public string Animation
        {
            get; set;
        }

        public string AnimationEnemy
        {
            get; set;
        }

        public string MissEffect
        {
            get; set;
        }

        public string Footstep
        {
            get; set;
        }

        public string MoveLoopSound
        {
            get; set;
        }

        public int PatrolRadius
        {
            get; set;
        }

        public int CombatSetupTimeMs
        {
            get; set;
        }

        public int CombatTeardownTimeMs
        {
            get; set;
        }

        public int CombatTeardownDelayMs
        {
            get; set;
        }

        public bool Flying
        {
            get; set;
        }

        public string Hero
        {
            get; set;
        }

        public int SizeClass
        {
            get; set;
        }

        public bool IsHero()
        {
            return !string.IsNullOrEmpty(this.Hero);
        }

        public ResourceBundle GetResourceBundle(int Level)
        {
            return new ResourceBundle(this.TrainingCost[Level]);
        }
    }
}
