namespace GL.Servers.BB.Extensions.Utils
{
    using System;

    internal class TileUtil
    {
        internal static readonly DateTime UnixDateTime = new DateTime(1970, 1, 1);

        internal static int Timestamp
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(UnixDateTime).TotalSeconds;
            }
        }

        internal static long TimestampMS
        {
            get
            {
                return (long) DateTime.UtcNow.Subtract(UnixDateTime).TotalMilliseconds;
            }
        }

        internal static int GetTimestamp(DateTime dateTime)
        {
            return (int) dateTime.Subtract(UnixDateTime).TotalSeconds;
        }

        internal static long GetTimestampMS(DateTime dateTime)
        {
            return (long) dateTime.Subtract(UnixDateTime).TotalMilliseconds;
        }
    }
}