namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class TauntData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TauntData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TauntData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // TauntData.
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

        internal bool TauntMenu
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal string BtnExportName
        {
            get; set;
        }

        internal string Sound
        {
            get; set;
        }

    }
}