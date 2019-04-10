namespace GL.Servers.CR.Packets
{
    using GL.Servers.CR.Logic;

    using GL.Servers.Logic.Enums;

    internal class Debug
    {
        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        internal Device Device;

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        internal string[] Parameters;

        /// <summary>
        /// Gets the required rank to execute this debug command.
        /// </summary>
        internal virtual Rank RequiredRank
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Debug"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Parameters">The parameters.</param>
        internal Debug(Device Device, params string[] Parameters)
        {
            this.Device         = Device;
            this.Parameters     = Parameters;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode()
        {
            // Decode.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Process.
        }
    }
}