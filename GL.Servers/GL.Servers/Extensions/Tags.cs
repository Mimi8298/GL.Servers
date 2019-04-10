namespace GL.Servers.Extensions
{
    using System;
    using System.Linq;
    using System.Diagnostics;

    public class Tags
    {
        internal static readonly char[] SEARCH_TAG_CHARS = "0289PYLQGRJCUV".ToCharArray();
        internal const int CHARS_LENGTH = 14;

        public static string ToCode(int HighId, int LowId)
        {
            if (HighId <= 255)
            {
                string Code = string.Empty;
                long Id = HighId | LowId << 8;

                for (int i = 11; i >= 0; --i)
                {
                    Code = Code + SEARCH_TAG_CHARS[(int)(Id % CHARS_LENGTH)];
                    Id /= CHARS_LENGTH;

                    if (Id <= 0)
                    {
                        return "#" + new string(Code.Reverse().ToArray());
                    }
                }

                return "#" + new string(Code.Reverse().ToArray());
            }

            Debug.WriteLine("Cannot convert the code to string. Higher int value too large");

            return string.Empty;
        }

        public static long ToId(string Code)
        {
            if (Code.Length <= 14)
            {
                if (Code.Length > 0)
                {
                    long Id = 0;

                    for (int i = Code[0] == '#' ? 1 : 0; i < Code.Length; i++)
                    {
                        int Index = SEARCH_TAG_CHARS.IndexOf(Char.ToUpper(Code[i]));

                        if (Index > -1)
                        {
                            Id = Id * CHARS_LENGTH + Index;
                        }
                        else
                        {
                            Debug.WriteLine("Cannot convert the string to code. String contains invalid character(s).");
                            return -1;
                        }
                    }

                    return Id % 256 << 32 | Id >> 8;
                }

                return 0;
            }

            Debug.WriteLine("Cannot convert the string to code. String is too long.");

            return -1;
        }

        public static bool ToId(string Code, out int HighId, out int LowId)
        {
            if (Code.Length <= 14)
            {
                long Id = 0;

                for (int i = 1; i < Code.Length; i++)
                {
                    int Index = SEARCH_TAG_CHARS.IndexOf(Code[i]);

                    if (Index > -1)
                        Id = Id * CHARS_LENGTH + Index;
                    else
                    {
                        Debug.WriteLine("Cannot convert the string to code. String contains invalid character(s).");
                        goto Error;
                    }
                }

                HighId = (int)(Id % 256);
                LowId  = (int)(Id >> 8);

                return true;
            }

            Debug.WriteLine("Cannot convert the string to code. String is too long.");

            Error:

            HighId = -1;
            LowId  = -1;

            return false;
        }
    }
}