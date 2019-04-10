namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class NewspaperLayoutData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="NewspaperLayoutData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public NewspaperLayoutData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string PageA1
        {
            get; set;
        }

        public string PageB1
        {
            get; set;
        }

        public string PageA2
        {
            get; set;
        }

        public string PageB2
        {
            get; set;
        }

        public string PageA3
        {
            get; set;
        }

        public string PageB3
        {
            get; set;
        }

        public string PageA4
        {
            get; set;
        }

        public string PageB4
        {
            get; set;
        }

        public string PageA5
        {
            get; set;
        }

        public string PageB5
        {
            get; set;
        }

        public string PageA6
        {
            get; set;
        }

        public string PageB6
        {
            get; set;
        }

        public string PageA7
        {
            get; set;
        }

        public string PageB7
        {
            get; set;
        }

        public string PageA8
        {
            get; set;
        }

        public string PageB8
        {
            get; set;
        }

        public string PageA9
        {
            get; set;
        }

        public string PageB9
        {
            get; set;
        }

        public string PageA10
        {
            get; set;
        }

        public string PageB10
        {
            get; set;
        }

        public string PageM1
        {
            get; set;
        }

        public string PageM2
        {
            get; set;
        }

        public string PageM3
        {
            get; set;
        }

        public string PageM4
        {
            get; set;
        }

        public string PageM5
        {
            get; set;
        }

        public string PageM6
        {
            get; set;
        }

        public string PageM7
        {
            get; set;
        }

        public string PageM8
        {
            get; set;
        }

        public string PageM9
        {
            get; set;
        }

        public string PageM10
        {
            get; set;
        }

        public string PageM11
        {
            get; set;
        }

        public string PageM12
        {
            get; set;
        }

        public string PageM13
        {
            get; set;
        }

        public string PageM14
        {
            get; set;
        }

        public string PageM15
        {
            get; set;
        }

        public string PageM16
        {
            get; set;
        }

        public string PageM17
        {
            get; set;
        }

        public string PageM18
        {
            get; set;
        }
    }
}
