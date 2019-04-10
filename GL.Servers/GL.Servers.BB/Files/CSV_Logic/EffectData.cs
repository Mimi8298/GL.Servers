namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class EffectData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EffectData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EffectData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Sound
        {
            get; set;
        }

        public int Volume
        {
            get; set;
        }

        public int MinPitch
        {
            get; set;
        }

        public int MaxPitch
        {
            get; set;
        }

        public string ParticleEmitter
        {
            get; set;
        }

        public int CameraShake
        {
            get; set;
        }

        public bool AttachToParent
        {
            get; set;
        }

        public string IsoLayer
        {
            get; set;
        }

        public bool Targeted
        {
            get; set;
        }

        public bool IgnoreTargetedScaling
        {
            get; set;
        }

        public int StartOffset
        {
            get; set;
        }

        public bool UseFlashOffset
        {
            get; set;
        }

        public bool IsLaser
        {
            get; set;
        }

        public bool PlayDuringFastForward
        {
            get; set;
        }

        public string StatusEffectTarget
        {
            get; set;
        }

        public int StatusEffectTargetClass
        {
            get; set;
        }

        public string StatusEffectType
        {
            get; set;
        }

        public string UnitState
        {
            get; set;
        }

        public int StatusEffectPriority
        {
            get; set;
        }

        public bool StatusEffectOverride
        {
            get; set;
        }

        public bool TriggerOnStatusRefresh
        {
            get; set;
        }

        public int StatusEffectTriggerRateMs
        {
            get; set;
        }

        public int StatusEffectChance
        {
            get; set;
        }

        public string ColorEffectType
        {
            get; set;
        }

        public int ColorEffectLengthMs
        {
            get; set;
        }

        public string Color1Add
        {
            get; set;
        }

        public string Color2Add
        {
            get; set;
        }

        public string Color1Mul
        {
            get; set;
        }

        public string Color2Mul
        {
            get; set;
        }

        public int ColorEffectPhase
        {
            get; set;
        }

        public int ColorEffectSpeed
        {
            get; set;
        }

        public int ShakeEffectLengthMs
        {
            get; set;
        }

        public int ShakeStrengthX
        {
            get; set;
        }

        public int ShakeStrengthY
        {
            get; set;
        }

        public int ShakeSpeedX
        {
            get; set;
        }

        public int ShakeSpeedY
        {
            get; set;
        }
    }
}
