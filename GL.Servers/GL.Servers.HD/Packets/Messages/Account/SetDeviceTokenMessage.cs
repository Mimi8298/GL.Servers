namespace GL.Servers.HD.Packets.Messages.Account
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.HD.Logic;

    internal class SetDeviceTokenMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetDeviceTokenMessage"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public SetDeviceTokenMessage(Device Device, Reader Reader) : base(Device, Reader)
        {
            // SetDeviceTokenMessage.
        }
    }
}