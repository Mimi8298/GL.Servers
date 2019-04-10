namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Move_GameObject_Command : Command
    {
        internal int X;
        internal int Y;

        internal int Id;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 501;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.X  = Reader.ReadInt32();
            this.Y  = Reader.ReadInt32();
            this.Id = Reader.ReadInt32();

            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            GameObject GameObject = Level.GameObjectManager.Filter.GetGameObjectById(this.Id);

            if (GameObject != null)
            {
                if (GameObject.Type == 0 || GameObject.Type == 4 || GameObject.Type == 6)
                {
                    GameObject.SetPositionXY(this.X, this.Y);
                }
            }
        }
    }
}