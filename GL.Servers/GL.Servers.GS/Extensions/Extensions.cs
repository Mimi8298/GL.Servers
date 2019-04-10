namespace GL.Servers.GS.Extensions
{
    internal static class Extensions
    {
        /// <summary>
        /// Turn two integer into a single long.
        /// </summary>
        /// <param name="Pair">The pair.</param>
        public static long ToInt64(this (int, int) Pair)
        {
            return (long)Pair.Item1 << 32 | (long)(uint)Pair.Item2;
        }
    }
}