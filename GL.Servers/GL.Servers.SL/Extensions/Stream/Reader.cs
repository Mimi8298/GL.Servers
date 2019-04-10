namespace GL.Servers.SL.Extensions.Stream
{
    using System;
    using System.Linq;
    using System.Text;

    internal class Reader : IDisposable
    {
        internal byte[] Buffer;
        internal int Offset;

        private ReadType LastReadType;
        private int LastReadTypeOffset;

        internal bool isAtEnd
        {
            get
            {
                return this.Offset >= this.Buffer.Length;
            }
        }

        internal byte[] RemainingBytes
        {
            get
            {
                return this.Buffer.Skip(this.Offset).ToArray();
            }
        }

        public Reader(byte[] Buffer)
        {
            this.Buffer = Buffer;
        }

        internal byte ReadByte()
        {
            this.SetReadType(ReadType.Byte);

            return this.Buffer[this.Offset++];
        }

        internal bool ReadBoolean()
        {
            if (this.LastReadType == ReadType.Boolean)
            {
                
            }
            else this.SetReadType(ReadType.Boolean);

            return false;
        }

        internal short ReadShort()
        {
            this.SetReadType(ReadType.Short);

            return (short) (this.Buffer[this.Offset++] << 8 |
                            this.Buffer[this.Offset++]);
        }

        internal ushort ReadUShort()
        {
            this.SetReadType(ReadType.UShort);

            return (ushort) (this.Buffer[this.Offset++] << 8 |
                             this.Buffer[this.Offset++]);
        }

        internal int ReadInt()
        {
            this.SetReadType(ReadType.Int);

            return this.Buffer[this.Offset++] << 24 |
                   this.Buffer[this.Offset++] << 16 |
                   this.Buffer[this.Offset++] << 8  |
                   this.Buffer[this.Offset++];
        }

        internal uint ReadUInt()
        {
            this.SetReadType(ReadType.UInt);

            return (uint) (this.Buffer[this.Offset++] << 24 |
                           this.Buffer[this.Offset++] << 16 |
                           this.Buffer[this.Offset++] << 8  |
                           this.Buffer[this.Offset++]);
        }

        internal long ReadLong()
        {
            this.SetReadType(ReadType.Long);

            return this.Buffer[this.Offset++] << 56 |
                   this.Buffer[this.Offset++] << 48 |
                   this.Buffer[this.Offset++] << 40 |
                   this.Buffer[this.Offset++] << 32 |
                   this.Buffer[this.Offset++] << 24 |
                   this.Buffer[this.Offset++] << 16 |
                   this.Buffer[this.Offset++] << 8  |
                   this.Buffer[this.Offset++];
        }

        internal ulong ReadULong()
        {
            this.SetReadType(ReadType.ULong);

            return (ulong) (this.Buffer[this.Offset++] << 56 |
                            this.Buffer[this.Offset++] << 48 |
                            this.Buffer[this.Offset++] << 40 |
                            this.Buffer[this.Offset++] << 32 |
                            this.Buffer[this.Offset++] << 24 |
                            this.Buffer[this.Offset++] << 16 |
                            this.Buffer[this.Offset++] << 8 |
                            this.Buffer[this.Offset++]);
        }

        internal byte[] ReadBytes()
        {
            int length = this.ReadInt();

            if (length < 0)
            {
                if (length != -1)
                {
                    throw new Exception("Byte array length is not valid. Length : " + length);
                }

                return null;
            }

            if (length > 0)
            {
                byte[] Array = new byte[length];
                System.Buffer.BlockCopy(this.Buffer, this.Offset, Array, 0, length);
                this.Offset += length;
                return Array;
            }

            return new byte[0];
        }

        internal string ReadString()
        {
            int length = this.ReadInt();

            if (length < 0)
            {
                if (length != -1)
                {
                    throw new Exception("String length is not valid. Length : " + length);
                }

                return null;
            }

            if (length > 0)
            {
                string String = Encoding.UTF8.GetString(this.Buffer, this.Offset, length);
                this.Offset += length;
                return String;
            }

            return string.Empty;
        }
        
        private void SetReadType(ReadType Type)
        {
            this.LastReadType       = Type;
            this.LastReadTypeOffset = this.Offset;
        }

        private enum ReadType
        {
            Byte,
            Boolean,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong
        }

        public void Dispose()
        {
            this.Buffer = null;
            this.Offset = 0;
            this.LastReadType = 0;
            this.LastReadTypeOffset = 0;
        }
    }
}