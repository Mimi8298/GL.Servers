namespace GL.Servers.CR.Packets.Messages.Client.Avatar
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Commands.Server;
    using GL.Servers.CR.Packets.Messages.Server.Avatar;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Change_Avatar_Name_Message : Message
    {
        private string Name;

        /// <summary>
        /// All those niggers needs to be banned forever.
        /// Want to talk soooome shiiit ? Call me.
        /// </summary>
        internal static readonly string[] Filters =
        {
            "admin", "moderator", "founder", "ultrapowa", "clashoflights", "howtoandroid", "eiffel", "metrog", "[", "]", "opegit", "danill"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Avatar_Name_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Change_Avatar_Name_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Change_Avatar_Name_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name = this.Data.ReadString();
            this.Data.ReadVInt();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                if (this.Name.Length >= 2 && this.Name.Length <= 16)
                {
                    for (int i = 0; i < Change_Avatar_Name_Message.Filters.Length; i++)
                    {
                        if (this.Name.Contains(Change_Avatar_Name_Message.Filters[i]))
                        {
                            new Avatar_Name_Change_Failed_Message(this.Device).Send();
                            return;
                        }
                    }

                    if (this.Device.GameMode.Player.NameSetByUser)
                    {
                        if (this.Device.GameMode.Player.NameChangeState > 1)
                        {
                            return;
                        }

                        this.Device.GameMode.CommandManager.AddCommand(new ChangeAvatarNameCommand(this.Name, true, 1));
                    }
                    else
                        this.Device.GameMode.CommandManager.AddCommand(new ChangeAvatarNameCommand(this.Name, true, 0));
                }
                else
                {
                    new Avatar_Name_Change_Failed_Message(this.Device).Send();
                }
            }
            else
            {
                new Avatar_Name_Change_Failed_Message(this.Device).Send();
            }
        }
    }
}