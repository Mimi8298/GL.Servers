namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

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

        public string TIDSlogan
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportNamePortrait
        {
            get; set;
        }

        public string ExportNameCharacterPose
        {
            get; set;
        }

        public string ExportNameBackside
        {
            get; set;
        }

        public string ExportNameIdle
        {
            get; set;
        }

        public string ExportNameAttack
        {
            get; set;
        }

        public int AttackFrame
        {
            get; set;
        }

        public string ExportNameCharge
        {
            get; set;
        }

        public string ExportNameReceiveDamage
        {
            get; set;
        }

        public string ExportNameCelebrate
        {
            get; set;
        }

        public string ExportNameDie
        {
            get; set;
        }

        public string ExportNameRun
        {
            get; set;
        }

        public string LoadedEffectExportName
        {
            get; set;
        }

        public string BlockColor
        {
            get; set;
        }

        public int Health
        {
            get; set;
        }

        public int BaseDamage
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string BeamSWF
        {
            get; set;
        }

        public string BeamExportName
        {
            get; set;
        }

        public string BeamCageExportName
        {
            get; set;
        }

        public string HurtSound
        {
            get; set;
        }

        public string ProjectileType
        {
            get; set;
        }

        public string Color
        {
            get; set;
        }

        public string LowHealthSound
        {
            get; set;
        }

        public string Boost
        {
            get; set;
        }

        public bool AlignToBottom
        {
            get; set;
        }
    }
}
