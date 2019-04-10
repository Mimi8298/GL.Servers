namespace GL.Servers.CoC.Packets.Cryptography
{
    using GL.Servers.CoC.Extensions;

    public class RC4Encrypter : IEncrypter
    {
        /// <summary>
        /// Gets a value indicating whether the encrypter is a rc4 encrypter.
        /// </summary>
        public bool IsRC4
        {
            get
            {
                return true;
            }
        }

        private byte i;
        private byte j;

        private byte[] Key;

        /// <summary>
        /// Initializes a new instance of the <see cref="RC4Encrypter"/> class.
        /// </summary>
        public RC4Encrypter(string Key)
        {
            this.Init(Key);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RC4Encrypter"/> class.
        /// </summary>
        public RC4Encrypter(string Key, string Nonce)
        {
            this.Init(Key + Nonce);
        }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        internal void Init(string Key)
        {
            this.i = 0;
            this.j = 0;

            this.Key = this.KSA(Extension.StringToByteArray(Key));

            for (int k = 0; k < Key.Length; k++)
            {
                this.PRGA();
            }
        }

        /// <summary>
        /// Returns a random byte contained in the key and swap 2 elements in key.
        /// </summary>
        /// <returns></returns>
        internal byte PRGA()
        {
            this.i = (byte) (this.i + 1);
            this.j = (byte) (this.j + this.Key[this.i]);

            byte SwapTemp = this.Key[this.i];

            this.Key[this.i] = this.Key[this.j];
            this.Key[this.j] = SwapTemp;

            return this.Key[(this.Key[this.i] + this.Key[this.j]) % 256];
        }

        /// <summary>
        /// Returns the generated key using Key Scheduling Algorithm.
        /// </summary>
        private byte[] KSA(byte[] key)
        {
            var keyLength = key.Length;
            var S = new byte[256];

            for (int i = 0; i != 256; i++)
            {
                S[i] = (byte) i;
            }

            byte j = 0;
            byte SwapTemp;

            for (int i = 0; i != 256; i++)
            {
                j = (byte)((j + S[i] + key[i % keyLength]) % 256); // meth is working
                
                SwapTemp = S[i];
                S[i] = S[j];
                S[j] = SwapTemp;
            }

            return S;
        }
        
        /// <summary>
        /// Decryptes the message data.
        /// </summary>
        public byte[] Decrypt(int MessageType, byte[] Data)
        {
            int Length = Data.Length;

            if (Length > 0)
            {
                for (int i = 0; i < Length; i++)
                {
                    Data[i] ^= this.PRGA();
                }
            }
            
            return Data;
        }

        /// <summary>
        /// Encryptes the message data.
        /// </summary>
        public byte[] Encrypt(int MessageType, byte[] Data)
        {
            int Length = Data.Length;

            if (Length > 0)
            {
                for (int i = 0; i < Length; i++)
                {
                    Data[i] ^= this.PRGA();
                }
            }

            return Data;
        }
    }
}