namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class VillageObjectData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="VillageObjectData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public VillageObjectData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
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

        public int TileX100
        {
            get; set;
        }

        public int TileY100
        {
            get; set;
        }

        public int RequiredTH
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

        public bool RequiresBuilder
        {
            get; set;
        }

        public string BuildResource
        {
            get; set;
        }

        public int BuildCost
        {
            get; set;
        }

        public int TownHallLevel
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public string Animations
        {
            get; set;
        }

        public int AnimX
        {
            get; set;
        }

        public int AnimY
        {
            get; set;
        }

        public int AnimID
        {
            get; set;
        }

        public int AnimDir
        {
            get; set;
        }

        public int AnimVisibilityOdds
        {
            get; set;
        }

        public bool HasInfoScreen
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }

        public int UnitHousing
        {
            get; set;
        }

        public bool HousesUnits
        {
            get; set;
        }

        internal ResourceData BuildResourceData
        {
            get
            {
                return (ResourceData) CSV.Tables.Get(Gamefile.Resource).GetData(this.BuildResource);
            }
        }

        internal int GetBuildTime(int Level)
        {
            return this.BuildTimeD[Level] * 86400 + this.BuildTimeH[Level] * 3600 + this.BuildTimeM[Level] * 60 + this.BuildTimeS[Level];
        }
    }
}
