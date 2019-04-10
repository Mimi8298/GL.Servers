namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Settings_Joystick : Command
    {
        private int Mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_Joystick"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Settings_Joystick(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Settings_Joystick.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();

            this.Mode = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.Player.Info.JoystickMode = this.Mode;
        }
    }
}
 