namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Matchmaking_Command : Command
    {
        public Matchmaking_Command() : base()
        {
            
        }

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 800;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            Reader.ReadInt32();
            Reader.ReadInt32();

            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (Level.Player.Gold >= Level.Player.TownhallLevelData.AttackCost)
            {
                // Resources.Battles.StartResearch(this.Device.Player);
            }
        }
    }
}