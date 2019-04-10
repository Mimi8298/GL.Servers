namespace GL.Servers.CoC.Packets.Messages.Client.Home
{
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Extensions.Utils;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Mode.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Home;
    using GL.Servers.Extensions.Binary;

    internal class Attack_Npc_Message : Message
    {
        internal NpcData Data;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14134;
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

        public Attack_Npc_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Attack_Npc_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Data = this.Reader.ReadData<NpcData>();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Data != null)
            {
                // if (this.Data.SinglePlayer)
                {
                    if (this.Data.AlwaysUnlocked || this.Device.GameMode.Level.Player.NpcMapProgress.CanAttackNPC(this.Data))
                    {
                        if (this.Device.GameMode.State == State.Home)
                        {
                            new Npc_Data_Message(this.Device, LevelFile.Files[this.Data.LevelFile], new NpcPlayer(this.Data), this.Device.GameMode.Level.Player, TimeUtil.Timestamp, 0).Send();
                        }
                    }
                }
            }
        }
    }
}