namespace GL.Servers.Library
{
    using System;

    public class Crypto : IDisposable
    {
        public byte[] SNonce;
        public byte[] RNonce;

        public byte[] PublicKey;
        public byte[] PrivateKey;

        public byte[] SharedKey;
        public byte[] SessionKey;

        public Sodium.Sodium Sodium;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto"/> class.
        /// </summary>
        public Crypto()
        {
            this.PublicKey  = new byte[32];
            this.PrivateKey = new byte[32];

            this.SharedKey  = new byte[32];
            this.SessionKey = new byte[24];

            this.SNonce     = new byte[24];
            this.RNonce     = new byte[24];
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées 
        /// à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.SNonce     = null;
            this.RNonce     = null;

            this.SharedKey  = null;
            this.SessionKey = null;

            this.PublicKey  = null;
            this.PrivateKey = null;
        }
    }
}