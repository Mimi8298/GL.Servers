namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class LocaleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LocaleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LocaleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // LocaleData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal bool Enabled
        {
            get; set;
        }

        internal string Description
        {
            get; set;
        }

        internal int SortOrder
        {
            get; set;
        }

        internal bool HasEvenSpaceCharacters
        {
            get; set;
        }

        internal string UsedSystemFont
        {
            get; set;
        }

        internal string HelpshiftSDKLanguage
        {
            get; set;
        }

        internal string HelpshiftSDKLanguageAndroid
        {
            get; set;
        }

        internal string HelpshiftLanguageTag
        {
            get; set;
        }

        internal string TermsAndServiceUrl
        {
            get; set;
        }

        internal string TournamentTermsUrl
        {
            get; set;
        }

        internal string ParentsGuideUrl
        {
            get; set;
        }

        internal string PrivacyPolicyUrl
        {
            get; set;
        }

        internal bool TestLanguage
        {
            get; set;
        }

        internal string TestExcludes
        {
            get; set;
        }

        internal string RegionListFile
        {
            get; set;
        }

        internal bool MaintenanceRoyalBox
        {
            get; set;
        }

        internal string RoyalBoxURL
        {
            get; set;
        }

        internal string RoyalBoxStageURL
        {
            get; set;
        }

        internal string RoyalBoxDevURL
        {
            get; set;
        }

        internal string BoomBoxURL
        {
            get; set;
        }

        internal string EventsBaseURL
        {
            get; set;
        }

        internal string EventsPostURL
        {
            get; set;
        }

        internal string EventsBaseStageURL
        {
            get; set;
        }

        internal string EventsPostStageURL
        {
            get; set;
        }

    }
}