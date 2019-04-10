namespace GL.Servers.Library.Sodium
{
    using System;

    using GL.Servers.Library.TweetNaCl;

    public class PublicBox
    {
        private const int BEFORENMBYTES = 32;
        private const int BOXZEROBYTES  = 16;
        private const int ZEROBYTES     = 32;

        public readonly byte[] PrecomputedSharedKey = new byte[PublicBox.BEFORENMBYTES];

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicBox"/> class.
        /// </summary>
        /// <param name="Privatekey">The privatekey.</param>
        /// <param name="Publickey">The publickey.</param>
        public PublicBox(byte[] Privatekey, byte[] Publickey)
        {
            curve25519xsalsa20poly1305.crypto_box_beforenm(this.PrecomputedSharedKey, Publickey, Privatekey);
        }

        /// <summary>
        /// Decrypts the specified cipher.
        /// </summary>
        /// <param name="_Cipher">The cipher.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <exception cref="System.Exception">PublicBox Decryption failed</exception>
        public byte[] Decrypt(byte[] _Cipher, byte[] _Nonce)
        {
            int cipherLength = _Cipher.Length;
            byte[] paddedbuffer = new byte[cipherLength + PublicBox.BOXZEROBYTES];
            Array.Copy(_Cipher, 0, paddedbuffer, PublicBox.BOXZEROBYTES, cipherLength);
            
            if (curve25519xsalsa20poly1305.crypto_box_afternm(paddedbuffer, paddedbuffer, paddedbuffer.Length, _Nonce, this.PrecomputedSharedKey) != 0)
            {
                throw new Exception("PublicBox Decryption failed");
            }

            byte[] output = new byte[paddedbuffer.Length - PublicBox.ZEROBYTES];
            Array.Copy(paddedbuffer, PublicBox.ZEROBYTES, output, 0, output.Length);
            return output;
        }

        /// <summary>
        /// Encrypts the specified plain.
        /// </summary>
        /// <param name="_Plain">The plain.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <exception cref="System.Exception">PublicBox Encryption failed</exception>
        public byte[] Encrypt(byte[] _Plain, byte[] _Nonce)
        {
            int plainLength = _Plain.Length;
            byte[] paddedbuffer = new byte[plainLength + PublicBox.ZEROBYTES];
            Array.Copy(_Plain, 0, paddedbuffer, PublicBox.ZEROBYTES, plainLength);

            if (curve25519xsalsa20poly1305.crypto_box_afternm(paddedbuffer, paddedbuffer, paddedbuffer.Length, _Nonce, this.PrecomputedSharedKey) != 0)
            {
                throw new Exception("PublicBox Encryption failed");
            }

            byte[] output = new byte[plainLength + PublicBox.BOXZEROBYTES];
            Array.Copy(paddedbuffer, PublicBox.ZEROBYTES - PublicBox.BOXZEROBYTES, output, 0, output.Length);
            return output;
        }
    }
}