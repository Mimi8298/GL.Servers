namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class NpcData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NpcData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NpcData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string MapInstanceName
        {
            get; set;
        }

        public List<string> MapDependencies
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public int ExpLevel
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

        public string LevelFile
        {
            get; set;
        }

        public int Gold
        {
            get; set;
        }

        public int Elixir
        {
            get; set;
        }

        public bool AlwaysUnlocked
        {
            get; set;
        }

        public string PlayerName
        {
            get; set;
        }

        public string AllianceName
        {
            get; set;
        }

        public int AllianceBadge
        {
            get; set;
        }

        public bool SinglePlayer
        {
            get; set;
        }
    }
}
