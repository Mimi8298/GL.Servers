namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Swap_GameObject_Command : Command
    {
        internal int Id1;
        internal int Id2;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 577;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Id1 = Reader.ReadInt32();
            this.Id2 = Reader.ReadInt32();

            base.Decode(Reader);
        }

        internal override void Execute(Level Level)
        {
            GameObject GameObject1 = Level.GameObjectManager.Filter.GetGameObjectById(this.Id1);
            GameObject GameObject2 = Level.GameObjectManager.Filter.GetGameObjectById(this.Id2);

            if (GameObject1 != null && GameObject2 != null)
            {
                if ((GameObject1.Type == 0 || GameObject1.Type == 4 || GameObject1.Type == 6) && (GameObject2.Type == 0 || GameObject2.Type == 4 || GameObject2.Type == 6))
                {
                    int X = GameObject1.TileX;
                    int Y = GameObject1.TileY;

                    GameObject1.SetPositionXY(GameObject2.TileX, GameObject2.TileY);
                    GameObject2.SetPositionXY(X, Y);
                }
            }
        }
    }
}