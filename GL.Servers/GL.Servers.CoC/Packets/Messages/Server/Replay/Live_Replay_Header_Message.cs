namespace GL.Servers.CoC.Packets.Messages.Server.Replay
{
    using System.Collections.Generic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Packets.Enums;

    internal class Live_Replay_Header_Message : Message
    {
        internal string ReplayHeaderJson;

        internal int InitialStreamEndSubTick;
        internal List<Command> InitialCommands;

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
        /// Initializes a new instance of the <see cref="Live_Replay_Header_Message"/> class.
        /// </summary>
        public Live_Replay_Header_Message(Device Device, string ReplayHeaderJson, int InitialStreamEndSubTick, List<Command> InitialCommands) : base(Device)
        {
            this.ReplayHeaderJson        = ReplayHeaderJson;
            this.InitialStreamEndSubTick = InitialStreamEndSubTick;
            this.InitialCommands         = InitialCommands;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            base.Encode();

            this.Data.AddCompressableString(this.ReplayHeaderJson);
            this.Data.AddInt(this.InitialStreamEndSubTick);
            this.Data.AddInt(0);
            this.Data.AddInt(this.InitialCommands.Count);

            this.InitialCommands.ForEach(Command =>
            {
                CommandManager.EncodeCommand(Command, this.Data);
            });
        }
    }
}