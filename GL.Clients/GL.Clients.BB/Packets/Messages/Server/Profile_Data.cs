namespace GL.Clients.BB.Packets.Messages.Server
{
    using GL.Clients.BB.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Profile_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Profile_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Profile_Data.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            
        }
    }
}