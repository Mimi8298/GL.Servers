namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class NewData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NewData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NewData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
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

        public bool EnabledAndroid
        {
            get; set;
        }

        public bool EnabledKunlun
        {
            get; set;
        }

        public bool EnabledTencent
        {
            get; set;
        }

        public bool EnabledLowEnd
        {
            get; set;
        }

        public bool EnabledHighEnd
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public bool ShowAsNew
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string ButtonTID
        {
            get; set;
        }

        public string ActionType
        {
            get; set;
        }

        public string ActionParameter1
        {
            get; set;
        }

        public string ActionParameter2
        {
            get; set;
        }

        public string NativeAndroidURL
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

        public string IncludedLoginCountries
        {
            get; set;
        }

        public string ExcludedLoginCountries
        {
            get; set;
        }

        public bool CenterText
        {
            get; set;
        }

        public bool LoadResources
        {
            get; set;
        }

        public bool LoadInLowEnd
        {
            get; set;
        }

        public string ItemSWF
        {
            get; set;
        }

        public string ItemExportName
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

        public int IconFrame
        {
            get; set;
        }

        public bool AnimateIcon
        {
            get; set;
        }

        public bool CenterIcon
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

        public bool ClickToDismiss
        {
            get; set;
        }

        public bool NotifyAlways
        {
            get; set;
        }

        public int NotifyMinLevel
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

        public bool Collapsed
        {
            get; set;
        }

        public string MinOS
        {
            get; set;
        }

        public string MaxOS
        {
            get; set;
        }

        public string ButtonTID2
        {
            get; set;
        }

        public string Action2Type
        {
            get; set;
        }

        public string Action2Parameter1
        {
            get; set;
        }

        public string Action2Parameter2
        {
            get; set;
        }

        public bool NotBound
        {
            get; set;
        }
    }
}
