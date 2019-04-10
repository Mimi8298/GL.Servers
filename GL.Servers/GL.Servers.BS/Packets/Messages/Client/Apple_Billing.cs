namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Apple_Billing : Message
    {
        private int PackIdentifier;
        private string PackBundle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Apple_Billing"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Apple_Billing(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Apple_Billing.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.PackIdentifier = this.Reader.ReadInt32();
            this.PackBundle     = this.Reader.ReadString();
        }
    }
}