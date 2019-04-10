namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ShieldData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ShieldData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ShieldData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int TimeH
        {
            get; set;
        }

        public int GuardTimeH
        {
            get; set;
        }

        public int Diamonds
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

        public int CooldownS
        {
            get; set;
        }

        public int LockedAboveScore
        {
            get; set;
        }
    }
}
