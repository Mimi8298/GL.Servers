namespace GL.Servers.CoC.Extensions
{
    using GL.Servers.CoC.Logic.Enums;

    internal static class Extension
    {
        internal static ulong Pair(int a, int b)
        {
            return (ulong) a << 32 | (uint) b;
        }

        internal static ulong Pair(uint a, uint b)
        {
            return (ulong)a << 32 | b;
        }

        internal static uint HIDWORD(ulong Value)
        {
            return (uint) (Value >> 32);
        }

        internal static uint LODWORD(ulong Value)
        {
            return (uint)(Value >> 32);
        }

        internal static bool IsEquals<T>(this T[] Array1, T[] Array2)
        {
            if (Array1.Length == Array2.Length)
            {
                for (int i = 0; i < Array1.Length; i++)
                {
                    if (!Array1[i].Equals(Array2[i])) return false;
                }

                return true;
            }

            return false;
        }

        internal static bool Superior(this Role a, Role b)
        {
            if (a == Role.Leader && b != a)
            {
                return false;
            }

            if (a == Role.CoLeader && b != Role.Leader)
            {
                return false;
            }

            if (a == Role.Admin && (b == a || b == Role.Member))
            {
                return false;
            }

            if (a == Role.Member && (b == a))
            {
                return false;
            }

            return true;
        }

        internal static byte[] StringToByteArray(string str)
        {
            byte[] Array = new byte[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                Array[i] = (byte) str[i];
            }

            return Array;
        }
    }
}