namespace GL.Servers.BB.Packets.Cryptography
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions;
    using GL.Servers.Extensions;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.TweetNaCl;

    internal class Sodium
    {
        internal byte[] OriginalPublicKey;

        internal byte[] PublicKey;
        internal byte[] SecretKey;

        internal byte[] PublicSharedKey;
        internal byte[] SecretSharedKey;

        internal byte[] SessionKey;

        internal byte[] RNonce;
        internal byte[] SNonce;

        internal Blake2BHasher Blake2;

        internal bool Authentified;
        internal bool Logged;

        /// <summary>
        /// Creates a new instance of the <see cref="Sodium"/> class.
        /// </summary>
        public Sodium()
        {
            // Sodium.
        }

        /// <summary>
        /// Decrytes the encrypted data with sodium method.
        /// </summary>
        /// <param name="MessageType">The message type.</param>
        /// <param name="Data">The encrypted data.</param>
        /// <returns>The decrypted data.</returns>
        public byte[] Decrypt(int MessageType, byte[] Data)
        {
            if (MessageType == 10100)
            {
                return Data;
            }

            if (MessageType == 10101)
            {
                return this.DecryptLogin(Data);
            }

            if (this.Logged)
            {
                if (Data.Length >= 16)
                {
                    this.RNonce.Increment();

                    byte[] m = new byte[Data.Length + 16];

                    Buffer.BlockCopy(Data, 0, m, 16, Data.Length);

                    int Success = xsalsa20poly1305.crypto_secretbox_open(m, m, m.Length, this.RNonce, this.SecretSharedKey);

                    if (Success == 0)
                    {
                        byte[] Decrypted = new byte[Data.Length - 16];
                        Buffer.BlockCopy(m, 32, Decrypted, 0, Decrypted.Length);
                        return Decrypted;
                    }
                    else
                        Logging.Error(this.GetType(), "Unable to decrypt data with secret box method. Error : " + Success);
                }
                else Logging.Error(this.GetType(), "Unable to decrypt data with secret box method. Message is too small.");


                return null;
            }

            return Data;
        }

        /// <summary>
        /// Encryptes the data with sodium method.
        /// </summary>
        /// <param name="MessageType">The message type.</param>
        /// <param name="Data">The decrypted data.</param>
        /// <returns>The encrypted data.</returns>
        public byte[] Encrypt(int MessageType, byte[] Data)
        {
            if (MessageType == 20100)
            {
                this.Authentified = true;
                return Data;
            }

            if ((MessageType == 20103 || MessageType == 20104) && !this.Logged)
            {
                return this.EncryptLoginResponse(Data);
            }

            if (this.Logged)
            {
                this.SNonce.Increment();

                byte[] c = new byte[Data.Length + 32];

                Buffer.BlockCopy(Data, 0, c, 32, Data.Length);

                int Success = xsalsa20poly1305.crypto_secretbox(c, c, c.Length, this.SNonce, this.SecretSharedKey);

                if (Success == 0)
                {
                    byte[] Encrypted = new byte[c.Length - 16];
                    Buffer.BlockCopy(c, 16, Encrypted, 0, c.Length - 16);
                    return Encrypted;
                }

                Logging.Error(this.GetType(), "Unable to create a secret box. Error : " + Success);

                return null;
            }

            return Data;
        }

        /// <summary>
        /// Decryptes the login data.
        /// </summary>
        /// <param name="Data">The encrypted data.</param>
        /// <returns>The decrypted data.</returns>
        private byte[] DecryptLogin(byte[] Data)
        {
            if (Data.Length >= 32 + 16 + 48)
            {
                if (this.Authentified && this.OriginalPublicKey != null)
                {
                    if (!this.Logged)
                    {
                        this.PublicKey = new byte[32];
                        this.SecretKey = new byte[32];

                        this.PublicSharedKey = new byte[32];

                        this.RNonce = new byte[24];

                        Logging.Info(this.GetType(), "Public Key : " + BitConverter.ToString(this.PublicKey));

                        // Array.Copy(Data, 0, this.SecretKey, 0, 32); // BB
                        Array.Copy(Data, 0, this.PublicKey, 0, 32); // CoC - HD - BB v2

                        /*
                         * Patch : CoC - HD - BB v2 : The client use as secret key the public key generated with the original public key by the client.
                         *         BB               : The client send instead the public key the secret key in pepper login.
                         * */

                        // int Success = curve25519xsalsa20poly1305.crypto_box_getpublickey(this.PublicKey, this.SecretKey); // BB
                        // Success = curve25519xsalsa20poly1305.crypto_box_beforenm(this.PublicSharedKey, this.OriginalPublicKey, this.PublicKey); // BB

                        int Success = curve25519xsalsa20poly1305.crypto_box_beforenm(this.PublicSharedKey, this.OriginalPublicKey, this.PublicKey); // CoC - HD - BB v2

                        if (Success == 0)
                        {
                            this.Blake2 = new Blake2BHasher();
                            this.Blake2.Update(this.PublicKey);
                            this.Blake2.Update(this.OriginalPublicKey);

                            byte[] Nonce = this.Blake2.Finish();
                            byte[] m = new byte[Data.Length - 32 + 16];

                            Buffer.BlockCopy(Data, 32, m, 16, Data.Length - 32);

                            Success = curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Nonce, this.PublicSharedKey);

                            if (Success == 0)
                            {
                                byte[] SessionKey = new byte[24];

                                Array.Copy(m, 32, SessionKey, 0, 24);
                                
                                if (this.SessionKey.IsEquals(SessionKey))
                                {
                                    Array.Copy(m, 32 + 24, this.RNonce, 0, 24);

                                    byte[] Decrypted = new byte[m.Length - 80];
                                    Buffer.BlockCopy(m, 80, Decrypted, 0, Decrypted.Length);
                                    return Decrypted;
                                }
                                else Logging.Error(this.GetType(), "Unable to decrypt packet. Error : Invalid session key.");
                            }
                            else Logging.Error(this.GetType(), "Unable to decrypt packet. Error : " + Success);
                        }
                        else Logging.Error(this.GetType(), "Unable to generate shared key. Error : " + Success);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Encryptes the login response data.
        /// </summary>
        /// <param name="Data">The decrypted data.</param>
        /// <returns>The encrypted data.</returns>
        private byte[] EncryptLoginResponse(byte[] Data)
        {
            if (this.Authentified)
            {
                this.Blake2.Init();
                this.Blake2.Update(this.RNonce);
                this.Blake2.Update(this.PublicKey);
                this.Blake2.Update(this.OriginalPublicKey);

                byte[] Nonce = this.Blake2.Finish();
                byte[] m = new byte[Data.Length + 88];

                this.SNonce = new byte[24];
                this.SecretSharedKey = new byte[32];

                Resources.Random.NextBytes(this.SNonce);
                Resources.Random.NextBytes(this.SecretSharedKey);

                Array.Copy(this.SNonce, 0, m, 32, 24);
                Array.Copy(this.SecretSharedKey, 0, m, 56, 32);
                Buffer.BlockCopy(Data, 0, m, 88, Data.Length);

                int Success = curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Nonce, this.PublicSharedKey);

                if (Success == 0)
                {
                    this.Logged = true;

                    this.OriginalPublicKey = null;
                    this.PublicSharedKey = null;
                    this.SessionKey = null;
                    this.PublicKey = null;
                    this.Blake2 = null;

                    byte[] Encrypted = new byte[m.Length - 16];
                    Buffer.BlockCopy(m, 16, Encrypted, 0, m.Length - 16);
                    return Encrypted;
                }

                Logging.Error(this.GetType(), "Unable to create a public box. Error : " + Success + ".");
            }

            return Data;
        }

        /// <summary>
        /// The sodium keys.
        /// </summary>
        internal class Keys
        {
            internal static readonly Dictionary<int, byte[]> PublicKeys = new Dictionary<int, byte[]>
            {
                {
                    6,
                    new byte[]
                    {
                        0xF6, 0xE0, 0x7C, 0x02, 0xB7, 0x28, 0xDF, 0xEE,
                        0x9A, 0x84, 0x92, 0x04, 0xB2, 0xAC, 0x1A, 0x30,
                        0x04, 0xAE, 0x3C, 0xE1, 0x91, 0x09, 0x2A, 0xE9,
                        0xAE, 0x51, 0x88, 0xDB, 0x41, 0x76, 0x85, 0x28
                    }
                }
            };
        }
    }
}