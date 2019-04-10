namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MysteryBoxeData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MysteryBoxeData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MysteryBoxeData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int UnlockLevel
        {
            get; set;
        }

        public int ReputationUnlockLevel
        {
            get; set;
        }

        public string GoodReward
        {
            get; set;
        }

        public int GoodAmount
        {
            get; set;
        }

        public int Probability
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public int RandomNumberMax
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

        public string OpenSound
        {
            get; set;
        }

        public string LockedSound
        {
            get; set;
        }

        public string Mode
        {
            get; set;
        }
    }
}
