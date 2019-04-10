namespace GL.Servers.BB.Logic.Manager
{
    internal class FootstepManager
    {
        internal Level Level;

        /// <summary>
        /// Initializes a new instance of the <see cref="FootstepManager"/> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        public FootstepManager(Level Level)
        {
            this.Level = Level;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            // Tick.
        }

        /// <summary>
        /// To call after initialization or after deserialization.
        /// </summary>
        internal void LoadingFinished()
        {

        }
    }
}