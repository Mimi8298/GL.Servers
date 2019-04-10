namespace GL.Clients.BB.Packets.Messages.Client
{
    using GL.Clients.BB.Logic;

    internal class Get_Device_Token : Message
    {
        internal string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Get_Device_Token"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Get_Device_Token(Device Device) : base(Device)
        {
            // Get_Device_Token.
        }
    }
}