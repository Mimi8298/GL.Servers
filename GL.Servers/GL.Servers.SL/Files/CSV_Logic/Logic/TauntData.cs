namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class TauntData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TauntData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TauntData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Cost
        {
            get; set;
        }

        public string TauntText
        {
            get; set;
        }

        public string CharacterUnlock
        {
            get; set;
        }

        public string Resource
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public bool UnlockedInBeginning
        {
            get; set;
        }

        public string ParamType
        {
            get; set;
        }
    }
}
