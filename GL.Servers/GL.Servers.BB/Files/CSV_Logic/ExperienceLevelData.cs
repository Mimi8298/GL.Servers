namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ExperienceLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExperienceLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExperienceLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int ExpPoints
        {
            get; set;
        }

        public int MaxOutposts
        {
            get; set;
        }

        public int AttackCost
        {
            get; set;
        }
    }
}
