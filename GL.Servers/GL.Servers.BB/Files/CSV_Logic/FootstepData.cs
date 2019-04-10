namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class FootstepData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="FootstepData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public FootstepData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int YOffset
        {
            get; set;
        }

        public int SpacingBetweenFeet
        {
            get; set;
        }

        public int IntervalTicks
        {
            get; set;
        }

        public int LifeTimeTicks
        {
            get; set;
        }
    }
}
