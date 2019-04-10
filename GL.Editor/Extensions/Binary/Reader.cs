namespace GL.Editor.Extensions.Binary
{
    using System;
    using System.IO;
    using System.Text;

    public class Reader : BinaryReader
    {
        public Reader(Stream Stream) : base(Stream)
        {
        }

        public Reader(byte[] Buffer) : base(new MemoryStream(Buffer))
        {
            // Packet Reader...
        }

        public override int Read(byte[] Buffer, int Offset, int Count)
        {
            return this.BaseStream.Read(Buffer, 0, Count);
        }

        public byte[] ReadArray()
        {
            int Length = this.ReadInt32();
            if (Length == -1 || Length < -1 || Length > this.BaseStream.Length - this.BaseStream.Position)
            {
                return null;
            }

            byte[] Buffer = this.ReadBytesWithEndian(Length, false);
            return Buffer;
        }

        public string ReadASCII()
        {
            int Length = this.ReadByte();
            if (Length < 255 && Length > 0)
            {
                return Encoding.UTF8.GetString(this.ReadBytes(Length));
            }
            return string.Empty;
        }

        public override bool ReadBoolean()
        {
            byte state = this.ReadByte();
            switch (state)
            {
                case 0:
                    return false;

                case 1:
                    return true;

                default:
                    throw new Exception("Error when reading a bool in packet.");
            }
        }

        public override byte ReadByte()
        {
            return (byte) this.BaseStream.ReadByte();
        }

        public byte[] ReadBytes()
        {
            int length = this.ReadInt32();
            if (length == -1)
            {
                return null;
            }

            return this.ReadBytes(length);
        }

        public override short ReadInt16()
        {
            return (short) this.ReadUInt16();
        }

        public int ReadInt24()
        {
            byte[] Temp = this.ReadBytesWithEndian(3, false);
            return (Temp[0] << 16) | (Temp[1] << 8) | Temp[2];
        }

        public override int ReadInt32()
        {
            return BitConverter.ToInt32(this.ReadBytes(4), 0);
            // return (int)this.ReadUInt32();
        }

        public override long ReadInt64()
        {
            return (long) this.ReadUInt64();
        }

        public override string ReadString()
        {
            int Length = this.ReadInt32();
            if (Length == -1 || Length < -1 || Length > this.BaseStream.Length - this.BaseStream.Position)
            {
                return null;
            }

            byte[] Buffer = this.ReadBytesWithEndian(Length, false);
            return Encoding.UTF8.GetString(Buffer);
        }

        public ushort ReadUInt16WithEndian()
        {
            byte[] Buffer = this.ReadBytesWithEndian(2);
            return BitConverter.ToUInt16(Buffer, 0);
        }

        public override ushort ReadUInt16()
        {
            byte[] Buffer = this.ReadBytes(2);
            return BitConverter.ToUInt16(Buffer, 0);
        }

        public uint ReadUInt24()
        {
            return (uint) this.ReadInt24();
        }

        public uint ReadUInt32WithEndian()
        {
            byte[] Buffer = this.ReadBytesWithEndian(4);
            return BitConverter.ToUInt32(Buffer, 0);
        }

        public override uint ReadUInt32()
        {
            byte[] Buffer = this.ReadBytes(4);
            return BitConverter.ToUInt32(Buffer, 0);
        }

        public ulong ReadUInt64WithEndian()
        {
            byte[] Buffer = this.ReadBytesWithEndian(8);
            return BitConverter.ToUInt64(Buffer, 0);
        }

        public override ulong ReadUInt64()
        {
            byte[] Buffer = this.ReadBytes(8);
            return BitConverter.ToUInt64(Buffer, 0);
        }

        public long Seek(long Offset, SeekOrigin Origin)
        {
            return this.BaseStream.Seek(Offset, Origin);
        }

        private byte[] ReadBytesWithEndian(int Count, bool Endian = true)
        {
            byte[] Buffer = new byte[Count];
            this.BaseStream.Read(Buffer, 0, Count);
            if (BitConverter.IsLittleEndian && Endian)
            {
                Array.Reverse(Buffer);
            }

            return Buffer;
        }
    }
}