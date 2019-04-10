namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using GL.Servers.BB.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Crypto_Error_Message : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto_Error_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Crypto_Error_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Crypto_Error.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.ShowBuffer();
        }
    }
}
