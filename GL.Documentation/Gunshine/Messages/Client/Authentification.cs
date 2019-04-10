namespace GL.Servers.GS.Packets.Messages.Client
{
    using GL.Servers.GS.Core.Network;
    using GL.Servers.GS.Extensions.Binary;
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;
    using GL.Servers.GS.Packets.Messages.Server;

    internal class Authentification : Message
    {
        internal string Email;
        internal string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Authentification(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Email      = this.Reader.ReadString();
            this.Password   = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Authentification_OK(this.Device).Send();
        }
    }
}