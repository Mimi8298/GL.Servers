namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ArtifactBonusData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ArtifactBonusData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ArtifactBonusData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int BoostMultiplier
        {
            get; set;
        }

        public string ExportNameAdd
        {
            get; set;
        }

        public int ArtifactType
        {
            get; set;
        }

        public int PowerStoneBoostTimeHours
        {
            get; set;
        }
    }
}
