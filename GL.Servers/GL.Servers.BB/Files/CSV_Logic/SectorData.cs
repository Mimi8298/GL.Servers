namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class SectorData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SectorData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SectorData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string AreaMesh
        {
            get; set;
        }

        public string GlowMesh
        {
            get; set;
        }

        public string BorderMesh
        {
            get; set;
        }

        public string AreaTexture
        {
            get; set;
        }

        public string AreaTexture2
        {
            get; set;
        }

        public string GlowTexture
        {
            get; set;
        }

        public string BorderTexture
        {
            get; set;
        }

        public int BaseRegionIndex
        {
            get; set;
        }

        public int BaseNodeIndex
        {
            get; set;
        }

        public int Regions
        {
            get; set;
        }

        public int BuildTimeD
        {
            get; set;
        }

        public int BuildTimeH
        {
            get; set;
        }

        public int BuildTimeM
        {
            get; set;
        }

        public int BuildTimeS
        {
            get; set;
        }

        public int BuildCost
        {
            get; set;
        }

        public int RewardResourcePercent
        {
            get; set;
        }

        public int RewardArtifactPieces
        {
            get; set;
        }

        public int MaxStoredSupplies
        {
            get; set;
        }

        public int SectorOverlayOpacity
        {
            get; set;
        }

        public int SectorOverlayMultiplierRed
        {
            get; set;
        }

        public int SectorOverlayMultiplierGreen
        {
            get; set;
        }

        public int SectorOverlayMultiplierBlue
        {
            get; set;
        }
    }
}
