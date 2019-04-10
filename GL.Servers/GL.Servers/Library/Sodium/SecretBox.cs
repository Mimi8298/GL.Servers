namespace GL.Servers.Library.Sodium
{
    using System;

    using GL.Servers.Library.TweetNaCl;

    public class SecretBox
    {
        private const int SHAREDKEYLENGTH = 32;
        public byte[] KnownSharedKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretBox"/> class.
        /// </summary>
        /// <param name="PublicKey">The public key.</param>
        internal SecretBox(byte[] PublicKey)
        {
            this.KnownSharedKey = PublicKey;
        }

        internal byte[] Decrypt(byte[] _Cipher, byte[] _Nonce)
        {
            int cipherLength = _Cipher.Length;
            byte[] buffer = new byte[cipherLength];

            if (xsalsa20poly1305.crypto_secretbox_open(buffer, _Cipher, cipherLength, _Nonce, this.KnownSharedKey) != 0)
            {
                throw new Exception("Error when decrypting a packet.");
            }

            byte[] final = new byte[buffer.Length - SecretBox.SHAREDKEYLENGTH];
            Array.Copy(buffer, SecretBox.SHAREDKEYLENGTH, final, 0, buffer.Length - SecretBox.SHAREDKEYLENGTH);
            return final;
        }

        internal byte[] Encrypt(byte[] _Plain, byte[] _Nonce)
        {
            int plainLength = _Plain.Length;
            byte[] paddedMessage = new byte[plainLength + SecretBox.SHAREDKEYLENGTH];
            Array.Copy(_Plain, 0, paddedMessage, SecretBox.SHAREDKEYLENGTH, plainLength);

            byte[] buffer = new byte[paddedMessage.Length];

            if (xsalsa20poly1305.crypto_secretbox(buffer, paddedMessage, paddedMessage.Length, _Nonce, this.KnownSharedKey) != 0)
            {
                throw new Exception("Error when encrypting a packet.");
            }

            return buffer;
        }
    }
}