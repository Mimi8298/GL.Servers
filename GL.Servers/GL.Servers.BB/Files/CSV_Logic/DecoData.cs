namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class DecoData : Data
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DecoData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameConstruction
        {
            get; set;
        }

        public string BuildCost
        {
            get; set;
        }

        public int RequiredExpLevel
        {
            get; set;
        }

        public int MaxCount
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public string Icon
        {
            get; set;
        }

        public string ExportNameBase
        {
            get; set;
        }

        public int VillagerProbability
        {
            get; set;
        }

        public int EventProbability
        {
            get; set;
        }
    }
}
