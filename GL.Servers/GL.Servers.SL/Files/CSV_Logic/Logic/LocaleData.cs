namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

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

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }
    }
}
