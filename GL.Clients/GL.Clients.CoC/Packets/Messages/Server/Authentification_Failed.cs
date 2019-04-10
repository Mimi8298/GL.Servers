namespace GL.Clients.CoC.Packets.Messages.Server
{
    using GL.Clients.CoC.Logic;
    using GL.Clients.CoC.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Authentification_Failed : Message
    {
        internal Reason Reason;

        internal string Fingerprint;
        internal string PatchingHost;
        internal string UpdateHost;
        internal string Message;

        internal int TimeLeft;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Failed(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.DISCONNECTED;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reason         = (Reason) this.Reader.ReadInt32();

            this.Fingerprint    = this.Reader.ReadString();

            this.Reader.ReadString();

            this.PatchingHost   = this.Reader.ReadString();
            this.UpdateHost     = this.Reader.ReadString();

            return;

            switch (this.Version)
            {
                case 4:
                {
                    this.Reader.ReadString();
                    this.Reader.Read();

                    this.TimeLeft   = this.Reader.ReadVInt();

                    this.Reader.ReadString();
                    break;
                }

                case 2:
                {
                    this.Message    = this.Reader.ReadString();
                    this.TimeLeft   = this.Reader.ReadInt32();
                    break;
                }
            }

            // 00-00-00-08-FF-FF-FF-FF-00-00-00-00-FF-FF-FF-FF-00-00-00-40-68-74-74-70-73-3A-2F-2F-72-69-6E-6B-2E-68-6F-63-6B-65-79-61-70-70-2E-6E-65-74-2F-61-70-70-73-2F-65-39-34-32-32-35-39-39-64-62-36-62-34-36-66-33-39-64-64-62-30-30-33-66-66-66-65-36-66-30-64-61-FF-FF-FF-FF-00-00-00-00-00-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-00-00-00-00-00-FF-FF-FF-FF-FF-FF-FF-FF-01
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
            
            System.Diagnostics.Debug.WriteLine("[*] " + this.Length);
            System.Diagnostics.Debug.WriteLine("[*] " + this.PatchingHost);
            System.Diagnostics.Debug.WriteLine("[*] " + this.UpdateHost);

            this.Reader.BaseStream.Position = 7;
            this.Debug();
        }
    }
}