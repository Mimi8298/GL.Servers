namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class EnemyCharacterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EnemyCharacterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EnemyCharacterData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int BountyMin
        {
            get; set;
        }

        public int BountyMax
        {
            get; set;
        }

        public int Scale
        {
            get; set;
        }

        public int Hitpoints
        {
            get; set;
        }

        public int Damage
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

        public bool HidesOnSpecialAttack
        {
            get; set;
        }

        public bool NoCollisionWhenDies
        {
            get; set;
        }

        public int FirstAttackOnTurn
        {
            get; set;
        }

        public int AttackTurnSeq
        {
            get; set;
        }

        public string AttackStateStartedEffect
        {
            get; set;
        }

        public string AppearEffect
        {
            get; set;
        }

        public string TakeDamageEffect
        {
            get; set;
        }

        public string TakeDamageEffect2
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public int ShadowHeight
        {
            get; set;
        }

        public int VelocityModifierOnCollision
        {
            get; set;
        }

        public int ColorMulR
        {
            get; set;
        }

        public int ColorMulG
        {
            get; set;
        }

        public int ColorMulB
        {
            get; set;
        }

        public int ColorAddR
        {
            get; set;
        }

        public int ColorAddG
        {
            get; set;
        }

        public int ColorAddB
        {
            get; set;
        }

        public string SpawnEnemyWhenDie1
        {
            get; set;
        }

        public string SpawnEnemyWhenDie2
        {
            get; set;
        }

        public string AttacksChosenInSequence
        {
            get; set;
        }

        public int AttackOddsA
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

        public string AttackTypeA
        {
            get; set;
        }

        public int AttackDelayA
        {
            get; set;
        }

        public string ProjectileA
        {
            get; set;
        }

        public string SpawnedCharacterWhenAttackA
        {
            get; set;
        }

        public int AttackRadiusA
        {
            get; set;
        }

        public int AttackOddsB
        {
            get; set;
        }

        public string AttackEffectB
        {
            get; set;
        }

        public string AttackEffect2B
        {
            get; set;
        }

        public string AttackTypeB
        {
            get; set;
        }

        public int AttackDelayB
        {
            get; set;
        }

        public string ProjectileB
        {
            get; set;
        }

        public string SpawnedCharacterWhenAttackB
        {
            get; set;
        }

        public int AttackRadiusB
        {
            get; set;
        }

        public int DamageB
        {
            get; set;
        }

        public int DeathAreaDamage
        {
            get; set;
        }

        public int DeathDelayTimer
        {
            get; set;
        }

        public string DeathAreaDamageEffect
        {
            get; set;
        }

        public int DeathAreaDamageRadius
        {
            get; set;
        }
    }
}
