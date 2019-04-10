namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class MonsterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MonsterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MonsterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string AppearEffect
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportNameAppear
        {
            get; set;
        }

        public string ExportNameIdle
        {
            get; set;
        }

        public string ExportNameMove
        {
            get; set;
        }

        public string ExportNamePrepareAttack
        {
            get; set;
        }

        public string ExportNameAttack
        {
            get; set;
        }

        public int AttackDelayMS
        {
            get; set;
        }

        public string ExportNameSpecial
        {
            get; set;
        }

        public string ExportNamePrepareSpecial
        {
            get; set;
        }

        public string ExportNameReceiveDamage
        {
            get; set;
        }

        public string ExportNameDie
        {
            get; set;
        }

        public string ExportNameDisappear
        {
            get; set;
        }

        public string ExportNamePreSpawn
        {
            get; set;
        }

        public string ExportNameSpawn
        {
            get; set;
        }

        public string CageExportName
        {
            get; set;
        }

        public string DieEffect
        {
            get; set;
        }

        public string AttackType
        {
            get; set;
        }

        public string SpecialMove
        {
            get; set;
        }

        public int Health
        {
            get; set;
        }

        public int Shield
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Reach
        {
            get; set;
        }

        public bool BlockBeam
        {
            get; set;
        }

        public bool Obstacle
        {
            get; set;
        }

        public string TurnScript
        {
            get; set;
        }

        public string HitScript
        {
            get; set;
        }

        public string DieScript
        {
            get; set;
        }

        public string ReachScript
        {
            get; set;
        }

        public bool AlignToBottom
        {
            get; set;
        }

        public int Energy
        {
            get; set;
        }

        public int TurnsToLive
        {
            get; set;
        }

        public int Score
        {
            get; set;
        }

        public int MaxSpawnCount
        {
            get; set;
        }

        public int SpawnDelayMS
        {
            get; set;
        }

        public string FaceExportNames
        {
            get; set;
        }
    }
}
