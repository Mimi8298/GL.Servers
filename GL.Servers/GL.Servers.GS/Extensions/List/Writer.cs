namespace GL.Servers.GS.Extensions.List
{
    using System.Collections.Generic;
    using System.Text;

    using GL.Servers.GS.Core;

    public static class Writer
    {
        /// <summary>
        /// Adds the byte.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddByte(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte)_Value);
        }

        /// <summary>
        /// Adds the compressed int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddVInt(this List<byte> _Packet, int _Value)
        {
            byte _Temporary = 0;

            _Temporary = (byte)((_Value >> 57) & 0x40L);
            _Value = _Value ^ (_Value >> 63);
            _Temporary |= (byte)(_Value & 0x3FL);
            _Value >>= 6;

            if (_Value != 0)
            {
                _Temporary |= 0x80;
                _Packet.Add(_Temporary);

                while (true)
                {
                    _Temporary = (byte)(_Value & 0x7F);
                    _Value >>= 7;
                    _Temporary |= (byte)((_Value != 0 ? 1 : 0) << 7);
                    _Packet.Add(_Temporary);

                    if (_Value == 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                _Packet.Add(_Temporary);
            }
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
            _Packet.Add((byte)(_Value & 0xFF));
        }

        public static void AddInt(this List<byte> _Packet, int? _Value)
        {
            _Packet.AddInt(_Value ?? 0);
        }

        /// <summary>
        /// Adds the int endian.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddIntEndian(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte)(_Value & 0xFF));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 24));
        }

        /// <summary>
        /// Adds the int.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Skip">The skip.</param>
        public static void AddInt24(this List<byte> _Packet, int _Value)
        {
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value & 0xFF));
        }

        /// <summary>
        /// Adds the long.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddLong(this List<byte> _Packet, long _Value)
        {
            _Packet.Add((byte)(_Value >> 56));
            _Packet.Add((byte)(_Value >> 48));
            _Packet.Add((byte)(_Value >> 40));
            _Packet.Add((byte)(_Value >> 32));

            _Packet.Add((byte)(_Value >> 24));
            _Packet.Add((byte)(_Value >> 16));
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value & 0xFF));
        }

        /// <summary>
        /// Adds the bool.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">if set to <c>true</c> [value].</param>
        public static void AddBool(this List<byte> _Packet, bool _Value)
        {
            _Packet.Add(_Value ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Adds the string.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddString(this List<byte> _Packet, string _Value)
        {
            if (_Value != null)
            {
                byte[] _Buffer = Encoding.UTF8.GetBytes(_Value);

                _Packet.AddInt(_Buffer.Length);
                _Packet.AddRange(_Buffer);
            }
            else _Packet.AddInt(-1);
        }

        /// <summary>
        /// Adds the signed short.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddShort(this List<byte> _Packet, short _Value)
        {
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value & 0xFF));
        }

        /// <summary>
        /// Adds the unsigned short.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddUShort(this List<byte> _Packet, ushort _Value)
        {
            _Packet.Add((byte)(_Value >> 8));
            _Packet.Add((byte)(_Value & 0xFF));
        }

        /// <summary>
        /// Adds the compressed data.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public static void AddCompressed(this List<byte> _Packet, string _Value)
        {
            bool Activate = _Value.Length > 100;

            _Packet.AddBool(Activate);

            if (Activate)
            {
                byte[] Compressed = Library.ZLib.ZlibStream.CompressString(_Value);

                _Packet.AddInt(Compressed.Length + 4);
                _Packet.AddInt(_Value.Length);
                _Packet.AddRange(Compressed);
            }
            else
            {
                _Packet.AddString(_Value);
            }
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
        public static IEnumerable<byte> HexaToBytes(this string _Value)
        {
            string _Tmp = _Value.Replace("-", string.Empty).Replace(" ", string.Empty);

            for (int i = 0; i < _Tmp.Length; i++)
            {
                yield return byte.Parse(_Tmp[i].ToString());
            }
        }

        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        internal static void Shuffle<T>(this IList<T> List)
        {
            int c = List.Count;

            while (c > 1)
            {
                c--;

                int r = Resources.Random.Next(c + 1);

                T Value = List[r];
                List[r] = List[c];
                List[c] = Value;
            }
        }

        internal static T Random<T>(this IList<T> List)
        {
            return List[Core.Resources.Random.Next(List.Count)];
        }

        internal static bool ArraysEqual(byte[] Array1, byte[] Array2)
        {
            if (Array1.Length == Array2.Length)
            {
                for (int i = 0; i < Array1.Length; i++)
                {
                    if (Array1[i] != Array2[i]) return false;
                }

                return true;
            }

            return false;
        }
    }
}