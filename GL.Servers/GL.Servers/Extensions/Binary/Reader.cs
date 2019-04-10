namespace GL.Servers.Extensions.Binary
{
    using System;
    using System.IO;
    using System.Text;

    public class Reader : BinaryReader
    {
        private byte BooleanValue;
        private int BooleanOffset;
        private int BooleanAdditionalValue;

        public Reader(byte[] _Buffer) : base(new MemoryStream(_Buffer))
        {
            // Packet Reader...
        }

        /// <summary>
        /// Gets a value indicating whether [end of stream].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [end of stream]; otherwise, <c>false</c>.
        /// </value>
        public bool EndOfStream
        {
            get
            {
                return this.BaseStream.Length == this.BaseStream.Position;
            }
        }

        /// <summary>
        /// Reads the specified buffer.
        /// </summary>
        /// <param name="_Buffer">The buffer.</param>
        /// <param name="_Offset">The offset.</param>
        /// <param name="_Count">The count.</param>
        /// <returns></returns>
        public override int Read(byte[] _Buffer, int _Offset, int _Count)
        {
            return this.BaseStream.Read(_Buffer, 0, _Count);
        }

        /// <summary>
        /// Reads the byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] ReadArray()
        {
            int length = this.ReadInt32();

            if (length < 0)
            {
                if (length != -1)
                {
                    throw new Exception("Byte array length is invalid. Length : " + length + ".");
                }

                return null;
            }

            return this.ReadBytes(length);
        }

        /// <summary>
        /// Reads a Boolean value from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <returns>
        /// true if the byte is nonzero; otherwise, false.
        /// </returns>
        /// <exception cref="System.Exception">Error when reading a bool in packet.</exception>
        public override bool ReadBoolean()
        {
            if (this.BooleanOffset == 0)
            {
                this.BooleanValue = this.ReadByte();
            }

            this.BooleanAdditionalValue += (8 - this.BooleanOffset) >> 3;
            bool Value = ((1 << this.BooleanOffset) & this.BooleanValue + this.BooleanAdditionalValue - 1) != 0;
            this.BooleanOffset = this.BooleanOffset + 1 & 7;

            return Value;
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBytes()
        {
            int length = this.ReadInt32();

            if (length == -1)
            {
                return null;
            }

            return this.ReadBytes(length);
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>
        /// A 2-byte signed integer read from the current stream.
        /// </returns>
        public override short ReadInt16()
        {
            return (short) this.ReadUInt16();
        }

        /// <summary>
        /// Reads the 3 bytes integer.
        /// </summary>
        /// <returns></returns>
        public int ReadInt24()
        {
            return this.ReadByte() << 16 | this.ReadByte() << 8 | this.ReadByte();
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>
        /// A 4-byte signed integer read from the current stream.
        /// </returns>
        public override int ReadInt32()
        {
            return (int) this.ReadUInt32();
        }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns>
        /// An 8-byte signed integer read from the current stream.
        /// </returns>
        public override long ReadInt64()
        {
            return (long) this.ReadUInt64();
        }

        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
        /// </summary>
        /// <returns>
        /// The string being read.
        /// </returns>
        public override string ReadString()
        {
            int lenght = this.ReadInt32();

            if (lenght < 0)
            {
                if (lenght != -1)
                {
                    throw new Exception("String length is not valid. Length : " + lenght + ".");
                }

                return null;
            }

            if (lenght > 0)
            {
                return Encoding.UTF8.GetString(this.ReadBytes(lenght));
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using little-endian encoding and advances the position of the stream by two bytes.
        /// </summary>
        /// <returns>
        /// A 2-byte unsigned integer read from this stream.
        /// </returns>
        public override ushort ReadUInt16()
        {
            return (ushort) (this.ReadByte() << 8 | this.ReadByte());
        }

        public uint ReadUInt24()
        {
            return (uint) this.ReadInt24();
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <returns>
        /// A 4-byte unsigned integer read from this stream.
        /// </returns>
        public override uint ReadUInt32()
        {
            return (uint) (this.ReadByte() << 24 | this.ReadByte() << 16 | this.ReadByte() << 8 | this.ReadByte());
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        /// <returns>
        /// An 8-byte unsigned integer read from this stream.
        /// </returns>
        public override ulong ReadUInt64()
        {
            return (ulong)(this.ReadByte() << 56 | this.ReadByte() << 48 | this.ReadByte() << 40 | this.ReadByte() << 32 | this.ReadByte() << 24 | this.ReadByte() << 16 | this.ReadByte() << 8 | this.ReadByte());
        }

        /// <summary>
        /// Seeks to the specified offset.
        /// </summary>
        /// <param name="_Offset">The offset.</param>
        /// <param name="_Origin">The origin.</param>
        /// <returns></returns>
        public long Seek(long _Offset, SeekOrigin _Origin = SeekOrigin.Current)
        {
            return this.BaseStream.Seek(_Offset, _Origin);
        }

        /// <summary>
        /// Reads the v int.
        /// </summary>
        public int ReadVInt()
        {
            byte Byte = this.ReadByte();
            int Result;

            if ((Byte & 0x40) != 0)
            {
                Result = Byte & 0x3F;

                if ((Byte & 0x80) != 0)
                {
                    Result |= ((Byte = this.ReadByte()) & 0x7F) << 6;

                    if ((Byte & 0x80) != 0)
                    {
                        Result |= ((Byte = this.ReadByte()) & 0x7F) << 13;

                        if ((Byte & 0x80) != 0)
                        {
                            Result |= ((Byte = this.ReadByte()) & 0x7F) << 20;

                            if ((Byte & 0x80) != 0)
                            {
                                Result |= ((Byte = this.ReadByte()) & 0x7F) << 27;
                                return (int) (Result | 0x80000000);
                            }

                            return (int) (Result | 0xF8000000);
                        }

                        return (int) (Result | 0xFFF00000);
                    }

                    return (int) (Result | 0xFFFFE000);
                }

                return (int) (Result | 0xFFFFFFC0);
            }
            else
            {
                Result = Byte & 0x3F;

                if ((Byte & 0x80) != 0)
                {
                    Result |= ((Byte = this.ReadByte()) & 0x7F) << 6;

                    if ((Byte & 0x80) != 0)
                    {
                        Result |= ((Byte = this.ReadByte()) & 0x7F) << 13;

                        if ((Byte & 0x80) != 0)
                        {
                            Result |= ((Byte = this.ReadByte()) & 0x7F) << 20;

                            if ((Byte & 0x80) != 0)
                            {
                                Result |= ((Byte = this.ReadByte()) & 0x7F) << 27;
                            }
                        }
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// Reads the v unsigned int.
        /// </summary>
        public uint ReadVUInt() 
        {
            return (uint) this.ReadVInt();
        }
    }
}