namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class SkinData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SkinData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SkinData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SkinData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string ExportNameRed
        {
            get; set;
        }

        internal string TopExportName
        {
            get; set;
        }

        internal string TopExportNameRed
        {
            get; set;
        }

        internal string Character
        {
            get; set;
        }

        internal int ValueGems
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal bool IsInUse
        {
            get; set;
        }

        internal string DeathEffect
        {
            get; set;
        }

        internal string DeathEffect2
        {
            get; set;
        }

        internal bool EventSkin
        {
            get; set;
        }

    }
}