namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TrainData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TrainData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TrainData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string IconName
        {
            get; set;
        }

        public string TID
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

        public int StopX
        {
            get; set;
        }

        public int StopY
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string ChooSound
        {
            get; set;
        }

        public string WhistleSound
        {
            get; set;
        }

        public int WheelCircum
        {
            get; set;
        }

        public string ChooEffectExportName
        {
            get; set;
        }

        public int StoppedTime
        {
            get; set;
        }

        public int MoveTime
        {
            get; set;
        }

        public int MoveDistanceX
        {
            get; set;
        }

        public string Carts
        {
            get; set;
        }
    }
}
