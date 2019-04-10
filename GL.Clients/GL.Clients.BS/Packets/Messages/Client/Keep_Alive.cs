namespace GL.Clients.BS.Packets.Messages.Client
{
    using GL.Clients.BS.Logic;

    internal class Keep_Alive : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive(Device Device) : base(Device)
        {
            this.Identifier = 10108;
        }
    }
}