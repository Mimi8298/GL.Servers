namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Skills : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Skills"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Skills(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string BehaviorType
        {
            get; set;
        }

        public bool CanMoveAtSameTime
        {
            get; set;
        }

        public bool Targeted
        {
            get; set;
        }

        public int Cooldown
        {
            get; set;
        }

        public int ActiveTime
        {
            get; set;
        }

        public int CastingTime
        {
            get; set;
        }

        public int CastingRange
        {
            get; set;
        }

        public int MaxCastingRange
        {
            get; set;
        }

        public int RechargeTime
        {
            get; set;
        }

        public int MaxCharge
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int MsBetweenAttacks
        {
            get; set;
        }

        public int Spread
        {
            get; set;
        }

        public int AttackPattern
        {
            get; set;
        }

        public int NumBulletsInOneAttack
        {
            get; set;
        }

        public bool TwoGuns
        {
            get; set;
        }

        public bool ExecuteFirstAttackImmediately
        {
            get; set;
        }

        public int ChargePushback
        {
            get; set;
        }

        public int ChargeSpeed
        {
            get; set;
        }

        public int ChargeType
        {
            get; set;
        }

        public int NumSpawns
        {
            get; set;
        }

        public int MaxSpawns
        {
            get; set;
        }

        public bool AlwaysCastAtMaxRange
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string SummonedCharacter
        {
            get; set;
        }

        public string AreaEffectObject
        {
            get; set;
        }

        public string AreaEffectObject2
        {
            get; set;
        }

        public string SpawnedItem
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

        public string LargeIconSWF
        {
            get; set;
        }

        public string LargeIconExportName
        {
            get; set;
        }

        public string ButtonSWF
        {
            get; set;
        }

        public string ButtonExportName
        {
            get; set;
        }

        public string AttackEffect
        {
            get; set;
        }

        public string UseEffect
        {
            get; set;
        }

        public string EndEffect
        {
            get; set;
        }

        public string LoopEffect
        {
            get; set;
        }
    }
}
