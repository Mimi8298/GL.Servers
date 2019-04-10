namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Logic;

    internal class Keep_Alive_Ok_Message : Message
    {
        internal override short Type
        {
            get
            {
                return 20108;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Ok_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Keep_Alive_Ok_Message(Device Device) : base(Device)
        {

        }
    }
}