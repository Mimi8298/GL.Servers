namespace GL.Servers.CR.Logic.Commands
{
    using System;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic.Mode;

    using GL.Servers.DataStream;

    internal class Command
    {
        internal int ExecuteTick;
        internal int TickWhenGiven;

        internal int ExecutorHighID;
        internal int ExecutorLowID;
        
        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal virtual int Type
        {
            get
            {
                Logging.Info(this.GetType(), "Type must be overridden.");
                return 0;
            }
        }

        /// <summary>
        /// Gets whether this command is a server command.
        /// </summary>
        internal virtual bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
            // Command.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(ByteStream Packet)
        {
            this.TickWhenGiven  = Packet.ReadVInt();
            this.ExecuteTick    = Packet.ReadVInt();

            this.ExecutorHighID = Packet.ReadVInt();
            this.ExecutorLowID  = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(ChecksumEncoder Packet)
        {
            Packet.EnableCheckSum(false);

            Packet.AddVInt(this.TickWhenGiven);
            Packet.AddVInt(this.ExecuteTick);

            Packet.AddVInt(this.ExecutorHighID);
            Packet.AddVInt(this.ExecutorLowID);

            Packet.EnableCheckSum(true);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal virtual void Execute(GameMode GameMode)
        {
            // Execute.
        }

#if DEBUG
        internal void ShowBuffer(ByteStream Packet)
        {
            Logging.Info(this.GetType(), BitConverter.ToString(Packet.ToArray(Packet.Offset, Packet.Length - Packet.Offset)));
        }
#endif
    }
}