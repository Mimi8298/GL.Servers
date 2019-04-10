namespace GL.Clients.BB.Packets.Messages.Server
{
    using GL.Clients.BB.Logic;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Authentification_Failed : Message
    {
        internal Reason Reason;

        internal string Fingerprint;
        internal string PatchingHost;
        internal string UpdateHost;
        internal string RedirectHost;
        internal string Message;

        internal int TimeLeft;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Failed(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Authentification_Failed.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reason         = (Reason) this.Reader.ReadInt32();

            this.Fingerprint    = this.Reader.ReadString();
            this.RedirectHost   = this.Reader.ReadString();
            this.PatchingHost   = this.Reader.ReadString();
            this.UpdateHost     = this.Reader.ReadString();
            this.Message        = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            int ErrorCode;

            if (!int.TryParse(this.Reason.ToString(), out ErrorCode))
            {
                System.Diagnostics.Debug.WriteLine("[*] We've been disconnected because : " + this.Reason + ".");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("[*] We've been disconnected because of error code " + ErrorCode + ".");
            }

            this.Device.State = State.DISCONNECTED;
        }
    }
}