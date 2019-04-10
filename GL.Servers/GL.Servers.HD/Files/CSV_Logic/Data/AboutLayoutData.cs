namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AboutLayoutData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AboutLayoutData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AboutLayoutData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Language
        {
            get; set;
        }

        public string CommunityButtonTID
        {
            get; set;
        }

        public string CommunityButtonLink
        {
            get; set;
        }

        public string CommunityButtonLinkAndroid
        {
            get; set;
        }

        public string CommunityButtonFailbackLink
        {
            get; set;
        }

        public string CommunityButtonIcon
        {
            get; set;
        }

        public string MoreButtonTID
        {
            get; set;
        }

        public string MoreButtonLink
        {
            get; set;
        }

        public string MoreButtonHelpshiftID
        {
            get; set;
        }

        public string MoreButtonIcon
        {
            get; set;
        }
    }
}
