namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class GlobalData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GlobalData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int NumberValue
        {
            get; set;
        }

        public bool BooleanValue
        {
            get; set;
        }

        public string TextValue
        {
            get; set;
        }

        public List<int> NumberArray
        {
            get; set;
        }

        public List<string> StringArray
        {
            get; set;
        }
    }
}
