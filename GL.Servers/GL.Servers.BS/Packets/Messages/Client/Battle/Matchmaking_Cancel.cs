namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Matchmaking_Cancel : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Matchmaking_Cancel"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Matchmaking_Cancel(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Matchmaking_Cancel.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.State == State.MATCHMAKING)
            {
                this.Device.State = State.LOGGED;

                new Matchmaking_Data(this.Device, this.Device.Player).Send();
            }
            else
            {
                if (this.Device.State != State.IN_BATTLE)
                {
                    Logging.Info(this.GetType(), "Warning, player tried to cancel the matchmake but wasn't in a matchmake.");
                }
            }
        }
    }
}