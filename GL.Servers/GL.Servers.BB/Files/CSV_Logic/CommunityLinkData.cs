namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class CommunityLinkData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CommunityLinkData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CommunityLinkData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int ID
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public bool EnabledIOS
        {
            get; set;
        }

        public bool EnabledGooglePlay
        {
            get; set;
        }

        public bool EnabledKunlun
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string ButtonTID
        {
            get; set;
        }

        public string ButtonURL
        {
            get; set;
        }

        public string NativeIOSURL
        {
            get; set;
        }

        public string IncludedLanguages
        {
            get; set;
        }

        public string ExcludedLanguages
        {
            get; set;
        }

        public string IncludedCountries
        {
            get; set;
        }

        public string ExcludedCountries
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

        public int MinTownHall
        {
            get; set;
        }

        public int MaxTownHall
        {
            get; set;
        }

        public int MinLevel
        {
            get; set;
        }

        public int MaxLevel
        {
            get; set;
        }

        public int MaxDiamonds
        {
            get; set;
        }

        public int AvatarIdModulo
        {
            get; set;
        }

        public int ModuloMin
        {
            get; set;
        }

        public int ModuloMax
        {
            get; set;
        }
    }
}
