namespace GL.Servers.SL.Extensions.Stream
{
    using System.Collections.Generic;
    using System.Text;

    internal class Writer : List<byte>
    {
        private WriteType LastWrite;
        private int LastWriteIndex;

        public new void Add(byte Value)
        {
            this.SetWriteType(WriteType.Byte);

            base.Add(Value);
        }

        public void Add(int Value)
        {
            this.SetWriteType(WriteType.Byte);

            base.Add((byte) Value);
        }

        public void AddBoolean(bool Value)
        {
            this.SetWriteType(WriteType.Boolean);

            base.Add((byte) (Value ? 1 : 0));
        }

        public void AddShort(short Value)
        {
            this.SetWriteType(WriteType.Short);

            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddUShort(ushort Value)
        {
            this.SetWriteType(WriteType.UShort);

            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddUInt24(int Value)
        {
            base.Add((byte)(Value >> 16));
            base.Add((byte)(Value >> 8));
            base.Add((byte)(Value));
        }

        public void AddInt(int Value)
        {
            this.SetWriteType(WriteType.Int);

            base.Add((byte) (Value >> 24));
            base.Add((byte) (Value >> 16));
            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddUInt(uint Value)
        {
            this.SetWriteType(WriteType.UInt);

            base.Add((byte) (Value >> 24));
            base.Add((byte) (Value >> 16));
            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddLong(long Value)
        {
            this.SetWriteType(WriteType.Long);

            base.Add((byte) (Value >> 56));
            base.Add((byte) (Value >> 48));
            base.Add((byte) (Value >> 40));
            base.Add((byte) (Value >> 32));
            base.Add((byte) (Value >> 24));
            base.Add((byte) (Value >> 16));
            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddULong(ulong Value)
        {
            this.SetWriteType(WriteType.ULong);

            base.Add((byte) (Value >> 56));
            base.Add((byte) (Value >> 48));
            base.Add((byte) (Value >> 40));
            base.Add((byte) (Value >> 32));
            base.Add((byte) (Value >> 24));
            base.Add((byte) (Value >> 16));
            base.Add((byte) (Value >> 8));
            base.Add((byte) (Value));
        }

        public void AddBytes(byte[] Array)
        {
            if (Array != null)
            {
                int length = Array.Length;

                if (length > 0)
                {
                    this.AddInt(length);
                    this.AddRange(Array);
                }
                else this.AddInt(0);
            }
            else this.AddInt(-1);
        }

        public void AddString(string String)
        {
            if (String != null)
            {
                int length = String.Length;

                if (length > 0)
                {
                    this.AddInt(length);
                    this.AddRange(Encoding.UTF8.GetBytes(String));
                }
                else this.AddInt(0);
            }
            else this.AddInt(-1);
        }

        private void SetWriteType(WriteType Type)
        {
            this.LastWrite      = Type;
            this.LastWriteIndex = this.Count;
        }

        private enum WriteType
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
    }
}