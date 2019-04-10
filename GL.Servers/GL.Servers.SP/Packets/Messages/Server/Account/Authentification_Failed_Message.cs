namespace GL.Servers.SP.Packets.Messages.Server.Account
{
    using GL.Servers.Logic.Enums;
    using GL.Servers.SP.Logic;

    internal class Authentification_Failed_Message : Message
    {
        internal Reason Reason;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20103;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed_Message"/> class.
        /// </summary>
        internal Authentification_Failed_Message(Device Device, Reason Reason) : base(Device)
        {
            this.Reason = Reason;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt((int) this.Reason);
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);
            this.Data.AddInt(0);
        }
    }
}