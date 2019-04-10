namespace GL.Tools.Intruder.Messages.Client
{
    using System.Collections.Generic;

    using GL.Servers.Extensions.List;

    internal class Authentification
    {
        internal List<byte> Writer;
        internal Header Header;

        internal int[] ClientVersion;

        internal int HighID         = 0;
        internal int LowID          = 0;

        internal string Token       = null;
        internal string Masterhash  = "b10029d266a4a4184ef840d6b9137f92fc20e26d";

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        public Authentification()
        {
            this.Writer = new List<byte>();
            this.Header = new Header(10101, 0, 8);
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
            this.Writer.AddInt(this.HighID);
            this.Writer.AddInt(this.LowID);

            this.Writer.AddString(this.Token);

            this.Writer.AddInt(this.ClientVersion[0]);
            this.Writer.AddInt(this.ClientVersion[1]);
            this.Writer.AddInt(this.ClientVersion[2]);

            this.Writer.AddString(this.Masterhash);

            this.Writer.AddInt(0);

            this.Writer.AddHexa("00-00-00-24-30-30-30-30-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-30-30-30-30-30-30-30-30-FF-FF-FF-FF-00-00-00-09-69-50-68-6F-6E-65-39-2C-33-00-00-00-00-05-66-72-2D-46-52-00-00-00-24-30-30-30-30-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-30-30-30-30-30-30-30-30-00-00-00-06-31-30-2E-33-2E-31-00-00-00-00-00-00-00-00-00-00-00-00-00-01-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00");

            this.Header.Length = (ushort) this.Writer.Count;
        }
    }
}