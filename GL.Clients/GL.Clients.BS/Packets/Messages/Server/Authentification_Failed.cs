namespace GL.Clients.BS.Packets.Messages.Server
{
    using GL.Clients.BS.Core;
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json.Linq;

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

                if (this.Reason == Reason.Patch)
                {
                    JObject JSON = JObject.Parse(this.Fingerprint);

                    if (JSON != null)
                    {
                        string SHA = JSON["sha"].ToObject<string>();
                        string VER = JSON["version"].ToObject<string>();

                        Logging.Info(this.GetType(), "The new patch is under the version " + VER + ", and the hash " + SHA + ".");
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "We've tried to decode the fingerprint, but it was null.");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("[*] We've been disconnected because of error code " + ErrorCode + ".");
            }

            this.Device.State = State.DISCONNECTED;
        }
    }
}