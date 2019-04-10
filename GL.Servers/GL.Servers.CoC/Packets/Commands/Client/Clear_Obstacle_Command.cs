namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Clear_Obstacle_Command : Command
    {
        private int Id;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 507;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Id = Reader.ReadInt32();
            this.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            Obstacle Obstacle = (Obstacle) Level.GameObjectManager.Filter.GetGameObjectById(this.Id);

            if (Obstacle != null)
            {
                // TODO Process the command.
            }
        }
    }
}