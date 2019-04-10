namespace GL.Servers.Extensions
{
    internal static class Extensions
    {
        /// <summary>
        /// Turn two integer into a single long.
        /// </summary>
        /// <param name="Pair">The pair.</param>
        public static long ToInt64(this (int, int) Pair)
        {
            return (long) Pair.Item1 << 32 | (long)(uint)Pair.Item2;
        }

        /// <summary>
        /// Rotate the int.
        /// </summary>
        /// <param name="value">The int</param>
        /// <param name="count">Rotation Count</param>
        /// <returns></returns>
        public static int RotateRight(int value, int count)
        {
            return value << count | value >> (32 - count);
        }

        /// <summary>
        /// Get index of the value in array. Return -1 if value is not in array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Array">The Array</param>
        /// <param name="Value">The Value</param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] Array, T Value)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i].Equals(Value))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}