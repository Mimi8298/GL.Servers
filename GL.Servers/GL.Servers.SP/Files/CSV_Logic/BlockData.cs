namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class BlockData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BlockData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BlockData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string EmitExportName
        {
            get; set;
        }

        public string BreakEffect
        {
            get; set;
        }

        public string BigBreakEffect
        {
            get; set;
        }

        public string SpawnEffect
        {
            get; set;
        }

        public int ColorIndex
        {
            get; set;
        }

        public string LockedSWF
        {
            get; set;
        }

        public string LockedExportName
        {
            get; set;
        }

        public bool Special
        {
            get; set;
        }

        public string Boost
        {
            get; set;
        }

        public int BombRadius
        {
            get; set;
        }

        public int RowBombRadius
        {
            get; set;
        }

        public int ColumnBombRadius
        {
            get; set;
        }
    }
}
