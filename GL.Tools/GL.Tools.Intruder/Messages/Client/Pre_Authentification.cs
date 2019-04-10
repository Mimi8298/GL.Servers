namespace GL.Tools.Intruder.Messages.Client
{
    using System.Collections.Generic;

    using GL.Servers.Extensions.List;

    internal class Pre_Authentification
    {
        internal List<byte> Writer;
        internal Header Header;
        internal int[] Version;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pre_Authentification"/> class.
        /// </summary>
        public Pre_Authentification()
        {
            this.Writer = new List<byte>();
            this.Header = new Header(10100, 0, 0);
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        internal byte[] ToBytes
        {
            get
            {
                List<byte> _Packet = new List<byte>();

                _Packet.AddRange(this.Header.ToBytes);
                _Packet.AddRange(this.Writer);

                return _Packet.ToArray();
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode()
        {
            this.Writer.AddInt(1); // Protocol
            this.Writer.AddInt(11); // Key Version
            this.Writer.AddInt(this.Version[0]); // Major Version
            this.Writer.AddInt(this.Version[2]); // Revision Version
            this.Writer.AddInt(this.Version[1]); // Minor Version
            this.Writer.AddString("f3e66ce990a7b68332211f6fc99a0a7a46c1bffc"); // Content Hash
            this.Writer.AddInt(2); // Device Type
            this.Writer.AddInt(2); // App Store

            this.Header.Length = (ushort) this.Writer.Count;
        }
    }
}