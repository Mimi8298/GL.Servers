namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class BoostData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BoostData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BoostData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string IconExportName
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public string LoopingEffect
        {
            get; set;
        }

        public string GlobalEffect
        {
            get; set;
        }

        public bool TargetSelf
        {
            get; set;
        }

        public bool TargetTeam
        {
            get; set;
        }

        public bool TargetLane
        {
            get; set;
        }

        public bool TargetAll
        {
            get; set;
        }

        public bool HeroSwap
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int Heal
        {
            get; set;
        }

        public int StunTurns
        {
            get; set;
        }

        public int DamageBoost
        {
            get; set;
        }

        public int KnockBack
        {
            get; set;
        }

        public int EnergyCost
        {
            get; set;
        }

        public int Turns
        {
            get; set;
        }

        public string BlockType
        {
            get; set;
        }
    }
}
