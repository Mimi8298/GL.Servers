namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class LandingShipData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LandingShipData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LandingShipData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ExportName
        {
            get; set;
        }

        public string ModelName
        {
            get; set;
        }

        public string TextureName
        {
            get; set;
        }

        public string EnemyTextureName
        {
            get; set;
        }

        public string MeshName
        {
            get; set;
        }

        public string ShadowModelName
        {
            get; set;
        }

        public string ShadowMeshName
        {
            get; set;
        }

        public string ShadowTextureName
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public int DeployTimeMS
        {
            get; set;
        }

        public int TimeBetweenUnitsMS
        {
            get; set;
        }

        public int UnitCapacity
        {
            get; set;
        }
    }
}
