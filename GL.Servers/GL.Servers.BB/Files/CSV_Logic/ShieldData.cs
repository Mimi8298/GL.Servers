namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class ShieldData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ShieldData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ShieldData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public string Filename
        {
            get; set;
        }

        public string ExportNameFront
        {
            get; set;
        }

        public string ExportNameBack
        {
            get; set;
        }

        public string DestroyEffect
        {
            get; set;
        }
    }
}
