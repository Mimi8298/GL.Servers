namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AmbientAnimalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AmbientAnimalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AmbientAnimalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int Behavior
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int SpeedMultiplier
        {
            get; set;
        }
    }
}
