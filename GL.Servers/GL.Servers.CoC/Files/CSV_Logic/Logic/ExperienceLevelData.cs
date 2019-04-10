namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class ExperienceLevelData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExperienceLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExperienceLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public int ExpPoints
        {
            get; set;
        }
    }
}
