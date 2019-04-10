namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class LanguageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="LanguageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public LanguageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string LocalizedName
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public string SystemFont
        {
            get; set;
        }

        public string ForcedFontFullName
        {
            get; set;
        }

        public string WordListName
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

        public string HelpshiftMetadataLanguage
        {
            get; set;
        }
    }
}
