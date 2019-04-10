namespace GL.Servers.Library
{
    using System;

    public class Rjindael
    {
        private Random Random               = new Random();

        private const string InitialKey     = "fhsd6f86f67rt8fw78fw789we78r9789wer6re";
        private const string InitialNonce   = "nonce";

        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto7"/> class with
        /// the default key and nonce.
        /// </summary>
        public Rjindael()
        {
            this.InitializeCiphers(Rjindael.InitialKey + Rjindael.InitialNonce);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto7"/> class with
        /// the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException"><paramref name="Rjindael"/> is null.</exception>
        public Rjindael(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.InitializeCiphers(key);
        }

        private RC4 Encryptor { get; set; }
        private RC4 Decryptor { get; set; }

        /// <summary>
        /// Encrypts the provided bytes(plain-text).
        /// </summary>
        /// <param name="data">Bytes to encrypt.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
        public void Encrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            for (int k = 0; k < data.Length; k++)
                data[k] ^= this.Encryptor.PRGA();
        }

        /// <summary>
        /// Decrypts the provided bytes(cipher-text).
        /// </summary>
        /// <param name="data">Bytes to decrypt.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
        public void Decrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            for (int k = 0; k < data.Length; k++)
                data[k] ^= this.Decryptor.PRGA();
        }

        /// <summary>
        /// Update the key with the specified client seed and server nonce.
        /// </summary>
        /// <param name="clientSeed">Client seed.</param>
        /// <param name="serverNonce">Server random nonce.</param>
        /// <exception cref="ArgumentNullException"><paramref name="serverNonce"/> is null.</exception>
        public void UpdateCiphers(int clientSeed, byte[] serverNonce)
        {
            if (serverNonce == null)
                throw new ArgumentNullException("serverNonce");

            var newNonce = Rjindael.ScrambleNonce((ulong) clientSeed, serverNonce);
            var key = Rjindael.InitialKey + newNonce;
            this.InitializeCiphers(key);
        }

        /// <summary>
        /// Generates a random byte array of random length between 15 and 25 bytes long.
        /// </summary>
        /// <returns>The random byte array.</returns>
        public byte[] GenerateNonce()
        {
            var buffer = new byte[this.Random.Next(15, 25)];
            this.Random.NextBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Initializes the ciphers with the specified key.
        /// </summary>
        /// <param name="key">The key used to update the cipher.</param>
        private void InitializeCiphers(string key)
        {
            this.Encryptor = new RC4(key);
            this.Decryptor = new RC4(key);

            for (int k = 0; k < key.Length; k++)
            {
                this.Encryptor.PRGA();
                this.Decryptor.PRGA();
            }
        }

        public static string ScrambleNonce(ulong clientSeed, byte[] serverNonce)
        {
            var scrambler = new Scrambler(clientSeed);
            var byte100 = 0;
            for (int i = 0; i < 100; i++)
                byte100 = scrambler.GetByte();
            var scrambled = string.Empty;
            for (int i = 0; i < serverNonce.Length; i++)
                scrambled += (char)(serverNonce[i] ^ (scrambler.GetByte() & byte100));
            return scrambled;
        }

        private class RC4
        {
            public RC4(byte[] key)
            {
                this.Key = RC4.KSA(key);
            }

            public RC4(string key)
            {
                this.Key = RC4.KSA(RC4.StringToByteArray(key));
            }

            public byte[] Key { get; set; } // "S"

            private byte i { get; set; }
            private byte j { get; set; }

            public byte PRGA()
            {
                /* Pseudo-Random Generation Algorithm
                 * 
                 * The returned value should be XORed with
                 * the data to encrypt or decrypt it.
                 */
                var temp = (byte)0;

                this.i = (byte)((this.i + 1) % 256);
                this.j = (byte)((this.j + this.Key[this.i]) % 256);

                // swap S[i] and S[j];           
                temp = this.Key[this.i];
                this.Key[this.i] = this.Key[this.j];
                this.Key[this.j] = temp;

                return this.Key[(this.Key[this.i] + this.Key[this.j]) % 256]; // value to XOR with data
            }

            private static byte[] KSA(byte[] key)
            {
                /* Key-Scheduling Algorithm
                 * 
                 * Used to initialize key array.
                 */
                var keyLength = key.Length;
                var S = new byte[256];

                for (int i = 0; i != 256; i++) S[i] = (byte)i;

                var j = (byte)0;
                var temp = (byte)0;

                for (int i = 0; i != 256; i++)
                {
                    j = (byte)((j + S[i] + key[i % keyLength]) % 256); // meth is working

                    // swap S[i] and S[j];
                    temp = S[i];
                    S[i] = S[j];
                    S[j] = temp;
                }

                return S;
            }

            private static byte[] StringToByteArray(string str)
            {
                var bytes = new byte[str.Length];
                for (int i = 0; i < str.Length; i++) bytes[i] = (byte)str[i];
                return bytes;
            }
        }

        private class Scrambler
        {
            public Scrambler(ulong seed)
            {
                this.IX = 0;
                this.Buffer = Scrambler.SeedBuffer(seed);
            }

            public int IX { get; set; }
            public ulong[] Buffer { get; set; }

            private static ulong[] SeedBuffer(ulong seed)
            {
                var buffer = new ulong[624];
                for (int i = 0; i < 624; i++)
                {
                    buffer[i] = seed;
                    seed = (1812433253 * ((seed ^ Scrambler.RShift(seed, 30)) + 1)) & 0xFFFFFFFF;
                }

                return buffer;
            }

            public int GetByte()
            {
                var x = (ulong)this.GetInt();
                if (Scrambler.IsNeg(x)) x = Scrambler.Negate(x);
                return (int)(x % 256);
            }

            private int GetInt()
            {
                if (this.IX == 0) this.MixBuffer();
                var val = this.Buffer[this.IX];

                this.IX = (this.IX + 1) % 624;
                val ^= Scrambler.RShift(val, 11) ^ Scrambler.LShift(val ^ Scrambler.RShift(val, 11), 7) & 0x9D2C5680;
                return (int)(Scrambler.RShift(val ^ Scrambler.LShift(val, 15L) & 0xEFC60000, 18L) ^ val ^ Scrambler.LShift(val, 15L) & 0xEFC60000);
            }

            private void MixBuffer()
            {
                var i = 0;
                var j = 0;
                while (i < 624)
                {
                    i += 1;
                    var v4 = (this.Buffer[i % 624] & 0x7FFFFFFF) + (this.Buffer[j] & 0x80000000);
                    var v6 = Scrambler.RShift(v4, 1) ^ this.Buffer[(i + 396) % 624];
                    if ((v4 & 1) != 0) v6 ^= 0x9908B0DF;
                    this.Buffer[j] = v6;
                    j += 1;
                }
            }

            private static ulong RShift(ulong num, ulong n)
            {
                var highbits = (ulong)0;
                if ((num & Scrambler.Pow(2, 31)) != 0) highbits = (Scrambler.Pow(2, n) - 1) * Scrambler.Pow(2, 32 - n);
                return (num / Scrambler.Pow(2, n)) | highbits;
            }

            private static ulong LShift(ulong num, ulong n)
            {
                return (num * Scrambler.Pow(2, n)) % Scrambler.Pow(2, 32);
            }

            private static bool IsNeg(ulong num)
            {
                return (num & (ulong)Math.Pow(2, 31)) != 0;
            }

            private static ulong Negate(ulong num)
            {
                return (~num) + 1;
            }

            private static ulong Pow(ulong x, ulong y)
            {
                return (ulong)Math.Pow(x, y);
            }
        }
    }
}