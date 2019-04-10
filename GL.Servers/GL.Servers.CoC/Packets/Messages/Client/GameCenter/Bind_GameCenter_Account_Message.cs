namespace GL.Servers.CoC.Packets.Messages.Client.GameCenter
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Core.Database;
    using GL.Servers.CoC.Core.Database.Models.Mongo;
    using GL.Servers.CoC.Packets.Messages.Server.GameCenter;
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    using MongoDB.Driver;

    internal class Bind_GameCenter_Account_Message : Message
    {
        private bool Force;
        private string GameCenterId;
        private string Url;
        private string BundleID;

        private byte[] Signature;
        private byte[] Salt;
        private byte[] Timestamp;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14212;
            }
        }

        /// <summary>
        /// Gets a value indicating the server node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_GameCenter_Account_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Bind_GameCenter_Account_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Bind_GameCenter_Account_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Force        = this.Reader.ReadBoolean();

            this.GameCenterId = this.Reader.ReadString();
            this.Url          = this.Reader.ReadString();
            this.BundleID     = this.Reader.ReadString();

            this.Signature    = this.Reader.ReadArray();
            this.Salt         = this.Reader.ReadArray();
            this.Timestamp    = this.Reader.ReadArray();
        }

        /// <summary>
        /// Processes this instances.
        /// </summary>
        internal override async void Process()
        {
            /*
            if (Constants.Database == DBMS.Mongo)
            {
                Player Bounded = await Resources.Players.FindPlayer(T => !(T.HighID == this.Device.GameMode.Level.Player.HighID && T.LowID == this.Device.GameMode.Level.Player.LowID) && T.GameCenterID == this.GameCenterId);

                if (Bounded != null)
                {
                    new GameCenter_Account_Already_Bound_Message(this.Device, Bounded).Send();
                    return;
                }
                
                Mongo.Players.UpdateOne(T => T.HighID == this.Device.GameMode.Level.Player.HighID && T.LowID == this.Device.GameMode.Level.Player.LowID, Builders<Players>.Update.Set(T => T.GameCenterID, this.GameCenterId));
            }

            new GameCenter_Account_Bound_Message(this.Device, 1).Send();
            */
        }
    }
}