namespace GL.Servers.SL.Logic
{
    using System;
    
    internal class Timer
    {
        internal Time Time;

        internal int TotalSeconds;
        internal int StartSubTick = -1;

        internal int RemainingSecs
        {
            get
            {
                if (this.TotalSeconds > 0)
                {
                    if (this.StartSubTick != -1)
                    {
                        int Ticks = Time.GetSecondsInTicks(this.TotalSeconds) + this.StartSubTick - this.Time.ClientSubTick;

                        if (Ticks > 0)
                        {
                            return Math.Max(
                                (int)(uint)((((-2004318071L * (Ticks + 59) >> 32) + Ticks + 59) >> 31)
                                              + ((((-2004318071L * (Ticks + 59)) >> 32) + Ticks + 59) >> 5)),
                                1);
                        }

                        return 0;
                    }

                    return this.TotalSeconds;
                }

                return 0;
            }
        }

        internal int RemainingMS
        {
            get
            {
                if (this.TotalSeconds > 0)
                {
                    if (this.StartSubTick != -1)
                    {
                        int Ticks = Time.GetSecondsInTicks(this.TotalSeconds) + this.StartSubTick - this.Time.ClientSubTick;
                        int MS = 1000 * (Ticks / 60);

                        if (Ticks % 60 > 0)
                        {
                            MS += 2133 * (Ticks % 60) >> 7;
                        }

                        return MS;
                    }

                    return this.TotalSeconds;
                }

                return 0;
            }
        }

        internal bool Started
        {
            get
            {
                return this.StartSubTick != -1;
            }
        }

        public Timer()
        {
            // Timer.
        }

        public Timer(Time Time)
        {
            this.Time = Time;
        }

        internal void StartTimer(int Seconds)
        {
            if (this.Time != null)
            {
                this.TotalSeconds += Seconds;

                if (!this.Started)
                {
                    this.StartSubTick = this.Time.ClientSubTick;
                }
            }
            else throw new Exception("Unable to start timer when the 'Time' is null. Start Timer with StartTimer(Time,Secs) method.");
        }

        internal void StartTimer(Time Time, int Seconds)
        {
            this.Time = Time;
            this.TotalSeconds = Seconds;
            this.StartSubTick = this.Time.ClientSubTick;
        }

        internal void StopTimer()
        {
            if (this.Started)
            {
                this.StartSubTick = -1;
                this.TotalSeconds = 0;
            }
        }

        internal void FastForward(int Seconds)
        {
            if (this.StartSubTick != -1)
            {
                if (Seconds >= 0)
                {
                    this.TotalSeconds -= Seconds;
                }
            }
        }

        internal void AdjustSubTick()
        {
            if (this.Started)
            {
                this.TotalSeconds = (Time.GetSecondsInTicks(this.TotalSeconds) + this.StartSubTick) / 60;
                this.StartSubTick = 0;
            }
        }
    }
}