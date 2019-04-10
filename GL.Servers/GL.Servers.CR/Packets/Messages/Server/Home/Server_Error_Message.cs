namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using System.Text;

    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Server_Error_Message : Message
    {
        private string Message;
        private StringBuilder Reason;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24115;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Error_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Message">The message.</param>
        /// <param name="Initial">if set to <c>true</c> [initial].</param>
        public Server_Error_Message(Device Device, string Message = "", bool Initial = false) : base(Device)
        {
            this.Message    = Message;
            this.Reason     = new StringBuilder();

            if (!Initial)
            {
                this.Reason.AppendLine("Your game threw an exception on our servers,\nplease contact one of the GL Servers Developers with these following informations :");

                if (this.Device.GameMode.Player != null)
                {
                    this.Reason.AppendLine("Your Player Name    : " + this.Device.GameMode.Player.Name + ".");
                    this.Reason.AppendLine("Your Player ID      : " + this.Device.GameMode.Player + ".");
                }

                this.Reason.AppendLine();
                this.Reason.AppendLine("Trace : ");
            }

            this.Reason.AppendLine(this.Message);
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Reason.ToString());
        }
    }
}