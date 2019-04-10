namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class TutorialData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TutorialData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TutorialData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string TutorialTitleTID
        {
            get; set;
        }

        public string TutorialTextTID
        {
            get; set;
        }

        public int Level
        {
            get; set;
        }

        public int Wave
        {
            get; set;
        }

        public int Turn
        {
            get; set;
        }

        public int DragEvent
        {
            get; set;
        }

        public string NextStep
        {
            get; set;
        }

        public bool ValidateMove
        {
            get; set;
        }

        public int ValidMoveFromX
        {
            get; set;
        }

        public int ValidMoveFromY
        {
            get; set;
        }

        public int ValidMoveToX
        {
            get; set;
        }

        public int ValidMoveToY
        {
            get; set;
        }

        public string DialogPosition
        {
            get; set;
        }

        public string DialogExportName
        {
            get; set;
        }

        public string Puzzle
        {
            get; set;
        }

        public bool BlockTimer
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public int HeroSwap
        {
            get; set;
        }

        public string PuzzleMask
        {
            get; set;
        }

        public string MaskColor
        {
            get; set;
        }
    }
}
