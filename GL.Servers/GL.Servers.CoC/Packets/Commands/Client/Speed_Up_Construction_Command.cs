namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Speed_Up_Construction_Command : Command
    {
        internal int Id;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 504;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
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
                switch (GameObject.Type)
                {
                    case 0:
                    {
                        ((Building) GameObject).SpeedUpConstruction();
                        break;
                    }
                    default:
                    {
                        Logging.Error(this.GetType(), "Unable to speed up the construction. GameObject Type : " + GameObject.Type + ".");
                        break;
                    }
                }
            }
            else
                Logging.Error(this.GetType(), "Unable to speed up the construction. The game object doesn't exist.");
        }
    }
}