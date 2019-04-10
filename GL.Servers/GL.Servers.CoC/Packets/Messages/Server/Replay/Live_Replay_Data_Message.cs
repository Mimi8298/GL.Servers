namespace GL.Servers.CoC.Packets.Messages.Server.Replay
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Packets.Enums;

    internal class Live_Replay_Data_Message : Message
    {
        internal int EndSubTick;
        internal List<Command> Commands;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24118;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Live_Replay_Data_Message"/> class.
        /// </summary>
        public Live_Replay_Data_Message(Device Device, int EndSubTick, List<Command> Commands) : base(Device)
        {
            this.EndSubTick              = EndSubTick;
            this.Commands                = Commands;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            base.Encode();
            
            this.Data.AddVInt(this.EndSubTick);
            this.Data.AddVInt(0);
            this.Data.AddVInt(this.Commands.Count);

            this.Commands.ForEach(Command =>
            {
                CommandManager.EncodeCommand(Command, this.Data);
            });
        }
    }
}