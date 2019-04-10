namespace GL.Servers.CoC.Logic
{
    internal class Time
    {
        internal int SubTick;

        /// <summary>
        /// Gets a value indicating the total time in millisecs of the state.
        /// </summary>
        internal int TotalMS
        {
            get
            {
                return 16 * this.SubTick; 
            }
        }

        /// <summary>
        /// Gets a value indicating the total time in seconds of the state.
        /// </summary>
        internal int TotalSecs
        {
            get
            {
                if (this.SubTick > 0)
                {
                    return Math.Max((int)(ulong)(16L * this.SubTick / 1000) + 1, 1);
                }

                return 0;
            }
        }
        
        /// <summary>
        /// Converts the seconds in ticks.
        /// </summary>
        internal static int GetSecondsInTicks(int Seconds)
        {
            return (int) ((((uint) ((int) ((ulong) (1000L * Seconds) >> 32) >> 31) >> 38) + 1000L * Seconds) >> 4);
        }
    }
}