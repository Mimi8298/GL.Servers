namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class SoundData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SoundData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SoundData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileNames
        {
            get; set;
        }

        public int MinVolume
        {
            get; set;
        }

        public int MaxVolume
        {
            get; set;
        }

        public string FileNameLite
        {
            get; set;
        }

        public int MinVolumeLite
        {
            get; set;
        }

        public int MaxVolumeLite
        {
            get; set;
        }

        public int FadeOutRadius
        {
            get; set;
        }

        public int MaxVolumeRadius
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

        public bool IsMusic
        {
            get; set;
        }
    }
}
