namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    internal class Unbind_Facebook_Account_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unbind_Facebook_Account_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Unbind_Facebook_Account_OK(Device Device) : base(Device)
        {
            this.Identifier = 24214;
        }
    }
}