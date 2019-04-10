namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Skins : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Skins"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Skins(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string Character
        {
            get; set;
        }

        public string PetSkin
        {
            get; set;
        }

        public int CostGems
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string Model
        {
            get; set;
        }

        public string BlueTexture
        {
            get; set;
        }

        public string RedTexture
        {
            get; set;
        }

        public string BlueSpecular
        {
            get; set;
        }

        public string RedSpecular
        {
            get; set;
        }

        public string IdleAnim
        {
            get; set;
        }

        public string WalkAnim
        {
            get; set;
        }

        public string PrimarySkillAnim
        {
            get; set;
        }

        public string SecondarySkillAnim
        {
            get; set;
        }

        public string PrimarySkillRecoilAnim
        {
            get; set;
        }

        public string PrimarySkillRecoilAnim2
        {
            get; set;
        }

        public string SecondarySkillRecoilAnim
        {
            get; set;
        }

        public string SecondarySkillRecoilAnim2
        {
            get; set;
        }

        public string ReloadingAnim
        {
            get; set;
        }

        public string PushbackAnim
        {
            get; set;
        }

        public string HappyAnim
        {
            get; set;
        }

        public string HappyAnimLoop
        {
            get; set;
        }

        public string SadAnim
        {
            get; set;
        }

        public string SadAnimLoop
        {
            get; set;
        }

        public string SignatureAnim
        {
            get; set;
        }

        public string UiPoseAnim
        {
            get; set;
        }

        public string IdleFace
        {
            get; set;
        }

        public string WalkFace
        {
            get; set;
        }

        public string HappyFace
        {
            get; set;
        }

        public string SadFace
        {
            get; set;
        }

        public string SignatureFace
        {
            get; set;
        }

        public string UiPoseFace
        {
            get; set;
        }

        public int HeadRotationIngame
        {
            get; set;
        }

        public int HatScaleIngame
        {
            get; set;
        }
    }
}
