namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ScoreBoardData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ScoreBoardData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ExportName
        {
            get; set;
        }

        public string FileName
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

        public string CategoryID
        {
            get; set;
        }

        public string CategoryNameTID
        {
            get; set;
        }

        public string ScoreType
        {
            get; set;
        }

        public bool IsGlobal
        {
            get; set;
        }

        public int MaxSize
        {
            get; set;
        }

        public bool ScoreBeatenIndication
        {
            get; set;
        }

        public bool CategoryEnabled
        {
            get; set;
        }
    }
}
