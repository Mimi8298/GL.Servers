namespace GL.Servers.SP.Logic
{
    internal class Time
    {
        internal int StartTimeStamp;
        internal int ClientSubTick;

        internal int TotalMS
        {
            get
            {
                int MS = 1000 * (this.ClientSubTick / 20);

                if (this.ClientSubTick % 20 > 0)
                {
                    MS += 6400 * this.ClientSubTick >> 7;
                }

                return MS;
            }
        }

        internal int TotalSecs
        {
            get
            {
                return this.ClientSubTick / 20 + (this.ClientSubTick % 20 > 0 ? 1 : 0);
            }
        }

        internal int TimeStamp
        {
            get
            {
                return this.StartTimeStamp + this.TotalSecs;
            }
        }

        internal static int GetSecondsInTicks(int Seconds)
        {
            return 20 * Seconds;
        }
    }
}