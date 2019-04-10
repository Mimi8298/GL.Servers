namespace GL.Servers.SL.Packets.Messages.Client
{
    using System;
    
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    using GL.Servers.SL.Core;
    using GL.Servers.SL.Core.Database;
    using GL.Servers.SL.Core.Network;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Logic.Avatar;
    using GL.Servers.SL.Packets.Messages.Server;

    using MongoDB.Driver;

    using Newtonsoft.Json;

    internal class Bind_GameCenter_Account : Message
    {
        private bool Force;
        private string GameCenterId;
        private string Url;
        private string BundleID;

        private byte[] Signature;
        private byte[] Salt;
        private byte[] Timestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_GameCenter_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Bind_GameCenter_Account(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Bind_GameCenter_Account.
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

        internal override async void Process()
        {
            /* if (Constants.Database == DBMS.Mongo)
            {
                Player Bounded = Resources.Players.FindPlayer(T => !(T.HighID == this.Device.Player.HighID && T.LowID == this.Device.Player.LowID) && T.GameCenterID == this.GameCenterId);

                if (Bounded != null)
                {
                    new GameCenter_Account_Already_Bound(this.Device, Bounded).Send();
                    return;
                }
                
                Mongo.Players.UpdateOne(T => T.HighID == this.Device.Player.HighID && T.LowID == this.Device.Player.LowID, Builders<Players>.Update.Set(T => T.GameCenterID, this.GameCenterId));
            } */

            new GameCenter_Account_Bound(this.Device, 1).Send();
        }
    }
}