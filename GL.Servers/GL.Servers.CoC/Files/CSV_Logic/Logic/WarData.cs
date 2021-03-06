namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class WarData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="WarData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public WarData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public int TeamSize
        {
            get; set;
        }

        public int PreparationMinutes
        {
            get; set;
        }

        public int WarMinutes
        {
            get; set;
        }

        public bool DisableProduction
        {
            get; set;
        }

        public bool AllowArrangedWar
        {
            get; set;
        }
    }
}
