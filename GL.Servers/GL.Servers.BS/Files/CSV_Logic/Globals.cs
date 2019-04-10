namespace GL.Servers.BS.Files.CSV_Logic
{
    using System.Collections.Generic;

    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Globals : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Globals"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Globals(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
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

        public List<string> StringArray
        {
            get; set;
        }

        public List<int> NumberArray
        {
            get; set;
        }
    }
}
