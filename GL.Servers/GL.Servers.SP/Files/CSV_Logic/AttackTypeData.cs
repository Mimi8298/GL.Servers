namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class AttackTypeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AttackTypeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AttackTypeData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int Range
        {
            get; set;
        }

        public int AttackSpeed
        {
            get; set;
        }

        public bool StunSingle
        {
            get; set;
        }

        public bool StunWave
        {
            get; set;
        }

        public int StunCoolDown
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public int BlockTimeMillis
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }
    }
}
