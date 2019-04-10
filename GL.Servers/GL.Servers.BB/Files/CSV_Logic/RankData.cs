namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class RankData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="RankData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public RankData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public int VP
        {
            get; set;
        }

        public int Diamond
        {
            get; set;
        }

        public int Gold
        {
            get; set;
        }

        public int Wood
        {
            get; set;
        }

        public int Stone
        {
            get; set;
        }

        public int Iron
        {
            get; set;
        }

        public int CommonPiece
        {
            get; set;
        }

        public int RarePiece
        {
            get; set;
        }

        public int Intel
        {
            get; set;
        }

        public int Ticket
        {
            get; set;
        }
    }
}
