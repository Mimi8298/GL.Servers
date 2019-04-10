namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

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

        public string ParticleSwf
        {
            get; set;
        }

        public string ParticleExportName
        {
            get; set;
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

        public int StartScale
        {
            get; set;
        }

        public int EndScale
        {
            get; set;
        }

        public bool RandomStartScale
        {
            get; set;
        }

        public int MinHorizAngle
        {
            get; set;
        }

        public int MaxHorizAngle
        {
            get; set;
        }

        public int MinVertAngle
        {
            get; set;
        }

        public int MaxVertAngle
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

        public int MinRotate
        {
            get; set;
        }

        public int MaxRotate
        {
            get; set;
        }

        public bool DisableRotation
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

        public int StartRadiusMax
        {
            get; set;
        }

        public bool ScaleTimeline
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

        public bool OrientToImpact
        {
            get; set;
        }

        public bool BounceFromGround
        {
            get; set;
        }

        public bool StopAnimationAfterBounce
        {
            get; set;
        }

        public bool AdditiveBlend
        {
            get; set;
        }

        public bool UseMovingEmitter
        {
            get; set;
        }

        public int EmitterMinHorizAngle
        {
            get; set;
        }

        public int EmitterMaxHorizAngle
        {
            get; set;
        }

        public int EmitterMinVertAngle
        {
            get; set;
        }

        public int EmitterMaxVertAngle
        {
            get; set;
        }

        public int EmitterMinSpeed
        {
            get; set;
        }

        public int EmitterMaxSpeed
        {
            get; set;
        }

        public int EmitterGravity
        {
            get; set;
        }

        public int EmitterInertia
        {
            get; set;
        }

        public string EmitterIndicatorSwf
        {
            get; set;
        }

        public string EmitterIndicatorExportName
        {
            get; set;
        }
    }
}
