namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Setting_Hints : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setting_Hints"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Setting_Hints(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Setting_Hints.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.Player.Info.HintsEnabled = !this.Device.Player.Info.HintsEnabled;
        }
    }
}
 