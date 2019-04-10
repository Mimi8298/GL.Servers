namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;
	using GL.Servers.CoC.Logic.Enums;

    internal class CharacterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CharacterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CharacterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // CharacterData.
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public int HousingSpace
        {
            get; set;
        }

        public int BarrackLevel
        {
            get; set;
        }

        public List<int> LaboratoryLevel
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

        public string TrainingResource
        {
            get; set;
        }

        public List<int> TrainingCost
        {
            get; set;
        }

        public List<int> UpgradeTimeH
        {
            get; set;
        }

        public List<int> UpgradeTimeM
        {
            get; set;
        }

        public string UpgradeResource
        {
            get; set;
        }

        public List<int> UpgradeCost
        {
            get; set;
        }

        public int DonateCost
        {
            get; set;
        }

        public int AttackRange
        {
            get; set;
        }

        public int AttackSpeed
        {
            get; set;
        }

        public int DPS
        {
            get; set;
        }

        public int PreferedTargetDamageMod
        {
            get; set;
        }

        public int DamageRadius
        {
            get; set;
        }

        public bool AreaDamageIgnoresWalls
        {
            get; set;
        }

        public bool SelfAsAoeCenter
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

        public string BigPicture
        {
            get; set;
        }

        public string BigPictureSWF
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string AltProjectile
        {
            get; set;
        }

        public string PreferedTargetBuilding
        {
            get; set;
        }

        public string PreferedTargetBuildingClass
        {
            get; set;
        }

        public bool PreferredTargetNoTargeting
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

        public string HitEffect
        {
            get; set;
        }

        public string HitEffect2
        {
            get; set;
        }

        public bool IsFlying
        {
            get; set;
        }

        public bool AirTargets
        {
            get; set;
        }

        public bool GroundTargets
        {
            get; set;
        }

        public int AttackCount
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public string DieEffect2
        {
            get; set;
        }

        public string Animation
        {
            get; set;
        }

        public int UnitOfType
        {
            get; set;
        }

        public bool IsJumper
        {
            get; set;
        }

        public int MovementOffsetAmount
        {
            get; set;
        }

        public int MovementOffsetSpeed
        {
            get; set;
        }

        public string TombStone
        {
            get; set;
        }

        public int DieDamage
        {
            get; set;
        }

        public int DieDamageRadius
        {
            get; set;
        }

        public string DieDamageEffect
        {
            get; set;
        }

        public int DieDamageDelay
        {
            get; set;
        }

        public bool DisableProduction
        {
            get; set;
        }

        public string SecondaryTroop
        {
            get; set;
        }

        public bool IsSecondaryTroop
        {
            get; set;
        }

        public int SecondaryTroopCnt
        {
            get; set;
        }

        public int SecondarySpawnDist
        {
            get; set;
        }

        public bool RandomizeSecSpawnDist
        {
            get; set;
        }

        public bool PickNewTargetAfterPushback
        {
            get; set;
        }

        public int PushbackSpeed
        {
            get; set;
        }

        public string SummonTroop
        {
            get; set;
        }

        public int SummonTroopCount
        {
            get; set;
        }

        public int SummonCooldown
        {
            get; set;
        }

        public string SummonEffect
        {
            get; set;
        }

        public int SummonLimit
        {
            get; set;
        }

        public int SpawnIdle
        {
            get; set;
        }

        public bool SpawnOnAttack
        {
            get; set;
        }

        public int StrengthWeight
        {
            get; set;
        }

        public string ChildTroop
        {
            get; set;
        }

        public int ChildTroopCount
        {
            get; set;
        }

        public int SpeedDecreasePerChildTroopLost
        {
            get; set;
        }

        public int ChildTroop0_X
        {
            get; set;
        }

        public int ChildTroop0_Y
        {
            get; set;
        }

        public int ChildTroop1_X
        {
            get; set;
        }

        public int ChildTroop1_Y
        {
            get; set;
        }

        public int ChildTroop2_X
        {
            get; set;
        }

        public int ChildTroop2_Y
        {
            get; set;
        }

        public bool AttackMultipleBuildings
        {
            get; set;
        }

        public bool IncreasingDamage
        {
            get; set;
        }

        public int DPSLv2
        {
            get; set;
        }

        public int DPSLv3
        {
            get; set;
        }

        public int Lv2SwitchTime
        {
            get; set;
        }

        public int Lv3SwitchTime
        {
            get; set;
        }

        public string AttackEffectLv2
        {
            get; set;
        }

        public string AttackEffectLv3
        {
            get; set;
        }

        public string AttackEffectLv4
        {
            get; set;
        }

        public string TransitionEffectLv2
        {
            get; set;
        }

        public string TransitionEffectLv3
        {
            get; set;
        }

        public string TransitionEffectLv4
        {
            get; set;
        }

        public int HitEffectOffset
        {
            get; set;
        }

        public int TargetedEffectOffset
        {
            get; set;
        }

        public int SecondarySpawnOffset
        {
            get; set;
        }

        public string CustomDefenderIcon
        {
            get; set;
        }

        public int SpecialMovementMod
        {
            get; set;
        }

        public int InvisibilityRadius
        {
            get; set;
        }

        public int HealthReductionPerSecond
        {
            get; set;
        }

        public int AutoMergeDistance
        {
            get; set;
        }

        public int AutoMergeGroupSize
        {
            get; set;
        }

        public bool IsUnderground
        {
            get; set;
        }

        public int ProjectileBounces
        {
            get; set;
        }

        public int FriendlyGroupWeight
        {
            get; set;
        }

        public int EnemyGroupWeight
        {
            get; set;
        }

        public int NewTargetAttackDelay
        {
            get; set;
        }

        public bool TriggersTraps
        {
            get; set;
        }

        public int ChainShootingDistance
        {
            get; set;
        }

        public string PreAttackEffect
        {
            get; set;
        }

        public string MoveStartsEffect
        {
            get; set;
        }

        public string MoveTrailEffect
        {
            get; set;
        }

        public string BecomesTargetableEffect
        {
            get; set;
        }

        public bool BoostedIfAlone
        {
            get; set;
        }

        public int BoostRadius
        {
            get; set;
        }

        public int BoostDmgPerfect
        {
            get; set;
        }

        public int BoostAttackSpeed
        {
            get; set;
        }

        public string HideEffect
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }

        public int UnitsInCamp
        {
            get; set;
        }

        public int SpecialAbilityLevel
        {
            get; set;
        }

        public string SpecialAbilityName
        {
            get; set;
        }

        public string SpecialAbilityInfo
        {
            get; set;
        }

        public string SpecialAbilityType
        {
            get; set;
        }

        public int SpecialAbilityAttribute
        {
            get; set;
        }

        public int SpecialAbilityAttribute2
        {
            get; set;
        }

        public int SpecialAbilityAttribute3
        {
            get; set;
        }

        public string SpecialAbilitySpell
        {
            get; set;
        }

        public string SpecialAbilityEffect
        {
            get; set;
        }

        public bool DisableDonate
        {
            get; set;
        }

        public bool ScaleByTH
        {
            get; set;
        }

        public bool EnabledByCalendar
        {
            get; set;
        }

        public int LoseHpPerTick
        {
            get; set;
        }

        public int LoseHpInterval
        {
            get; set;
        }

        internal ResourceData TrainingResourceData
        {
            get
            {
                return (ResourceData) CSV.Tables.Get(Gamefile.Resource).GetData(this.TrainingResource);
            }
        }

        internal ResourceData UpgradeResourceData
        {
            get
            {
                return (ResourceData) CSV.Tables.Get(Gamefile.Resource).GetData(this.UpgradeResource);
            }
        }

        internal int GetUpgradeTime(int Level)
        {
            return this.UpgradeTimeH[Level] * 3600 + this.UpgradeTimeM[Level] * 60;
        }

        internal bool IsUnlockedForBarrackLevel(int BarrackLevel)
        {
            return BarrackLevel >= this.BarrackLevel - 1;
        }
    }
}
