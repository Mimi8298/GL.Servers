namespace GL.Servers.CoC.Packets.Commands.Client
{
    using System.Linq;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Log;
    using GL.Servers.CoC.Extensions.Game;

    using GL.Servers.Extensions.Binary;

    internal class Send_Alliance_Mail_Command : Command
    {
        internal string Message;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 537;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Message = Reader.ReadString();
            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (!Level.GameObjectManager.Bunker.Locked)
            {
                BunkerComponent BunkerComponent = Level.GameObjectManager.Bunker.BunkerComponent;

                if (BunkerComponent.CanSendClanMail)
                {
                    if (!string.IsNullOrWhiteSpace(this.Message))
                    {
                        if (this.Message.Length <= 256)
                        {
                            this.Message = Resources.Regex.Replace(this.Message, " ");

                            if (this.Message.StartsWith(" "))
                            {
                                this.Message = this.Message.Remove(0, 1);
                            }

                            if (this.Message.Length > 0)
                            {
                                foreach (Player Player in Level.Player.Alliance.Members.Connected.Values.ToArray())
                                {
                                    Player.Level.GameMode.GameLogManager.Add(new AllianceMailAvatarStreamEntry(Level.Player, Level.Player.Alliance, this.Message));
                                }
                            }

                            BunkerComponent.ClanMailTimer.StartTimer(Level.GameMode.Time, Globals.ClanMailCooldown);
                        }
                    }
                }
            }
        }
    }
}