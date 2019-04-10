namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class ResourceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ResourceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ResourceData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string TID
        {
            get; set;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal bool UsedInBattle
        {
            get; set;
        }

        internal string CollectEffect
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal bool PremiumCurrency
        {
            get; set;
        }

        internal string CapFullTID
        {
            get; set;
        }

        internal int TextRed
        {
            get; set;
        }

        internal int TextGreen
        {
            get; set;
        }

        internal int TextBlue
        {
            get; set;
        }

        internal int Cap
        {
            get; set;
        }

        internal string IconFile
        {
            get; set;
        }

        internal string ShopIcon
        {
            get; set;
        }

    }
}