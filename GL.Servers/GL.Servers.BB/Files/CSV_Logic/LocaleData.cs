namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class LocaleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LocaleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LocaleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Description
        {
            get; set;
        }

        public string PreferedFallbackFont
        {
            get; set;
        }

        public string ForcedFontFullName
        {
            get; set;
        }

        public bool TestLanguage
        {
            get; set;
        }

        public string HelpshiftMetaDataLanguage
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

        public string MaintenanceUrl
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
    }
}
