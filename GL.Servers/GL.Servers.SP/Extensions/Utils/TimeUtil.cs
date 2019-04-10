namespace GL.Servers.SP.Extensions.Utils
{
    using System;

    internal class TimeUtil
    {
        internal static readonly DateTime Unix = new DateTime(1970, 1, 1);

        internal static int Timestamp
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(Unix).TotalSeconds;
            }
        }

        internal static long TimestampMS
        {
            get
            {
                return (long) DateTime.UtcNow.Subtract(Unix).TotalMilliseconds;
            }
        }
    }
}
