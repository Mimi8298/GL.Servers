namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PeopleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PeopleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PeopleData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string PopupExportName
        {
            get; set;
        }

        public int WalkSpeed
        {
            get; set;
        }

        public int IdleTime
        {
            get; set;
        }

        public string Greeting
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }
    }
}
