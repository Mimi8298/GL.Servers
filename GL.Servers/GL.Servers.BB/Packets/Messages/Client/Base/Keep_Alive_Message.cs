namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Packets.Messages.Server;
    using GL.Servers.BB.Packets.Messages.Server.Base;
    using GL.Servers.Extensions.Binary;

    internal class Keep_Alive_Message : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Keep_Alive_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Keep_Alive.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            new Keep_Alive_Ok_Message(this.Device).Send();
        }
    }
}