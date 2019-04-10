namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class DeepseaParameterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DeepseaParameterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DeepseaParameterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Probability
        {
            get; set;
        }

        public int Parameter1
        {
            get; set;
        }

        public int Parameter2
        {
            get; set;
        }
    }
}
