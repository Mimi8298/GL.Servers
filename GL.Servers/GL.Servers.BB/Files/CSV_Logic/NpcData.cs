namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class NpcData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NpcData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NpcData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string LevelFile
        {
            get; set;
        }

        public string BaseLevelFile
        {
            get; set;
        }

        public string Loot
        {
            get; set;
        }

        public int ExpLevel
        {
            get; set;
        }

        public int MatchmakingSkill
        {
            get; set;
        }

        public int DefenseValue
        {
            get; set;
        }

        public bool IsOutpost
        {
            get; set;
        }

        public int OutpostRes
        {
            get; set;
        }

        public int OutpostProduction
        {
            get; set;
        }

        public string UnitType
        {
            get; set;
        }

        public int UnitCount
        {
            get; set;
        }

        public int CommonArtifactPiecesFromTownHall
        {
            get; set;
        }

        public int RareArtifactPiecesFromTownHall
        {
            get; set;
        }

        public int EpicArtifactPiecesFromTownHall
        {
            get; set;
        }

        public bool IsEvent
        {
            get; set;
        }

        public bool IsCoop
        {
            get; set;
        }

        public bool IsGearheartEvent
        {
            get; set;
        }

        public bool IsMercenaryCamp
        {
            get; set;
        }

        public string LevelAuthorName
        {
            get; set;
        }
    }
}
