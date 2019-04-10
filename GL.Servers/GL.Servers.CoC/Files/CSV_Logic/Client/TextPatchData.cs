namespace GL.Servers.CoC.Files.CSV_Logic.Client
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class TextPatchData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TextPatchData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TextPatchData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            
        }

        public string TID
        {
            get; set;
        }

        public string EN
        {
            get; set;
        }

        public string FR
        {
            get; set;
        }

        public string DE
        {
            get; set;
        }

        public string ES
        {
            get; set;
        }

        public string IT
        {
            get; set;
        }

        public string NL
        {
            get; set;
        }

        public string NO
        {
            get; set;
        }

        public string PT
        {
            get; set;
        }

        public string TR
        {
            get; set;
        }

        public string JP
        {
            get; set;
        }

        public string CN
        {
            get; set;
        }

        public string KR
        {
            get; set;
        }

        public string RU
        {
            get; set;
        }

        public string AR
        {
            get; set;
        }

        public string CNT
        {
            get; set;
        }

        public string FA
        {
            get; set;
        }

        public string ID
        {
            get; set;
        }

        public string MS
        {
            get; set;
        }

        public string VI
        {
            get; set;
        }

        public string TH
        {
            get; set;
        }
    }
}
