namespace GL.Servers.BB.Packets.Commands.Client
{
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.GameObject;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Move_Building_Command : Command
    {
        internal int X;
        internal int Y;

        internal int Id;

        /// <summary>
        /// Gets the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 501;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in attack state.
        /// </summary>
        internal override bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in home state.
        /// </summary>
        internal override bool AllowInHomeState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in visit state.
        /// </summary>
        internal override bool AllowInVisitState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a debug command.
        /// </summary>
        internal override bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Move_Building_Command"/> class.
        /// </summary>
        public Move_Building_Command() : base()
        {
            // Move_Building_Command.
        }

        /// <summary>
        /// Decodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Decode(Reader reader)
        {
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt32();
            this.Id = reader.ReadInt32();

            base.Decode(reader);
        }

        /// <summary>
        /// Encodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Encode(ByteWriter writer)
        {
            writer.AddInt(this.X);
            writer.AddInt(this.Y);
            writer.AddInt(this.Id);

            base.Encode(writer);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="Level">The level.</param>
        internal override void Execute(Level Level)
        {
            GameObject GameObject = Level.Player.Home.GameObjectManager.GetGameObjectById(this.Id);

            if (GameObject != null && (GameObject.Type == 4 || GameObject.Type == 6 || GameObject.Type == 0))
            {
                GameObject.X = this.X << 9;
                GameObject.Y = this.Y << 9;
            }
        }
    }
}