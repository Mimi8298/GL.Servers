namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TownHallData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TownHallData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TownHallData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public int BrokenPartCount
        {
            get; set;
        }

        public string BrokenExportName
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public int VillageCapacity
        {
            get; set;
        }

        public string CollectionTool1
        {
            get; set;
        }

        public int CollectionTool1Amount
        {
            get; set;
        }

        public string CollectionTool2
        {
            get; set;
        }

        public int CollectionTool2Amount
        {
            get; set;
        }

        public string CollectionTool3
        {
            get; set;
        }

        public int CollectionTool3Amount
        {
            get; set;
        }

        public int ReputationLevelRequired
        {
            get; set;
        }
    }
}
