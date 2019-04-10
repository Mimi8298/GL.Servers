namespace GL.Servers.CoC.Packets.Messages.Client.Avatar
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Commands.Server;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Change_Avatar_Name_Message : Message
    {
        internal string AvatarName;
        internal bool NameSetByUser;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10212;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Avatar_Name_Message"/> class.
        /// </summary>
        public Change_Avatar_Name_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.AvatarName    = this.Reader.ReadString();
            this.NameSetByUser = this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (!this.Device.GameMode.CommandManager.ChangeNameOnGoing)
            {
                if (!string.IsNullOrEmpty(this.AvatarName))
                {
                    if (!this.Device.GameMode.Level.Player.NameSetByUser == this.NameSetByUser)
                    {
                        if (this.AvatarName.Length <= 16)
                        {
                            this.AvatarName = Resources.Regex.Replace(this.AvatarName, " ");

                            if (this.AvatarName.StartsWith(" "))
                            {
                                this.AvatarName = this.AvatarName.Remove(0, 1);
                            }

                            if (this.AvatarName.Length >= 2)
                            {
                                this.Device.GameMode.CommandManager.ChangeNameOnGoing = true;
                                this.Device.GameMode.CommandManager.AddCommand(new Change_Avatar_Name_Command(this.AvatarName, this.Device.GameMode.Level.Player.ChangeNameCount));
                            }
                        }
                    }
                }
            }
        }
    }
}