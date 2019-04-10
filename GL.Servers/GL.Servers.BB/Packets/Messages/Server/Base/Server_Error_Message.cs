namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using System.Text;

    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.List;

    internal class Server_Error_Message : Message
    {
        private int Reason;

        internal override short Type
        {
            get
            {
                return 24115;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Error_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reason">The reason.</param>
        public Server_Error_Message(Device Device, int Reason) : base(Device)
        {
            this.Reason = Reason;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Reason);
        }
    }
}