namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class SanctuaryEventData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SanctuaryEventData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SanctuaryEventData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SanctuaryAnimal
        {
            get; set;
        }

        public int SanctuaryAnimalWeight
        {
            get; set;
        }
    }
}
