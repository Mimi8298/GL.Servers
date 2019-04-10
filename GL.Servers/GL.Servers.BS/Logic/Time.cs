namespace GL.Servers.BS.Logic
{
    internal class Time
    {
        internal int ClientSubTick;

        internal int TotalMS
        {
            get
            {
                int MS = 1000 * (this.ClientSubTick / 20);

                if (this.ClientSubTick % 20 > 0)
                {
                    MS += (2133 * (this.ClientSubTick % 20)) >> 7;
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
                        (int) (uint) ((((-2004318071L * (this.ClientSubTick + 19) >> 32) + this.ClientSubTick + 19) >> 31)
                                      + ((((-2004318071L * (this.ClientSubTick + 19)) >> 32) + this.ClientSubTick + 19) >> 5)),
                        1);
                }

                return 0;
            }
        }

        internal static int GetSecondsInTicks(int Seconds)
        {
            return Seconds * 20;
        }
    }
}