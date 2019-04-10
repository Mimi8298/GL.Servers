namespace GL.Servers.CoC.Logic
{
    internal class Timer
    {
        internal int EndSubTick;

        internal bool Started
        {
            get
            {
                return this.EndSubTick != -1;
            }
        }

        public Timer()
        {
            // Timer.
        }
        
        /// <summary>
        /// Starts the timer.
        /// </summary>
        internal void StartTimer(Time Time, int Seconds)
        {
            this.EndSubTick = Time.SubTick + Time.GetSecondsInTicks(Seconds);
        }

        /// <summary>
        /// Increases the end time.
        /// </summary>
        internal void IncreaseTimer(int Seconds)
        {
            this.EndSubTick += Time.GetSecondsInTicks(Seconds);
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        internal void StopTimer()
        {
            this.EndSubTick = -1;
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForward(int Seconds)
        {
            this.EndSubTick -= Time.GetSecondsInTicks(Seconds);
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForwardSubTicks(int SubTick)
        {
            if (this.EndSubTick > 0)
            {
                this.EndSubTick -= SubTick;
            }
        }

        /// <summary>
        /// Gets the remaining seconds before the end of timer.
        /// </summary>
        internal int GetRemainingSeconds(Time Time)
        {
            int SubTicks = this.EndSubTick - Time.SubTick;

            if (SubTicks > 0)
            {
                return Math.Max((int)(16L * SubTicks / 1000) + 1, 1);
            }

            return 0;
        }

        /// <summary>
        /// Gets the remaining millisecs before the end of timer.
        /// </summary>
        internal int GetRemainingMs(Time Time)
        {
            int SubTicks = this.EndSubTick - Time.SubTick;

            if (SubTicks > 0)
            {
                return 16 * SubTicks;
            }

            return 0;
        }

        /// <summary>
        /// Adjusts the EnSubTick.
        /// </summary>
        internal void AdjustSubTick(Time Time)
        {
            this.EndSubTick -= Time.SubTick;

            if (this.EndSubTick < 0)
            {
                this.EndSubTick = 0;
            }
        }
    }
}