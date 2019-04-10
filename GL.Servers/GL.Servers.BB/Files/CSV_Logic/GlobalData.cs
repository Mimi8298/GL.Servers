using System.Collections.Generic;

namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

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
