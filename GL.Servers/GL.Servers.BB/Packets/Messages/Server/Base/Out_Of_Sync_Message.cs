namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Logic;

    internal class Out_Of_Sync_Message : Message
    {
        internal override short Type
        {
            get
            {
                return 24101;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Out_Of_Sync_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Out_Of_Sync_Message(Device Device) : base(Device)
        {

        }
    }
}