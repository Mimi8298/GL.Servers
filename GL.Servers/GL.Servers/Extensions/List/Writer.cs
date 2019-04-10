using System.IO;

namespace GL.Servers.Extensions.List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GL.Servers.Core;
    using GL.Servers.Library.ZLib;

    public static class Writer
    {
        /// <summary>
        /// Adds the byte.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddByte(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte) _Value);
        }

        /// <summary>
        /// Adds the int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddInt(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte)(_Value >> 24));
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value));
        }

        /// <summary>
        /// Adds the int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Skip">The skip.</param>
        public static void AddInt(this List<byte> _Packet, int _Value, int _Skip)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse().Skip(_Skip));
        }

        /// <summary>
        /// Adds the long.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddLong(this List<byte> _Packet, long _Value)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        /// <summary>
        /// Adds the long.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Skip">The skip.</param>
        public static void AddLong(this List<byte> _Packet, long _Value, int _Skip)
        {
            _Packet.AddRange(BitConverter.GetBytes(_Value).Reverse().Skip(_Skip));
        }

        /// <summary>
        /// Adds the bool.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">if set to <c>true</c> [value].</param>
        public static void AddBool(this List<byte> _Packet, bool _Value)
        {
            _Packet.Add(_Value ? (byte) 1 : (byte) 0);
        }

        /// <summary>
        /// Adds the booleans.
        /// </summary>
        /// <param name="Packet"></param>
        /// <param name="Booleans"></param>
        public static void AddBools(this List<byte> Packet, params bool[] Booleans)
        {
            byte Boolean    = 0;

            for (var i = 0; i < Booleans.Length; i++)
            {
                bool Bool   = Booleans[i];

                if (Bool)
                {
                    Boolean |= (byte)(1 << i);
                }
            }

            Packet.Add(Boolean);
        }

        /// <summary>
        /// Adds the bytes.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddBytes(this List<byte> Packet, byte[] Array)
        {
            if (Array != null)
            {
                int length = Array.Length;

                if (length > 0)
                {
                    Packet.AddInt(length);
                    Packet.AddRange(Array);
                }
                else Packet.AddInt(0);
            }
            else Packet.AddInt(-1);
        }

        /// <summary>
        /// Adds the string.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddString(this List<byte> _Packet, string _Value)
        {
            if (_Value == null)
            {
                _Packet.AddInt(-1);
            }
            else
            {
                int length = _Value.Length;

                if (length > 0)
                {
                    byte[] _Buffer = Encoding.UTF8.GetBytes(_Value);

                    _Packet.AddInt(_Buffer.Length);
                    _Packet.AddRange(_Buffer);
                }
                else _Packet.AddInt(0);
            }
        }

        /// <summary>
        /// Adds the unsigned short.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddShort(this List<byte> _Packet, short _Value)
        {
            _Packet.Add((byte) (_Value >> 8));
            _Packet.Add((byte) (_Value));
        }

        /// <summary>
        /// Adds the unsigned short.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddUShort(this List<byte> _Packet, ushort _Value)
        {
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value));
        }

        /// <summary>
        /// Adds the unsigned int 24.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddUInt24(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value));
        }

        /// <summary>
        /// Adds the unsigned int 24.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddUInt24(this List<byte> _Packet, uint _Value)
        {
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value));
        }

        /// <summary>
        /// Adds the compressed int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddVInt(this List<byte> Packet, int Value)
        {
            if (Value >= 0)
            {
                if (Value >= 64)
                {
                    if (Value >= 0x2000)
                    {
                        if (Value >= 0x100000)
                        {
                            if (Value >= 0x8000000)
                            {
                                Packet.Add((byte) (Value         & 0x3F | 0x80));
                                Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 13) & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 20) & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 27) & 0xF));

                                return;
                            }

                            Packet.Add((byte) (Value         & 0x3F | 0x80));
                            Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                            Packet.Add((byte) ((Value >> 13) & 0x7F | 0x80));
                            Packet.Add((byte) ((Value >> 20) & 0x7F));

                            return;
                        }

                        Packet.Add((byte) (Value         & 0x3F | 0x80));
                        Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                        Packet.Add((byte) ((Value >> 13) & 0x7F));

                        return;
                    }

                    Packet.Add((byte) (Value        & 0x3F | 0x80));
                    Packet.Add((byte) ((Value >> 6) & 0x7F));

                    return;
                }

                Packet.Add((byte) (Value & 0x3F));
            }
            else
            {
                if (Value <= -64)
                {
                    if (Value <= -0x2000)
                    {
                        if (Value <= -0x100000)
                        {
                            if (Value <= -0x8000000)
                            {
                                Packet.Add((byte) (Value         & 0x3F | 0xC0));
                                Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 13) & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 20) & 0x7F | 0x80));
                                Packet.Add((byte) ((Value >> 27) & 0xF));

                                return;
                            }

                            Packet.Add((byte) (Value         & 0x3F | 0xC0));
                            Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                            Packet.Add((byte) ((Value >> 13) & 0x7F | 0x80));
                            Packet.Add((byte) ((Value >> 20) & 0x7F));

                            return;
                        }

                        Packet.Add((byte) (Value         & 0x3F | 0xC0));
                        Packet.Add((byte) ((Value >> 6)  & 0x7F | 0x80));
                        Packet.Add((byte) ((Value >> 13) & 0x7F));

                        return;
                    }

                    Packet.Add((byte) (Value        & 0x3F | 0xC0));
                    Packet.Add((byte) ((Value >> 6) & 0x7F));
                }
                else
                {
                    Packet.Add((byte) (Value & 0x3F | 0x40));
                }
            }
        }

        /// <summary>
        /// Adds the compressed int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddVInt(this List<byte> _Packet, uint _Value)
        {
            _Packet.AddVInt((int) _Value);
        }

        /// <summary>
        /// Adds the compressed int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Prefix">The prefix.</param>
        public static void AddVInt(this List<byte> _Packet, int _Value, int _Prefix)
        {
            _Packet.AddVInt(_Prefix);
            _Packet.AddVInt(_Value);
        }

        /// <summary>
        /// Adds the compressed data.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddCompressed(this List<byte> _Packet, string _Value)
        {
            byte[] Compressed = ZlibStream.CompressString(_Value);

            _Packet.AddInt(Compressed.Length + 4);
            _Packet.AddInt(_Value.Length);
            _Packet.AddRange(Compressed);
        }

        /// <summary>
        /// Adds the hexadecimal string.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddHexa(this List<byte> _Packet, string _Value)
        {
            _Packet.AddRange(_Value.HexaToBytes());
        }

        /// <summary>
        /// Turn a hexa string into a byte array.
        /// </summary>
        /// <param name="_Value">The value.</param>
        /// <returns></returns>
        public static byte[] HexaToBytes(this string _Value)
        {
            string _Tmp = _Value.Replace("-", string.Empty).Replace(" ", string.Empty);
            return Enumerable.Range(0, _Tmp.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(_Tmp.Substring(x, 2), 16)).ToArray();
        }

        public static void AddData(this List<byte> _Writer, int _Type, int _ID)
        {
            _Writer.AddVInt(_Type);
            _Writer.AddVInt(_ID);
        }

        public static void AddData(this List<byte> _Writer, int Data)
        {
            _Writer.AddVInt(Data / 1000000);
            _Writer.AddVInt(Data % 1000000);
        }

        public static void AddCompressableString(this List<byte> _Writer, string String)
        {
            if (String != null)
            {
                int length = String.Length;

                if (length > 100)
                {
                    _Writer.Add(1);

                    byte[] Compressed = ZlibStream.CompressString(String);

                    _Writer.AddInt(Compressed.Length + 4);
                    _Writer.AddIntEndian(length);
                    _Writer.AddRange(Compressed);
                }
                else
                {
                    _Writer.Add(0);
                    _Writer.AddString(String);
                }
            }
            else
            {
                _Writer.Add(0);
                _Writer.AddInt(0);
            }
        }

        public static void AddShortEndian(this List<byte> Packet, short Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
        }

        public static void AddUShortEndian(this List<byte> Packet, ushort Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
        }

        public static void AddIntEndian(this List<byte> Packet, int Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
            Packet.Add((byte) (Value >> 16));
            Packet.Add((byte) (Value >> 24));
        }

        public static void AddUIntEndian(this List<byte> Packet, uint Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
            Packet.Add((byte) (Value >> 16));
            Packet.Add((byte) (Value >> 24));
        }

        public static void AddLongEndian(this List<byte> Packet, long Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
            Packet.Add((byte) (Value >> 16));
            Packet.Add((byte) (Value >> 24));
            Packet.Add((byte) (Value >> 32));
            Packet.Add((byte) (Value >> 40));
            Packet.Add((byte) (Value >> 48));
            Packet.Add((byte) (Value >> 56));
        }

        public static void AddULongEndian(this List<byte> Packet, ulong Value)
        {
            Packet.Add((byte) (Value));
            Packet.Add((byte) (Value >> 8));
            Packet.Add((byte) (Value >> 16));
            Packet.Add((byte) (Value >> 24));
            Packet.Add((byte) (Value >> 32));
            Packet.Add((byte) (Value >> 40));
            Packet.Add((byte) (Value >> 48));
            Packet.Add((byte) (Value >> 56));
        }

        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        public static void Shuffle<T>(this IList<T> List, XorShift Random)
        {
            int c = List.Count;

            while (c > 1)
            {
                c--;

                int r = Random.Next(c + 1);

                T Value = List[r];
                List[r] = List[c];
                List[c] = Value;
            }
        }
    }
}