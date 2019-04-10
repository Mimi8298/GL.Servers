namespace GL.Tools.Intruder.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.Extensions.List;

    internal class Header : IDisposable
    {
        /// <summary>
        /// The header length, which is 7 and constant.
        /// </summary>
        internal const int HeaderLength = 7;

        /// <summary>
        /// The message identifier.
        /// </summary>
        internal ushort Identifier;

        /// <summary>
        /// The message content length.
        /// </summary>
        internal uint Length;

        /// <summary>
        /// The message version.
        /// </summary>
        internal ushort Version;

        /// <summary>
        /// If an error occured,
        /// we fallback to this length.
        /// </summary>
        internal ushort FLength;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Header" /> struct.
        /// </summary>
        /// <param name="_Identifier">The identifier.</param>
        /// <param name="_Length">The length.</param>
        /// <param name="_Version">The version.</param>
        internal Header(ushort _Identifier = 0, ushort _Length = 0, ushort _Version = 0)
        {
            this.Identifier = _Identifier;
            this.Length     = _Length;
            this.Version    = _Version;
            this.FLength    = _Length;
        }

        /// <summary>
        /// Gets or sets the original packet header in a 7 bytes long byte array.
        /// </summary>
        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>(7);
                Packet.AddUShort(this.Identifier);
                Packet.AddUInt24(this.Length);
                Packet.AddUShort(this.Version);
                return Packet.ToArray();
            }
            set
            {
                this.Identifier = BitConverter.ToUInt16(value.Take(2).Reverse().ToArray(), 0);
                this.Length     = BitConverter.ToUInt16(value.Skip(3).Take(2).Reverse().ToArray(), 0);
                this.Version    = BitConverter.ToUInt16(value.Skip(5).Take(2).Reverse().ToArray(), 0);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Identifier = 0;
            this.Length     = 0;
            this.Version    = 0;
            this.FLength    = 0;
        }
    }
}