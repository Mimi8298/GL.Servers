namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class LocaleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LocaleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LocaleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string Description
        {
            get; set;
        }

        public bool HasEvenSpaceCharacters
        {
            get; set;
        }

        public bool isRTL
        {
            get; set;
        }

        public string UsedSystemFont
        {
            get; set;
        }

        public string HelpshiftSDKLanguage
        {
            get; set;
        }

        public string HelpshiftSDKLanguageAndroid
        {
            get; set;
        }

        public int SortOrder
        {
            get; set;
        }

        public bool TestLanguage
        {
            get; set;
        }

        public string TestExcludes
        {
            get; set;
        }

        public bool BoomboxEnabled
        {
            get; set;
        }

        public string BoomboxUrl
        {
            get; set;
        }

        public string BoomboxStagingUrl
        {
            get; set;
        }

        public string HelpshiftLanguageTagOverride
        {
            get; set;
        }

        public string ForcedFontName
        {
            get; set;
        }
    }
}
