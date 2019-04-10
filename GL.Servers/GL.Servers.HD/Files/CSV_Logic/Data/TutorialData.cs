namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

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

        public string Mode
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string TID2
        {
            get; set;
        }

        public string RequiredTutorial
        {
            get; set;
        }

        public string AnimationExportName
        {
            get; set;
        }

        public string AnimationTitleTID
        {
            get; set;
        }

        public string ConfirmButtonTID
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string HumanExportName
        {
            get; set;
        }

        public int DelayMS
        {
            get; set;
        }

        public string AutoCompleteOtherTutorial
        {
            get; set;
        }

        public string ReplayTutorial
        {
            get; set;
        }

        public string Target
        {
            get; set;
        }

        public string ShopTarget
        {
            get; set;
        }

        public string Action
        {
            get; set;
        }

        public string StartAction
        {
            get; set;
        }

        public string EndAction
        {
            get; set;
        }

        public int RequiredCount
        {
            get; set;
        }

        public bool CameraPanning
        {
            get; set;
        }

        public int MinExpLevel10
        {
            get; set;
        }

        public int MaxExpLevel10
        {
            get; set;
        }

        public int MinRepLevel
        {
            get; set;
        }

        public int Priority
        {
            get; set;
        }

        public string PeopleOrder
        {
            get; set;
        }

        public int ArrowXOffset
        {
            get; set;
        }

        public int ArrowYOffset
        {
            get; set;
        }

        public string GoodReward
        {
            get; set;
        }

        public string NetReward
        {
            get; set;
        }

        public int PositionX
        {
            get; set;
        }

        public int PositionY
        {
            get; set;
        }

        public bool Disabled
        {
            get; set;
        }

        public string TargetData
        {
            get; set;
        }
    }
}
