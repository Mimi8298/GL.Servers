namespace GL.Servers.BB.Packets.Messages.Client.Player
{
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Packets.Messages.Server.Player;

    using GL.Servers.Extensions.Binary;

    internal class Go_Home : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Go_Home"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Go_Home(Device Device, Reader Reader) : base(Device, Reader)
        {
            
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Own_Home_Data_Message(this.Device).Send();
        }
    }
}