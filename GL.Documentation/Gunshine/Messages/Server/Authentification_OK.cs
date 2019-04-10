namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Extensions.List;
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    internal class Authentification_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Authentification_OK(Device Device) : base(Device)
        {
            this.Identifier     = 20104;
            this.Device.State   = State.LOGGED;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(1);
        }
    }
}