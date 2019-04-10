namespace GL.Servers.BB.Packets
{
    using System;

    using GL.Servers.BB.Logic;
    using GL.Servers.Logic.Enums;

    internal class Debug : IDisposable
    {
        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        internal Device Device
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        internal string[] Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the required rank.
        /// </summary>
        internal Rank RequiredRank
        {
            get;
            set;
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

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la
        /// libération ou à la redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
            this.Parameters = null;
            this.Device     = null;
        }
    }
}