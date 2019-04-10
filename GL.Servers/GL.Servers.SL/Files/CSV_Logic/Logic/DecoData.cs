namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class DecoData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DecoData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DecoData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public int Cost
        {
            get; set;
        }

        public string Resource
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

        public string ObjectSWF
        {
            get; set;
        }

        public string ObjectExportName
        {
            get; set;
        }

        public string ShadowSWF
        {
            get; set;
        }

        public string ShadowExportName
        {
            get; set;
        }

        public string IconBGSWF
        {
            get; set;
        }

        public string IconBGExportName
        {
            get; set;
        }

        public string VisualType
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public string CharacterUnlock
        {
            get; set;
        }

        public int CostumeIndex
        {
            get; set;
        }

        public bool Hidden
        {
            get; set;
        }

        public int SortIndex
        {
            get; set;
        }
    }
}
