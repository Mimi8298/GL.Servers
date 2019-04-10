namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class SpecialMoveData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SpecialMoveData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SpecialMoveData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public int Cooldown
        {
            get; set;
        }

        public int Attributes
        {
            get; set;
        }

        public bool ExecuteBeforeMoves
        {
            get; set;
        }

        public string Effect
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public int DelayMillis
        {
            get; set;
        }
    }
}
