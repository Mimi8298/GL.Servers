namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class OpenGraphObjectData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphObjectData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public OpenGraphObjectData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Title
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public int GlobalID
        {
            get; set;
        }

        public int ObjectGlobalID
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string ObjectType
        {
            get; set;
        }
    }
}
