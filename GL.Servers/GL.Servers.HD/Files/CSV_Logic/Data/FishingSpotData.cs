namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class FishingSpotData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FishingSpotData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FishingSpotData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string AdjacentSpots
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

        public string TID
        {
            get; set;
        }

        public string RandomSounds
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int ShapeMultiplierFat
        {
            get; set;
        }

        public int ShapeMultiplierSlim
        {
            get; set;
        }

        public int ShapeMultiplierNormal
        {
            get; set;
        }

        public string Special
        {
            get; set;
        }

        public string CollectionTool
        {
            get; set;
        }

        public int CollectionToolAmount
        {
            get; set;
        }

        public int CooldownMinutes
        {
            get; set;
        }

        public int CooldownInstantCompletePrice
        {
            get; set;
        }
    }
}
