namespace GL.Servers.CoC.Packets.Cryptography
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Extensions;
    using GL.Servers.CoC.Packets.Cryptography.State;

    using GL.Servers.Extensions;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.TweetNaCl;

    internal class PepperEncrypter : IEncrypter
    {
        public bool IsRC4
        {
            get
            {
                return false;
            }
        }
        
        internal byte[] Nonce;
        internal PepperState State;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PepperEncrypter"/> class.
        /// </summary>
        internal PepperEncrypter(PepperState State)
        {
            this.State = State;
        }

        /// <summary>
        /// Decryptes the message.
        /// </summary>
        public byte[] Decrypt(int MessageType, byte[] Data)
        {
            switch (MessageType)
            {
                case 10100:
                {
                    return Data;
                }

                case 10101:
                {
                    if (this.State.Authentified)
                    {
                        if (!this.State.Logged)
                        {
                            return this.DecryptLogin(Data);
                        }

                        goto default;
                    }

                    goto case 10100;
                }

                default:
                {
                    if (this.State.Logged)
                    {
                        if (Data.Length >= 16)
                        {
                            this.Nonce.Increment();

                            byte[] m = new byte[Data.Length + 16];

                            Buffer.BlockCopy(Data, 0, m, 16, Data.Length);

                            int Success = xsalsa20poly1305.crypto_secretbox_open(m, m, m.Length, this.Nonce, this.State.SharedKey);

                            if (Success == 0)
                            {
                                byte[] Decrypted = new byte[Data.Length - 16];
                                Buffer.BlockCopy(m, 32, Decrypted, 0, Decrypted.Length);
                                return Decrypted;
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "Unable to decrypt data with secret box method. Error : " + Success);
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Unable to decrypt data with secret box method. Message is too small. Type:" + MessageType + ".");
                        }

                        return null;
                    }

                    goto case 10100;
                }
            }
        }

        /// <summary>
        /// Encryptes the message.
        /// </summary>
        public byte[] Encrypt(int MessageType, byte[] Data)
        {
            switch (MessageType)
            {
                case 20100:
                {
                    this.State.Authentified = true;
                    return Data;
                }

                case 20103:
                case 20104:
                {
                    if (this.State.Authentified)
                    {
                        if (!this.State.Logged)
                        {
                            return this.EncryptLoginResponse(Data);
                        }

                        goto default;
                    }

                    goto case 20100;
                }

                default:
                {
                    if (this.State.Logged)
                    {
                        this.Nonce.Increment();

                        byte[] c = new byte[Data.Length + 32];

                        Buffer.BlockCopy(Data, 0, c, 32, Data.Length);

                        int Success = xsalsa20poly1305.crypto_secretbox(c, c, c.Length, this.Nonce, this.State.SharedKey);

                        if (Success == 0)
                        {
                            byte[] Encrypted = new byte[c.Length - 16];
                            Buffer.BlockCopy(c, 16, Encrypted, 0, c.Length - 16);
                            return Encrypted;
                        }

                        Logging.Error(this.GetType(), "Unable to create a secret box. Error : " + Success);

                        return null;
                    }

                    goto case 20100;
                }
            }
        }

        /// <summary>
        /// Decryptes the login message.
        /// </summary>
        private byte[] DecryptLogin(byte[] Data)
        {
            if (Data.Length >= 32 + 16 + 48)
            {
                this.State.PublicKey = new byte[32];
                this.State.SecretKey = new byte[32];

                this.State.SharedKey = new byte[32];

                this.Nonce = new byte[24];
                
                Array.Copy(Data, 0, this.State.PublicKey, 0, 32);
                
                int Success = curve25519xsalsa20poly1305.crypto_box_beforenm(this.State.SharedKey, this.State.OriginalPublicKey, this.State.PublicKey);

                if (Success == 0)
                {
                    Blake2BHasher Blake2 = new Blake2BHasher();

                    Blake2.Update(this.State.PublicKey);
                    Blake2.Update(this.State.OriginalPublicKey);

                    byte[] Nonce = Blake2.Finish();
                    byte[] m = new byte[Data.Length - 32 + 16];

                    Buffer.BlockCopy(Data, 32, m, 16, Data.Length - 32);

                    Success = curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Nonce, this.State.SharedKey);

                    if (Success == 0)
                    {
                        byte[] SessionKey = new byte[24];

                        Array.Copy(m, 32, SessionKey, 0, 24);

                        if (this.State.SessionKey.IsEquals(SessionKey))
                        {
                            Array.Copy(m, 32 + 24, this.Nonce, 0, 24);

                            this.State.ClientNonce = this.Nonce;

                            byte[] Decrypted = new byte[m.Length - 80];
                            Buffer.BlockCopy(m, 80, Decrypted, 0, Decrypted.Length);
                            return Decrypted;
                        }
                        else
                            Logging.Error(this.GetType(), "Unable to decrypt packet. Error : Invalid session key. \n " + BitConverter.ToString(m, 16) + "\n Pk : " + BitConverter.ToString(this.State.PublicKey) + "\n SPk : " + BitConverter.ToString(this.State.OriginalPublicKey));

                        Console.ReadKey();
                    }
                    else
                        Logging.Error(this.GetType(), "Unable to decrypt packet. Error : " + Success);
                }
                else
                    Logging.Error(this.GetType(), "Unable to generate shared key. Error : " + Success);
            }

            return null;
        }

        /// <summary>
        /// Encryptes the login response message.
        /// </summary>
        private byte[] EncryptLoginResponse(byte[] Data)
        {
            Blake2BHasher Blake2 = new Blake2BHasher();

            Blake2.Update(this.State.ClientNonce);
            Blake2.Update(this.State.PublicKey);
            Blake2.Update(this.State.OriginalPublicKey);

            byte[] Nonce = Blake2.Finish();
            byte[] m = new byte[Data.Length + 88];
            byte[] NewSK = new byte[32];
            
            Resources.Random.NextBytes(this.Nonce = new byte[24]);
            Resources.Random.NextBytes(NewSK);

            Array.Copy(this.Nonce, 0, m, 32, 24);
            Array.Copy(NewSK, 0, m, 56, 32);

            Buffer.BlockCopy(Data, 0, m, 88, Data.Length);

            int Success = curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Nonce, this.State.SharedKey);

            if (Success == 0)
            {
                this.State.Logged = true;
                this.State.SharedKey = NewSK;

                byte[] Encrypted = new byte[m.Length - 16];
                Buffer.BlockCopy(m, 16, Encrypted, 0, m.Length - 16);
                return Encrypted;
            }

            Logging.Error(this.GetType(), "Unable to create a public box. Error : " + Success + ".");

            return null;
        }

        internal class Keys
        {
            internal static readonly Dictionary<int, byte[]> PublicKeys = new Dictionary<int, byte[]>
            {
                {
                    31,
                    new byte[]
                    {
                        0x7e, 0xb1, 0x5f, 0x65, 0xbd, 0xd5, 0x76, 0x61,
                        0x9a, 0xbb, 0xd0, 0xb0, 0x65, 0x0c, 0x45, 0xdb,
                        0x10, 0x20, 0xf5, 0xec, 0x96, 0x9f, 0xcf, 0x48,
                        0xa8, 0x28, 0x42, 0x4f, 0x1b, 0xc8, 0xd8, 0x09
                    }
                },
                {
                    35,
                    new byte[]
                    {
                        0xAC, 0x69, 0xC8, 0xFD, 0x97, 0xBC, 0x0B, 0x2C,
                        0xD2, 0x3B, 0x7A, 0x55, 0xE9, 0x43, 0xD7, 0xE2,
                        0xC9, 0x6F, 0xC3, 0x19, 0x86, 0xFB, 0x8F, 0x96,
                        0x8A, 0xE9, 0xCA, 0x9B, 0x1D, 0xED, 0x1D, 0x5E
                    }
                }
            };
        }
    }
}