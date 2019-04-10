namespace GL.Servers.DataStream
{
    using System;
    using GL.Servers.Extensions;

    public class ChecksumEncoder
    {
        private int _Checksum;
        private int _BefChecksum;
        private bool _Enabled;
        private ByteStream _ByteStream;

        /// <summary>
        /// Gets the checksum of this stream.
        /// </summary>
        public int Checksum
        {
            get
            {
                return this._Checksum;
            }
        }

        /// <summary>
        /// Gets the byte stream instance.
        /// </summary>
        public ByteStream ByteStream
        {
            get
            {
                return this._ByteStream;
            }
        }

        /// <summary>
        /// Gets if this instance is checksum only mode.
        /// </summary>
        public virtual bool IsCheckSumOnlyMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksumEncoder"/> class.
        /// </summary>
        public ChecksumEncoder(ByteStream ByteStream) : base()
        {
            this._Enabled = true;
            this._ByteStream = ByteStream;
        }

        /// <summary>
        /// Writes a byte value.
        /// </summary>
        public virtual void AddByte(byte Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + Value + 11;
            this._ByteStream?.AddByte(Value);
        }

        /// <summary>
        /// Writes a boolean value.
        /// </summary>
        public virtual void AddBoolean(bool Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + (Value ? 13 : 7);
            this._ByteStream?.AddBoolean(Value);
        }

        /// <summary>
        /// Writes a short value.
        /// </summary>
        public virtual void AddShort(short Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + Value + 19;
            this._ByteStream?.AddShort(Value);
        }

        /// <summary>
        /// Writes a ushort value.
        /// </summary>
        public virtual void AddUShort(ushort Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + Value + 20;
            this._ByteStream?.AddUShort(Value);
        }

        /// <summary>
        /// Writes a int value.
        /// </summary>
        public virtual void AddInt(int Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + Value + 9;
            this._ByteStream?.AddInt(Value);
        }

        /// <summary>
        /// Writes a uint value.
        /// </summary>
        public virtual void AddUInt(uint Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + (int) Value + 21;
            this._ByteStream?.AddUInt(Value);
        }

        /// <summary>
        /// Writes a long value.
        /// </summary>
        public virtual void AddLong(long Value)
        {
            this._Checksum = (int)((Value >> 32) + Extensions.RotateRight((int)(Value >> 32) + Extensions.RotateRight((int)Value, 31) + 67, 31) + 91);
            this._ByteStream?.AddLong(Value);
        }

        /// <summary>
        /// Writes a ulong value.
        /// </summary>
        public virtual void AddULong(ulong Value)
        {
            this._Checksum = (int)((Value >> 32) + (ulong) Extensions.RotateRight ((int)(uint)(Value >> 32) + Extensions.RotateRight((int)(uint)Value, 31) + 67, 31) + 91);
            this._ByteStream?.AddULong(Value);
        }

        /// <summary>
        /// Writes a byte array.
        /// </summary>
        public virtual void AddBytes(byte[] Buffer)
        {
            int ROR = Extensions.RotateRight(this._Checksum, 31);

            if (Buffer != null)
            {
                this._Checksum = ROR + Buffer.Length + 28;
            }
            else
                this._Checksum = ROR + 27;

            this._ByteStream?.AddBytes(Buffer);
        }

        /// <summary>
        /// Writes a string.
        /// </summary>
        public virtual void AddString(string String)
        {
            int ROR = Extensions.RotateRight(this._Checksum, 31);

            if (String != null)
            {
                this._Checksum = ROR + String.Length + 28;
            }
            else
                this._Checksum = ROR + 27;

            this._ByteStream?.AddString(String);
        }

        /// <summary>
        /// Writes a string reference.
        /// </summary>
        public virtual void AddStringReference(string String)
        {
            if (String == null)
            {
                throw new ArgumentNullException("String");
            }

            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + String.Length + 9;
            this._ByteStream?.AddStringReference(String);
        }

        /// <summary>
        /// Writes a vint.
        /// </summary>
        public virtual void AddVInt(int Value)
        {
            this._Checksum = Extensions.RotateRight(this.Checksum, 31) + Value + 33;
            this._ByteStream?.AddVInt(Value);
        }

        /// <summary>
        /// Adds range to byte stream.
        /// </summary>
        public virtual void AddRange(byte[] Packet)
        {
            this._ByteStream?.AddRange(Packet);
        }

        /// <summary>
        /// Sets if encoder is enabled.
        /// </summary>
        public void EnableCheckSum(bool Value)
        {
            if (!this._Enabled || Value)
            {
                if (!this._Enabled && Value)
                {
                    this._Checksum = this._BefChecksum;
                }
            }
            else
                this._BefChecksum = this._Checksum;

            this._Enabled = Value;
        }

        /// <summary>
        /// Resets the checksum of this instance.
        /// </summary>
        public void ResetChecksum()
        {
            this._Checksum = 0;
        }

        /// <summary>
        /// Sets the bytestream instance.
        /// </summary>
        public void SetByteStream(ByteStream ByteStream)
        {
            this._ByteStream = ByteStream;
        }
        
        ~ChecksumEncoder()
        {
            this._ByteStream = null;
        }
    }
}