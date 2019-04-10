namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ResourceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string SubtitleTID
        {
            get; set;
        }

        public string CategoryTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string CollectEffect
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string ResourceIconExportName
        {
            get; set;
        }

        public string GroupIconExportName
        {
            get; set;
        }

        public string StealEffect
        {
            get; set;
        }

        public bool BaseResource
        {
            get; set;
        }

        public bool HeroResource
        {
            get; set;
        }

        public int ArtifactType
        {
            get; set;
        }

        public int ArtifactTier
        {
            get; set;
        }

        public int PrototypePart
        {
            get; set;
        }

        public bool PremiumCurrency
        {
            get; set;
        }

        public string HudInstanceName
        {
            get; set;
        }

        public string CapFullTID
        {
            get; set;
        }

        public int TextRed
        {
            get; set;
        }

        public int TextGreen
        {
            get; set;
        }

        public int TextBlue
        {
            get; set;
        }

        public string SoundEffect
        {
            get; set;
        }
    }
}
