namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class HelperCharacterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HelperCharacterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HelperCharacterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string Helper
        {
            get; set;
        }

        public string ProductionBuildings
        {
            get; set;
        }

        public string AlertBuilding
        {
            get; set;
        }

        public int MaxPathLength
        {
            get; set;
        }

        public int MaxDistanceFromBuilding
        {
            get; set;
        }

        public int Speed
        {
            get; set;
        }

        public int SpeedNotWorking
        {
            get; set;
        }

        public int IdleMinMS
        {
            get; set;
        }

        public int IdleMaxMS
        {
            get; set;
        }

        public int IdleNotWorkingMinMS
        {
            get; set;
        }

        public int IdleNotWorkingMaxMS
        {
            get; set;
        }

        public int StayAtBuildingMinMS
        {
            get; set;
        }

        public int StayAtBuildingMaxMS
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }
    }
}
