namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class TutorialPeopleOrderData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TutorialPeopleOrderData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TutorialPeopleOrderData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Good
        {
            get; set;
        }

        public int GoodAmount
        {
            get; set;
        }

        public string PeopleName
        {
            get; set;
        }
    }
}
