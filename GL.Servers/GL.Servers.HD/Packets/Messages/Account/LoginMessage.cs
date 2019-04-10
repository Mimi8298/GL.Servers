namespace GL.Servers.HD.Packets.Messages.Account
{
    using System;
    using System.Linq;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.HD.Core;
    using GL.Servers.HD.Logic;
    using GL.Servers.Logic.Enums;

    internal class LoginMessage : Message
    {
        private int HighID;
        private int LowID;

        private string PassToken;
        private string ResourceSHA;
        private string SVersion;

        private int Major;
        private int Minor;
        private int Build;

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public LoginMessage(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {

            return;
            this.HighID = this.Reader.ReadInt32();
            this.LowID = this.Reader.ReadInt32();
            this.PassToken = this.Reader.ReadString();
            this.ResourceSHA = this.Reader.ReadString();

            this.Reader.ReadInt32();

            if (!this.Reader.EndOfStream)
            {
                this.Device.UDID = this.Reader.ReadString();
                this.Device.OpenUDID = this.Reader.ReadString();
                this.Device.MacAddress = this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Device.DeviceModel = this.Reader.ReadString();

                    if (!this.Reader.EndOfStream)
                    {
                        this.Device.ADID = this.Reader.ReadString();

                        if (!this.Reader.EndOfStream)
                        {
                            this.Reader.ReadBoolean();

                            if (!this.Reader.EndOfStream)
                            {
                                this.Device.OSVersion = this.Reader.ReadString();

                                this.Reader.ReadString();
                                this.Reader.ReadString();

                                if (!this.Reader.EndOfStream)
                                {
                                    this.Device.PreferredLanguageId = this.Reader.ReadString();

                                    if (!this.Reader.EndOfStream)
                                    {
                                        this.Reader.ReadString();
                                        this.Reader.ReadBoolean();
                                        this.Reader.ReadString();

                                        if (!this.Reader.EndOfStream)
                                        {
                                            this.Reader.ReadInt32();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            
        }

        /// <summary>
        /// Gets if the client can be logged.
        /// </summary>
        internal bool CanLogin
        {
            get
            {
                if (this.HighID >= 0 && this.LowID >= 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
