namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;
    using GL.Servers.SL.Logic.Enums;

    internal class HeroData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HeroData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HeroData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public bool HiddenFromGame
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

        public string ClipReplaceOld
        {
            get; set;
        }

        public string ClipReplaceNew
        {
            get; set;
        }

        public string SpecialIconSWF
        {
            get; set;
        }

        public int Scale
        {
            get; set;
        }

        public string SpecialIconExportName
        {
            get; set;
        }

        public string SpecialAttackAimClip
        {
            get; set;
        }

        public string SpecialAttackName
        {
            get; set;
        }

        public string SpecialAttackDescription
        {
            get; set;
        }

        public string PassiveAbilityName
        {
            get; set;
        }

        public string PassiveAbilityDescription
        {
            get; set;
        }

        public string PassiveAbilityIconSWF
        {
            get; set;
        }

        public string PassiveAbilityIconExportName
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

        public string IconSelectSWF
        {
            get; set;
        }

        public string IconSelectExportName
        {
            get; set;
        }

        public int SortingOrder
        {
            get; set;
        }

        public int RelativeStartingRank
        {
            get; set;
        }

        public List<int> UpgradeTimeSeconds
        {
            get; set;
        }

        public int RequiredXpLevel
        {
            get; set;
        }

        public string RequiredQuest
        {
            get; set;
        }

        public string ShowOnQuest
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int SpecialAttackDamage
        {
            get; set;
        }

        public int Hitpoints
        {
            get; set;
        }

        public string Resource
        {
            get; set;
        }

        public List<string> Cost
        {
            get; set;
        }

        public int PushEnemyHeroMod
        {
            get; set;
        }

        public int SpecialAttackActiveLimit
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public string SpecialAttack
        {
            get; set;
        }

        public int SpecialAttackPushback
        {
            get; set;
        }

        public string SpecialAttackProjectile
        {
            get; set;
        }

        public string SpecialAttackSpawnObstacle
        {
            get; set;
        }

        public int SpecialAttackProjectileCount
        {
            get; set;
        }

        public int SpecialAttackKnockback
        {
            get; set;
        }

        public int SpecialAttackSpeedBoostDuration
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

        public string FlickEffect
        {
            get; set;
        }

        public string MoveEffect
        {
            get; set;
        }

        public string SpecialAttackDragEffect
        {
            get; set;
        }

        public string SpecialFlickEffect
        {
            get; set;
        }

        public string SpecialHitEffect
        {
            get; set;
        }

        public string SpecialHitEffectObstacle
        {
            get; set;
        }

        public string SpecialHitEffectEdge
        {
            get; set;
        }

        public string SpecialMoveEffect
        {
            get; set;
        }

        public string EndSpecialEffect
        {
            get; set;
        }

        public string DrawBackSpecialLoop
        {
            get; set;
        }

        public string SpecialRollLoop
        {
            get; set;
        }

        public string TakeDamageEffect
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public string RollSound
        {
            get; set;
        }

        public int ShadowHeight
        {
            get; set;
        }

        public string BecomesActiveEffect
        {
            get; set;
        }

        public string World
        {
            get; set;
        }

        public string BaseExportName
        {
            get; set;
        }

        public string PassiveAbility
        {
            get; set;
        }

        public int PassiveAbilityOpensRank
        {
            get; set;
        }

        public string TipPortraitSWF
        {
            get; set;
        }

        public string TipPortraitExportName
        {
            get; set;
        }

        public int TiredTimer
        {
            get; set;
        }

        internal QuestData RequiredQuestData
        {
            get
            {
                return string.IsNullOrEmpty(this.RequiredQuest) ? null : (QuestData) CSV.Tables.Get(Gamefile.Quests).GetData(this.RequiredQuest);
            }
        }
    }
}
