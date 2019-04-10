namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class EventEntrieData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventEntrieData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventEntrieData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string ItemSWF
        {
            get; set;
        }

        public string ItemExportName
        {
            get; set;
        }

        public string UpcomingItemExportName
        {
            get; set;
        }

        public bool LoadSWF
        {
            get; set;
        }

        public string TitleTID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string UpcomingTitleTID
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

        public string ButtonTID
        {
            get; set;
        }

        public string ButtonAction
        {
            get; set;
        }

        public string ButtonActionData
        {
            get; set;
        }

        public string Button2TID
        {
            get; set;
        }

        public string Button2Action
        {
            get; set;
        }

        public string Button2ActionData
        {
            get; set;
        }

        public string ButtonLanguage
        {
            get; set;
        }

        public string ExcludedLanguage
        {
            get; set;
        }
    }
}
