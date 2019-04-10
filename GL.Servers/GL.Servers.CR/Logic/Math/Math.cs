namespace GL.Servers.CR.Logic
{
    public static class Math
    {
        private static readonly byte[] SQRT_TABLE =
        {
            0x00, 0x10, 0x16, 0x1B, 0x20, 0x23, 0x27, 0x2A, 0x2D,
            0x30, 0x32, 0x35, 0x37, 0x39, 0x3B, 0x3D, 0x40, 0x41,
            0x43, 0x45, 0x47, 0x49, 0x4B, 0x4C, 0x4E, 0x50, 0x51,
            0x53, 0x54, 0x56, 0x57, 0x59, 0x5A, 0x5B, 0x5D, 0x5E,
            0x60, 0x61, 0x62, 0x63, 0x65, 0x66, 0x67, 0x68, 0x6A,
            0x6B, 0x6C, 0x6D, 0x6E, 0x70, 0x71, 0x72, 0x73, 0x74,
            0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D,
            0x7E, 0x80, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86,
            0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
            0x90, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x96,
            0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9B, 0x9C, 0x9D, 0x9E,
            0x9F, 0xA0, 0xA0, 0xA1, 0xA2, 0xA3, 0xA3, 0xA4, 0xA5,
            0xA6, 0xA7, 0xA7, 0xA8, 0xA9, 0xAA, 0xAA, 0xAB, 0xAC,
            0xAD, 0xAD, 0xAE, 0xAF, 0xB0, 0xB0, 0xB1, 0xB2, 0xB2,
            0xB3, 0xB4, 0xB5, 0xB5, 0xB6, 0xB7, 0xB7, 0xB8, 0xB9,
            0xB9, 0xBA, 0xBB, 0xBB, 0xBC, 0xBD, 0xBD, 0xBE, 0xBF,
            0xC0, 0xC0, 0xC1, 0xC1, 0xC2, 0xC3, 0xC3, 0xC4, 0xC5,
            0xC5, 0xC6, 0xC7, 0xC7, 0xC8, 0xC9, 0xC9, 0xCA, 0xCB,
            0xCB, 0xCC, 0xCC, 0xCD, 0xCE, 0xCE, 0xCF, 0xD0, 0xD0,
            0xD1, 0xD1, 0xD2, 0xD3, 0xD3, 0xD4, 0xD4, 0xD5, 0xD6,
            0xD6, 0xD7, 0xD7, 0xD8, 0xD9, 0xD9, 0xDA, 0xDA, 0xDB,
            0xDB, 0xDC, 0xDD, 0xDD, 0xDE, 0xDE, 0xDF, 0xE0, 0xE0,
            0xE1, 0xE1, 0xE2, 0xE2, 0xE3, 0xE3, 0xE4, 0xE5, 0xE5,
            0xE6, 0xE6, 0xE7, 0xE7, 0xE8, 0xE8, 0xE9, 0xEA, 0xEA,
            0xEB, 0xEB, 0xEC, 0xEC, 0xED, 0xED, 0xEE, 0xEE, 0xEF,
            0xF0, 0xF0, 0xF1, 0xF1, 0xF2, 0xF2, 0xF3, 0xF3, 0xF4,
            0xF4, 0xF5, 0xF5, 0xF6, 0xF6, 0xF7, 0xF7, 0xF8, 0xF8,
            0xF9, 0xF9, 0xFA, 0xFA, 0xFB, 0xFB, 0xFC, 0xFC, 0xFD,
            0xFD, 0xFE, 0xFE, 0xFF
        };

        private static readonly int[] SIN_TABLE =
        {
            0x00, 0x12, 0x24, 0x36, 0x47, 0x59, 0x6B, 0x7D, 0x8F,
            0xA0, 0xB2, 0xC3, 0xD5, 0xE6, 0xF8, 0x109, 0x11A, 0x12B,
            0x13C, 0x14D, 0x15E, 0x16F, 0x180, 0x190, 0x1A0, 0x1B1,
            0x1C1, 0x1D1, 0x1E1, 0x1F0, 0x200, 0x20F, 0x21F, 0x22E,
            0x23D, 0x24B, 0x25A, 0x268, 0x276, 0x284, 0x292, 0x2A0,
            0x2AD, 0x2BA, 0x2C7, 0x2D4, 0x2E1, 0x2ED, 0x2F9, 0x305,
            0x310, 0x31C, 0x327, 0x332, 0x33C, 0x347, 0x351, 0x35B,
            0x364, 0x36E, 0x377, 0x380, 0x388, 0x390, 0x398, 0x3A0,
            0x3A7, 0x3AF, 0x3B5, 0x3BC, 0x3C2, 0x3C8, 0x3CE, 0x3D3,
            0x3D8, 0x3DD, 0x3E2, 0x3E6, 0x3EA, 0x3ED, 0x3F0, 0x3F3,
            0x3F6, 0x3F8, 0x3FA, 0x3FC, 0x3FE, 0x3FF, 0x3FF, 0x400,
            0x400
        };

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        internal static int Abs(int a)
        {
            if (a < 0)
                return -a;
            return a;
        }

        /// <summary>
        /// Returns the number with the highest value.
        /// </summary>
        /// <returns></returns>
        internal static int Max(int a, int b)
        {
            if (a <= b)
                return b;
            return a;
        }

        /// <summary>
        /// Returns the number with the lowest value.
        /// </summary>
        internal static int Min(int a, int b)
        {
            if (a >= b)
                return b;
            return a;
        }

        /// <summary>
        /// Returns the square root of x.
        /// </summary>
        internal static int Sqrt(int Value)
        {
            int v6;
            int v9;
            int Result;

            if (Value < 0x10000)
            {
                if (Value < 0x100)
                {
                    if (Value < 0)
                        Result = -1;
                    else
                        Result = Math.SQRT_TABLE[Value] >> 4;
                }
                else
                {
                    if (Value < 4096)
                    {
                        if (Value < 1024)
                            v9 = (Math.SQRT_TABLE[Value >> 2] >> 3) + 1;
                        else
                            v9 = (Math.SQRT_TABLE[Value >> 4] >> 2) + 1;
                    }
                    else if (Value < 0x4000)
                    {
                        v9 = (Math.SQRT_TABLE[Value >> 6] >> 1) + 1;
                    }
                    else
                    {
                        v9 = Math.SQRT_TABLE[Value >> 8] + 1;
                    }

                    Result = v9 * v9 <= Value ? v9 : v9 - 1;
                }
            }
            else if (Value < 0x1000000)
            {
                if (Value < 0x100000)
                {
                    if (Value < 0x40000)
                        v9 = 2 * Math.SQRT_TABLE[Value >> 10];
                    else
                        v9 = 4 * Math.SQRT_TABLE[Value >> 12];
                }
                else if (Value < 0x400000)
                {
                    v9 = 8 * Math.SQRT_TABLE[Value >> 14];
                }
                else
                {
                    v9 = 16 * Math.SQRT_TABLE[Value >> 16];
                }

                v6 = (Value / v9 + v9 + 1) >> 1;
                Result = v6 * v6 <= Value ? v6 : v6 - 1;
            }
            else
            {
                if (Value < 0x10000000)
                {
                    if (Value < 0x4000000)
                        v9 = 32 * Math.SQRT_TABLE[Value >> 18];
                    else
                        v9 = Math.SQRT_TABLE[Value >> 20] << 6;
                }
                else if (Value < 0x40000000)
                {
                    v9 = Math.SQRT_TABLE[Value >> 22] << 7;
                }
                else
                {
                    if (Value >= 0x7FFFFFFF)
                        return 0xFFFF;
                    v9 = Math.SQRT_TABLE[Value >> 24] << 8;
                }

                v6 = (Value / ((Value / v9 + v9 + 1) >> 1) + ((Value / v9 + v9 + 1) >> 1) + 1) >> 1;
                Result = v6 * v6 <= Value ? v6 : v6 - 1;
            }

            return Result;
        }

        internal static int GetRotatedX(int X, int Y, int Degrees)
        {
            long v4 = ((-1240768329L * (Degrees + 90)) >> 32) + Degrees + 90;
            long v5 = Degrees + 90 - 360 * ((v4 >> 8) + (v4 >> 31));
            int v6 = (int)(v5 + (-(v5 < 0 ? 1 : 0) & 0x168));

            int SinValue;

            if (v6 > 179)
            {
                int v8 = v6 - 180;
                if (v6 - 180 > 90)
                    v8 = 360 - v6;

                SinValue = -Math.SIN_TABLE[v8];
            }
            else
            {
                if (v6 > 90)
                    v6 = 180 - v6;

                SinValue = Math.SIN_TABLE[v6];
            }

            int v9 = X * SinValue;
            int v10 = Degrees % 360 + (-(Degrees % 360 < 0 ? 1 : 0) & 0x168);

            if (v10 > 179)
            {
                int v12 = v10 - 180;
                if (v10 - 180 > 90)
                    v12 = 360 - v10;

                SinValue = -Math.SIN_TABLE[v12];
            }
            else
            {
                if (v10 > 90)
                    v10 = 180 - v10;

                SinValue = Math.SIN_TABLE[v10];
            }

            return (v9 - SinValue * Y) >> 10;
        }

        internal static int GetRotatedY(int X, int Y, int Degrees)
        {
            int v4 = Degrees % 360;

            if (Degrees % 360 < 0)
                v4 += 360;

            int SinValue;

            if (v4 > 179)
            {
                int v6 = v4 - 180;

                if (v4 - 180 > 90)
                    v6 = 360 - v4;

                SinValue = -Math.SIN_TABLE[v6];
            }
            else
            {
                if (v4 > 90)
                    v4 = 180 - v4;

                SinValue = Math.SIN_TABLE[v4];
            }

            int v7 = X * SinValue;
            int v8 = (Degrees + 90) % 360 + (-((Degrees + 90) % 360 < 0 ? 1 : 0) & 0x168);

            if (v8 > 179)
            {
                int v10 = v8 - 180;

                if (v8 - 180 > 90)
                    v10 = 360 - v8;

                SinValue = -Math.SIN_TABLE[v10];
            }
            else
            {
                if (v8 > 90)
                    v8 = 180 - v8;

                SinValue = Math.SIN_TABLE[v8];
            }
            return (v7 + SinValue * Y) >> 10;
        }
    }
}