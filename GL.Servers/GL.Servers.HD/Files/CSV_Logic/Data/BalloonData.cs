namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class BalloonData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BalloonData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BalloonData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string TapSound
        {
            get; set;
        }

        public int SpeedHorizontal
        {
            get; set;
        }

        public int SpeedVertical
        {
            get; set;
        }

        public int TileMinX
        {
            get; set;
        }

        public int TileMaxX
        {
            get; set;
        }

        public int TileMinY
        {
            get; set;
        }

        public int TileMaxY
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
    }
}
