namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class BoomboxData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BoomboxData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BoomboxData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // BoomboxData.
        }

        public bool Enabled
        {
            get; set;
        }

        public bool EnabledLowMem
        {
            get; set;
        }

        public string DisabledDevices
        {
            get; set;
        }

        public bool PreLoading
        {
            get; set;
        }

        public bool PreLoadingLowMem
        {
            get; set;
        }

        public string SupportedPlatforms
        {
            get; set;
        }

        public string SupportedPlatformsVersion
        {
            get; set;
        }

        public string AllowedDomains
        {
            get; set;
        }

        public string AllowedUrls
        {
            get; set;
        }

        public bool ForYouEnabled
        {
            get; set;
        }
    }
}
