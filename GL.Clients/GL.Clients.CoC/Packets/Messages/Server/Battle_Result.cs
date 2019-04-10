namespace GL.Clients.CoC.Packets.Messages.Server
{
    using GL.Clients.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Battle_Result : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Result"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Battle_Result(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Battle_Result.
        }
    }
}