namespace GL.Servers.Library.Sodium
{
    using System.Linq;

    using GL.Servers.Extensions;
    using GL.Servers.Library.TweetNaCl;

    public class Sodium
    {
        private PublicBox Public;
        private SecretBox Secret;

        public Crypto Keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sodium"/> class.
        /// </summary>
        public Sodium(Crypto Keys)
        {
            this.Keys           = Keys;

            this.Public         = new PublicBox(this.Keys.PrivateKey, this.Keys.PublicKey);
            this.Secret         = new SecretBox(this.Public.PrecomputedSharedKey);

            this.Keys.SharedKey = this.Public.PrecomputedSharedKey;

            curve25519xsalsa20poly1305.crypto_box_getpublickey(Keys.PublicKey, Keys.PrivateKey);
        }

        /// <summary>
        /// Encrypts the specified packet.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        /// <param name="Nonce">The nonce.</param>
        public byte[] Encrypt(byte[] Packet, byte[] Nonce)
        {
            return this.Public.Encrypt(Packet, Nonce);
        }

        /// <summary>
        /// Decrypts the specified packet.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        /// <param name="Nonce">The nonce.</param>
        public byte[] Decrypt(byte[] Packet, byte[] Nonce)
        {
            return this.Public.Decrypt(Packet, Nonce);
        }

        /// <summary>
        /// Encrypts the specified packet.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public byte[] Encrypt(byte[] Packet)
        {
            int Add = 2;

            for (int i = 0; i < 24; i++)
            {
                int val = Add + this.Keys.SNonce[i];
                this.Keys.SNonce[i] = (byte) val;
                Add = val / 256;
            }

            return this.Secret.Encrypt(Packet, this.Keys.SNonce).Skip(16).ToArray();
        }

        /// <summary>
        /// Decrypts the specified packet.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public byte[] Decrypt(byte[] Packet)
        {
            int Add = 2;

            for (int i = 0; i < 24; i++)
            {
                int val = Add + this.Keys.SNonce[i];
                this.Keys.SNonce[i] = (byte)val;
                Add = val / 256;
            }

            return new byte[16].Concat(this.Secret.Decrypt(Packet, this.Keys.RNonce)).ToArray();
        }
    }
}