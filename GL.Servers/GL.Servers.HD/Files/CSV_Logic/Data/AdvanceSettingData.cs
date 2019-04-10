namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AdvanceSettingData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AdvanceSettingData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AdvanceSettingData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public int MinLevel
        {
            get; set;
        }

        public bool DefaultValue
        {
            get; set;
        }

        public bool UserConfigDisabled
        {
            get; set;
        }

        public string Group
        {
            get; set;
        }

        public string HeadlineTID
        {
            get; set;
        }
    }
}
