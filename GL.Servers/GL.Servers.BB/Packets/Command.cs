namespace GL.Servers.BB.Packets
{
    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Command
    {
        internal int ExecuteSubTick = -1;

        internal virtual int Type
        {
            get
            {
                return 0;
            }
        }

        internal virtual bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        internal virtual bool AllowInHomeState
        {
            get
            {
                return false;
            }
        }

        internal virtual bool AllowInVisitState
        {
            get
            {
                return false;
            }
        }

        internal virtual bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

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
        /// <param name="Player">The player.</param>
        public Command()
        {
            // Command.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(Reader reader)
        {
            this.ExecuteSubTick = reader.ReadInt32();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(ByteWriter writer)
        {
            writer.AddInt(this.ExecuteSubTick);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Execute(Level Level)
        {
            // Process.
        }
    }
}