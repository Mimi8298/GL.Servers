namespace GL.Tools.Intruder.Extensions.Binary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GL.Servers.Extensions.List;

    internal class Writer : List<byte>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Writer"/> class.
        /// </summary>
        internal Writer() : base(2048)
        {
            // Writer.
        }

        /// <summary>
        /// Add the integer to the packet.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public void AddInt(int _Value)
        {
            this.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        /// <summary>
        /// Add the integer non-reversed, to the packet.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public void AddIntEndian(int _Value)
        {
            this.AddRange(BitConverter.GetBytes(_Value));
        }

        /// <summary>
        /// Add the integer to the packet, and skip some bytes.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Skip">The skip.</param>
        public void AddInt(int _Value, int _Skip)
        {
            this.AddRange(BitConverter.GetBytes(_Value).Reverse().Skip(_Skip));
        }

        /// <summary>
        /// Add the long to the packet.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        public void AddLong(long _Value)
        {
            this.AddRange(BitConverter.GetBytes(_Value).Reverse());
        }

        /// <summary>
        /// Add the long to the packet, and skip some bytes.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Value">The value.</param>
        /// <param name="_Skip">The skip.</param>
        public void AddLong(long _Value, int _Skip)
        {
            this.AddRange(BitConverter.GetBytes(_Value).Reverse().Skip(_Skip));
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="_Value">if set to <c>true</c> [value].</param>
        public void Add(bool _Value)
        {
            this.Add(_Value ? (byte) 1 : (byte) 0);
        }

        /// <summary>
        /// Add the string to the packet.
        /// </summary>
        /// <param name="_Value">The value.</param>
        public void AddString(string _Value)
        {
            if (_Value == null)
            {
                this.AddRange(BitConverter.GetBytes(-1).Reverse());
            }
            else
            {
                byte[] _Buffer = Encoding.UTF8.GetBytes(_Value);

                this.AddInt(_Buffer.Length);
                this.AddRange(_Buffer);
            }
        }

        /// <summary>
        /// Adds the specified string of hexa.
        /// </summary>
        /// <param name="_Value">The value.</param>
        public void AddRange(string _Value)
        {
            base.AddRange(_Value.HexaToBytes());
        }
    }
}