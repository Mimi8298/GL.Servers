namespace GL.Servers.BS.Packets.Messages.Client
{
    using System.Linq;

    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Commands.Server;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Change_Name : Message
    {
        private string Username;
        private int State;

        /// <summary>
        /// All those niggers needs to be banned forever.
        /// Want to talk soooome shiiit ? Call me.
        /// </summary>
        internal readonly string[] Filters =
        {
            "admin", "moderator", "founder",
            "ultrapowa", "clashoflights", "howtoandroid", "eiffel", "metrog",
            "[", "]",
            "opegit", "danill"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Name"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Change_Name(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Change_Name.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Username   = this.Reader.ReadString();
            this.State      = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (!string.IsNullOrEmpty(this.Username))
            {
                if (this.Username.Length >= 2 && this.Username.Length <= 20)
                {
                    if (!this.Filters.Any(this.Username.ToLower().Contains))
                    {
                        this.Device.Player.Name         = this.Username;

                        new Server_Commands(this.Device, new Change_Name_Callback(this.Device, this.Username, this.State)).Send();
                    }
                    else
                    {
                        new Change_Name_Failed(this.Device).Send();
                    }
                }
                else
                {
                    new Change_Name_Failed(this.Device).Send();
                }
            }
            else
            {
                new Change_Name_Failed(this.Device).Send();
            }
        }
    }
}