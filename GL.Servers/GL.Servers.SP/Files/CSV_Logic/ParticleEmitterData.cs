namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class ParticleEmitterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ParticleEmitterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ParticleEmitterData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int ParticleCount
        {
            get; set;
        }

        public int EmissionTime
        {
            get; set;
        }

        public int MinLife
        {
            get; set;
        }

        public int MaxLife
        {
            get; set;
        }

        public int MinAngle
        {
            get; set;
        }

        public int MaxAngle
        {
            get; set;
        }

        public int MinSpeed
        {
            get; set;
        }

        public int MaxSpeed
        {
            get; set;
        }

        public int StartZ
        {
            get; set;
        }

        public int Gravity
        {
            get; set;
        }

        public int Inertia
        {
            get; set;
        }

        public int StartScale
        {
            get; set;
        }

        public int EndScale
        {
            get; set;
        }

        public int MinRotate
        {
            get; set;
        }

        public int MaxRotate
        {
            get; set;
        }

        public int FadeOutTime
        {
            get; set;
        }

        public int StartRadius
        {
            get; set;
        }

        public int DepthEffect
        {
            get; set;
        }

        public int TargetMagnet
        {
            get; set;
        }

        public string ParticleSWF
        {
            get; set;
        }

        public string ParticleExportName
        {
            get; set;
        }

        public bool OrientToMovement
        {
            get; set;
        }

        public bool OrientToParent
        {
            get; set;
        }

        public bool BounceFromGround
        {
            get; set;
        }

        public bool AdditiveBlend
        {
            get; set;
        }
    }
}
