namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class LobsterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LobsterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LobsterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ToolIconExportName
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public string Good
        {
            get; set;
        }

        public int JacuzziTimeSec
        {
            get; set;
        }

        public string StateIdleExportName
        {
            get; set;
        }

        public int MinSecNextIdle
        {
            get; set;
        }

        public int MaxSecNextIdle
        {
            get; set;
        }

        public string StateReadyExportName
        {
            get; set;
        }

        public int MinSecNextReady
        {
            get; set;
        }

        public int MaxSecNextReady
        {
            get; set;
        }

        public string StateHarvestExportName
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string DropSound
        {
            get; set;
        }

        public string CollectSound
        {
            get; set;
        }

        public string SlideSound
        {
            get; set;
        }
    }
}
