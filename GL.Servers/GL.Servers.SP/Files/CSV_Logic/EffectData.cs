namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

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

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public string ParticleEmitter
        {
            get; set;
        }

        public bool AttachToParent
        {
            get; set;
        }

        public bool Looping
        {
            get; set;
        }

        public int MaxCount
        {
            get; set;
        }

        public int CameraShake
        {
            get; set;
        }
    }
}
