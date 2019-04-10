namespace GL.Servers.CoC.Packets.Messages.Client.Home
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Extensions.Binary;

    internal class Go_Home_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14101;
            }
        }

        /// <summary>
        /// Gets a value indicating the server node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Go_Home_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Go_Home_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Go_Home_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            /*
            switch (this.Device.Player.PlayerState)
            {
                case State.Attack:
                {
                    if (this.Device.Player.BattleManager != null)
                    {
                        if (!this.Device.Player.BattleManager.Enemy.Connected)
                        {
                            Resources.Players.Remove(this.Device.Player.BattleManager.Enemy);
                        }
                        
                        this.Device.Player.BattleManager.Dispose();
                    }

                    new Own_Home_Data_Message(this.Device).Send();

                    break;
                }

                case State.Npc:
                {
                    this.Device.Player.NpcAttack.End();
                    new Own_Home_Data_Message(this.Device).Send();
                    break;
                }

                case PlayerState.Search_PVP:
                {
                    if (Resources.Battles.CancelResearch(this.Device.Player, false))
                    {
                        new Own_Home_Data_Message(this.Device).Send();
                    }

                    break;
                }

                default:
                {
                    new Own_Home_Data_Message(this.Device).Send();
                    break;
                }
            }
            */
        }
    }
}