namespace GL.Servers.BB.Logic
{
    internal class Timer
    {
        internal int EndSubTick;

        public Timer()
        {
            // Timer.
        }

        /// <summary>
        /// Gets the remaining time before the end.
        /// </summary>
        /// <param name="Time">The game time.</param>
        /// <returns>The time in ms.</returns>
        internal int GetRemainingMS(Time Time)
        {
            int RemainingSubTick = this.EndSubTick - Time.SubTick;
            int MS = 1000 * (RemainingSubTick / 60);

            if (RemainingSubTick % 60 > 0)
                MS += 2133 * (RemainingSubTick % 60) >> 7;

            return MS;
        }

        /// <summary>
        /// Gets the remaining time before the end.
        /// </summary>
        /// <param name="Time">The game time.</param>
        /// <returns>The time in seconds.</returns>
        internal int GetRemainingSeconds(Time Time)
        {
            int RemainingSubTick = this.EndSubTick - Time.SubTick;

            if (RemainingSubTick > 0)
            {
                return Math.Max(
                    (int) ((uint) (((0xFFFFFFFF88888889 * (ulong) (RemainingSubTick + 59)) >> 32) + (ulong) RemainingSubTick + 59) >> 31)
                    + ((int) (((0xFFFFFFFF88888889 * (ulong) (RemainingSubTick + 59)) >> 32) + (ulong) RemainingSubTick + 59) >> 5),
                    1);
            }

            return 0;
        }

        /// <summary>
        /// Increases the end time.
        /// </summary>
        /// <param name="Seconds">The increase count.</param>
        internal void IncreaseTimer(int Seconds)
        {
            this.EndSubTick += 60 * Seconds;
        }

        /// <summary>
        /// Skips the specified time.
        /// </summary>
        /// <param name="seconds">The time in seconds.</param>
        internal void FastForward(int seconds)
        {
            this.EndSubTick -= 60 * seconds;
        }

        /// <summary>
        /// Stats the timer.
        /// </summary>
        /// <param name="Seconds">The time before the end.</param>
        /// <param name="Time">The game time.</param>
        internal void StartTimer(int Seconds, Time Time)
        {
            this.EndSubTick = Time.SubTick + 60 * Seconds;
        }
    }
}