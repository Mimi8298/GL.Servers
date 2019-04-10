namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Items : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Items(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameEnemy
        {
            get; set;
        }

        public string ShadowExportName
        {
            get; set;
        }

        public string GroundGlowExportName
        {
            get; set;
        }

        public string LoopingEffect
        {
            get; set;
        }

        public int Value
        {
            get; set;
        }

        public int Value2
        {
            get; set;
        }

        public int TriggerRangeSubTiles
        {
            get; set;
        }

        public string TriggerAreaEffect
        {
            get; set;
        }

        public bool CanBePickedUp
        {
            get; set;
        }

        public string SpawnEffect
        {
            get; set;
        }
    }
}
