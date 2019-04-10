namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ArtifactData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ArtifactData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ArtifactData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int PiecesNeeded
        {
            get; set;
        }

        public string PieceResource
        {
            get; set;
        }

        public int PieceDropChance
        {
            get; set;
        }

        public string SellResource
        {
            get; set;
        }

        public int SellResourceAmount
        {
            get; set;
        }

        public int BoostPercentage
        {
            get; set;
        }

        public int BoostProbability
        {
            get; set;
        }

        public int BuildTimeS
        {
            get; set;
        }

        public int ArtifactType
        {
            get; set;
        }
    }
}
