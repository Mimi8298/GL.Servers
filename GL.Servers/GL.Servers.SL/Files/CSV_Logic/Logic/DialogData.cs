namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class DialogData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DialogData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DialogData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Quest
        {
            get; set;
        }

        public string DialogType
        {
            get; set;
        }

        public string DialogTID
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public bool OwnText
        {
            get; set;
        }

        public string Boss
        {
            get; set;
        }

        public string TakeDmgTaunt
        {
            get; set;
        }

        public string DealDmgTaunt
        {
            get; set;
        }

        public string HitObstacleTaunt
        {
            get; set;
        }

        public string DieTaunt
        {
            get; set;
        }

        public string KillTaunt
        {
            get; set;
        }

        public string SpecialTaunt
        {
            get; set;
        }
    }
}
