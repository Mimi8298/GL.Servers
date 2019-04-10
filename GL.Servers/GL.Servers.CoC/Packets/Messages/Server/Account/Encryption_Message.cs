namespace GL.Servers.CoC.Packets.Messages.Server.Account
{
    using System;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Cryptography;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Library;

    internal class Encryption_Message : Message
    {
        private int Seed;
        private byte[] Nonce;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20000;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Encryption_Message(Device Device, int ClientSeed) : base(Device)
        {
            this.Seed = ClientSeed;
            this.Nonce = new byte[16];

            Resources.Random.NextBytes(this.Nonce);
        }
        
        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBytes(this.Nonce);
            this.Data.AddInt(1);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            RC4Encrypter ReceiveEncrypter = (RC4Encrypter) this.Device.ReceiveEncrypter;
            RC4Encrypter SendEncrypter = (RC4Encrypter) this.Device.SendEncrypter;

            string Nonce = Rjindael.ScrambleNonce((uint) this.Seed, this.Nonce);

            ReceiveEncrypter.Init(Factory.RC4Key + Nonce);
            SendEncrypter.Init(Factory.RC4Key + Nonce);
        }
    }
}