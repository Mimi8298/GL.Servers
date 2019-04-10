namespace GL.Editor.Extensions.List
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using GL.Editor.Extensions.Binary;

    internal static class ReaderList
    {
        public static bool EndOfStream(this BinaryReader _Reader)
        {
            return _Reader.BaseStream.Length == _Reader.BaseStream.Position;
        }

        public static byte[] ReadAllBytes(this BinaryReader _Reader)
        {
            using (MemoryStream _Stream = new MemoryStream())
            {
                byte[] _Buffer = new byte[2048];
                int _Count;
                while ((_Count = _Reader.Read(_Buffer, 0, _Buffer.Length)) != 0)
                {
                    _Stream.Write(_Buffer, 0, _Count);
                }

                return _Stream.ToArray();
            }
        }

        public static int ReadInt32WithEndian(this BinaryReader _Reader)
        {
            byte[] _Buffer = _Reader.ReadBytes(4);
            Array.Reverse(_Buffer);
            return BitConverter.ToInt32(_Buffer, 0);
        }

        public static long ReadInt64WithEndian(this BinaryReader _Reader)
        {
            byte[] _Buffer = _Reader.ReadBytes(8);
            Array.Reverse(_Buffer);
            return BitConverter.ToInt64(_Buffer, 0);
        }

        public static string ReadString(this BinaryReader _Reader)
        {
            int _Length = _Reader.ReadInt32WithEndian();
            if (_Length > -1)
            {
                if (_Length > 0)
                {
                    byte[] _Buffer = _Reader.ReadBytes(_Length);
                    return Encoding.UTF8.GetString(_Buffer);
                }

                return string.Empty;
            }

            return null;
        }

        public static ushort ReadUInt16WithEndian(this BinaryReader _Reader)
        {
            byte[] _Buffer = _Reader.ReadBytes(2);
            Array.Reverse(_Buffer);
            return BitConverter.ToUInt16(_Buffer, 0);
        }

        public static uint ReadUInt32WithEndian(this BinaryReader _Reader)
        {
            byte[] _Buffer = _Reader.ReadBytes(4);
            Array.Reverse(_Buffer);
            return BitConverter.ToUInt32(_Buffer, 0);
        }

        public static int ReadVInt(this BinaryReader br)
        {
            int v5;
            byte b = br.ReadByte();
            v5 = b & 0x80;
            int _LR = b & 0x3F;

            if ((b & 0x40) != 0)
            {
                if (v5 != 0)
                {
                    b = br.ReadByte();
                    v5 = ((b << 6) & 0x1FC0) | _LR;
                    if ((b & 0x80) != 0)
                    {
                        b = br.ReadByte();
                        v5 = v5 | ((b << 13) & 0xFE000);
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            v5 = v5 | ((b << 20) & 0x7F00000);
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                _LR = (int) (v5 | (b << 27) | 0x80000000);
                            }
                            else
                            {
                                _LR = (int) (v5 | 0xF8000000);
                            }
                        }
                        else
                        {
                            _LR = (int) (v5 | 0xFFF00000);
                        }
                    }
                    else
                    {
                        _LR = (int) (v5 | 0xFFFFE000);
                    }
                }
            }
            else
            {
                if (v5 != 0)
                {
                    b = br.ReadByte();
                    _LR |= (b << 6) & 0x1FC0;
                    if ((b & 0x80) != 0)
                    {
                        b = br.ReadByte();
                        _LR |= (b << 13) & 0xFE000;
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            _LR |= (b << 20) & 0x7F00000;
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                _LR |= b << 27;
                            }
                        }
                    }
                }
            }

            return _LR;
        }

        public static int ReadVInt(byte[] ba)
        {
            int v5;
            int _LR;
            using (Reader br = new Reader(ba))
            {
                byte b = br.ReadByte();
                v5 = b & 0x80;
                _LR = b & 0x3F;

                if ((b & 0x40) != 0)
                {
                    if (v5 != 0)
                    {
                        b = br.ReadByte();
                        v5 = ((b << 6) & 0x1FC0) | _LR;
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            v5 = v5 | ((b << 13) & 0xFE000);
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                v5 = v5 | ((b << 20) & 0x7F00000);
                                if ((b & 0x80) != 0)
                                {
                                    b = br.ReadByte();
                                    _LR = (int) (v5 | (b << 27) | 0x80000000);
                                }
                                else
                                {
                                    _LR = (int) (v5 | 0xF8000000);
                                }
                            }
                            else
                            {
                                _LR = (int) (v5 | 0xFFF00000);
                            }
                        }
                        else
                        {
                            _LR = (int) (v5 | 0xFFFFE000);
                        }
                    }
                }
                else
                {
                    if (v5 != 0)
                    {
                        b = br.ReadByte();
                        _LR |= (b << 6) & 0x1FC0;
                        if ((b & 0x80) != 0)
                        {
                            b = br.ReadByte();
                            _LR |= (b << 13) & 0xFE000;
                            if ((b & 0x80) != 0)
                            {
                                b = br.ReadByte();
                                _LR |= (b << 20) & 0x7F00000;
                                if ((b & 0x80) != 0)
                                {
                                    b = br.ReadByte();
                                    _LR |= b << 27;
                                }
                            }
                        }
                    }
                }
            }

            return _LR;
        }

        public static long ReadVInt64(this BinaryReader br)
        {
            byte temp = br.ReadByte();
            long i = 0;
            int Sign = (temp >> 6) & 1;
            i = temp & 0x3FL;

            while (true)
            {
                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << 6;

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7 + 7);

                if ((temp & 0x80) == 0)
                {
                    break;
                }

                temp = br.ReadByte();
                i |= (temp & 0x7FL) << (6 + 7 + 7 + 7 + 7 + 7 + 7 + 7);
            }
            i ^= -Sign;
            return i;
        }

        public static string ToHexa(byte[] _Packet)
        {
            return BitConverter.ToString(_Packet).Replace("-", string.Empty).ToUpper();
        }

        public static string ToHexa(this List<byte> _Packet)
        {
            return BitConverter.ToString(_Packet.ToArray()).Replace("-", string.Empty).ToUpper();
        }

        public static string ToHexa(this string _Packet)
        {
            return BitConverter.ToString(Encoding.UTF8.GetBytes(_Packet)).Replace("-", string.Empty).ToUpper();
        }
    }
}