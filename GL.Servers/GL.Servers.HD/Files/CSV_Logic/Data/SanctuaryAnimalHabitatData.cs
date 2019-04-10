namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class SanctuaryAnimalHabitatData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SanctuaryAnimalHabitatData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SanctuaryAnimalHabitatData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ShopIconExportName
        {
            get; set;
        }

        public string Feed
        {
            get; set;
        }

        public int FeedAmount
        {
            get; set;
        }

        public int Capacity
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public string CollectIconExportName
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string MaxHabitatsColumnName
        {
            get; set;
        }

        public string BuySound
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

        public string WhistleSound
        {
            get; set;
        }

        public string ShopDescriptionTID
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

        public int ReputationUnlockLevel
        {
            get; set;
        }

        public string EffectExportName
        {
            get; set;
        }

        public int CollectIconOffsetX
        {
            get; set;
        }

        public int CollectIconOffsetY
        {
            get; set;
        }

        public int FeedTileOffsetX
        {
            get; set;
        }

        public int FeedTileOffsetY
        {
            get; set;
        }
    }
}
