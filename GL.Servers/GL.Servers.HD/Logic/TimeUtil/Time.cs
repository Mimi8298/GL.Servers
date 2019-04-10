namespace GL.Servers.HD.Logic
{
    internal class Time
    {
        internal int ClientSubTick;

        internal int TotalMS
        {
            get
            {
                int MS = 1000 * (this.ClientSubTick / 60);

                if (this.ClientSubTick % 60 > 0)
                {
                    MS += (2133 * (this.ClientSubTick % 60)) >> 7;
                }

                return MS;
            }
        }

        internal int TotalSecs
        {
            get
            {
                if (this.ClientSubTick > 0)
                {
                    return Math.Max(
                        (int) (uint) ((((-2004318071L * (this.ClientSubTick + 59) >> 32) + this.ClientSubTick + 59) >> 31)
                                      + ((((-2004318071L * (this.ClientSubTick + 59)) >> 32) + this.ClientSubTick + 59) >> 5)),
                        1);
                }

                return 0;
            }
        }

        internal static int GetSecondsInTicks(int Seconds)
        {
            return Seconds * 60;
        }
    }
}