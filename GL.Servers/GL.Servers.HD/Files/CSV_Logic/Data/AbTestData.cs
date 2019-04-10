namespace GL.Servers.HD.Files.CSV_Logic.Data
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class AbTestData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AbTestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AbTestData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string RowName
        {
            get; set;
        }

        public string ColumnName
        {
            get; set;
        }

        public List<int> ArrayIndex
        {
            get; set;
        }

        public int NewIntValue
        {
            get; set;
        }

        public string NewStringValue
        {
            get; set;
        }

        public bool NewBooleanValue
        {
            get; set;
        }

        public int Group
        {
            get; set;
        }

        public int Percentage
        {
            get; set;
        }

        public bool iOSOnly
        {
            get; set;
        }

        public bool AndroidOnly
        {
            get; set;
        }

        public bool SpendersOnly
        {
            get; set;
        }

        public bool NonSpendersOnly
        {
            get; set;
        }
    }
}
