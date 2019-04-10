namespace GL.Servers.CoC.Packets.Messages.Server.GlobalChat
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    
    internal class Global_Chat_Line_Message : Message
    {
        internal Player Player;
        internal Home Home;
        internal string Message;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24715;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.GlobalChat;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Global_Chat_Line_Message(Device Device, Player Player, Home Home, string Message) : base(Device)
        {
            this.Player  = Player;
            this.Home    = Home;
            this.Message = Message;
        }

        internal override void Encode()
        {
            this.Data.AddString(this.Message);
            this.Data.AddString(this.Player.Name);

            this.Data.AddInt(this.Player.ExpLevel);
            this.Data.AddInt(this.Player.League);

            this.Data.AddInt(this.Player.HighID);
            this.Data.AddInt(this.Player.LowID);
            this.Data.AddInt(this.Home.HighID); // HomeID
            this.Data.AddInt(this.Home.LowID);

            if (this.Player.InAlliance)
            {
                this.Data.AddBoolean(true);

                this.Data.AddLong(this.Player.AllianceID);
                this.Data.AddString(this.Player.Alliance.Header.Name);
                this.Data.AddInt(this.Player.Alliance.Header.Badge);
            }
            else
                this.Data.AddBoolean(false);
        }
    }
}