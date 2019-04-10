namespace GL.Proxy.CR.Core.Crypto
{
    using System;
    using System.Linq;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.TweetNaCl;

    public static class PepperCrypto
    {
        internal static byte[] PepperAuthentification(byte[] Packet)
        {
            return Packet;
        }

        internal static byte[] PepperAuthentificationOpen(byte[] Packet)
        {
            return Packet;
        }

        internal static byte[] PepperAuthentificationResponse(byte[] Packet)
        {
            return Packet;
        }

        internal static byte[] PepperAuthentificationResponseOpen(byte[] Packet)
        {
            return Packet;
        }

        internal static byte[] PepperLogin(byte[] BoxPublicKey, byte[] BoxSecretKey, byte[] BlakePublicKey, byte[] AuthSessionKey, byte[] OutNonce, byte[] Data)
        {
            byte[] c = new byte[Data.Length + 32 + 24 + 24];

            Array.Copy(AuthSessionKey, 0, c, 32, 24);
            Array.Copy(OutNonce, 0, c, 32 + 24, 24);
            Array.Copy(Data, 0, c, 32 + 48, Data.Length);

            Blake2BHasher Blake2B = new Blake2BHasher();

            Blake2B.Update(BlakePublicKey);
            Blake2B.Update(BoxPublicKey);
            
            if (curve25519xsalsa20poly1305.crypto_box_afternm(c, c, Blake2B.Finish(), PepperCrypto.SecretKey(BoxPublicKey, BoxSecretKey)) == 0)
            {
                byte[] Encrypted = new byte[c.Length - 16 + 32];

                Array.Copy(BlakePublicKey, 0, Encrypted, 0, 32);
                Array.Copy(c, 16, Encrypted, 32, c.Length - 16);

                return Encrypted;
            }

            Logging.Error(typeof(PepperCrypto) , "PepperLogin() - Unable to encrypt pepper login. curve25519xsalsa20poly1305.crypto_box_afternm != 0");

            return null;
        }

        internal static byte[] PepperLoginOpen(byte[] BlakePublicKey, byte[] BoxSecretKey, byte[] AuthSessionKey, ref byte[] BoxPublicKey, ref byte[] OutNonce, byte[] Data)
        {
            if (Data.Length >= 32)
            {
                Array.Copy(Data, BoxPublicKey = new byte[32], 32);
                
                Blake2BHasher Blake2B = new Blake2BHasher();

                Blake2B.Update(BoxPublicKey);
                Blake2B.Update(BlakePublicKey);

                byte[] c = new byte[16].Concat(Data.Skip(32)).ToArray();
                
                if (curve25519xsalsa20poly1305.crypto_box_afternm(c, c, Blake2B.Finish(), PepperCrypto.SecretKey(BoxPublicKey, BoxSecretKey)) == 0)
                {
                    byte[] SessionKey = new byte[24];
                    byte[] Decrypted = new byte[c.Length - 32 - 48];

                    Array.Copy(c, 32, SessionKey, 0, 24);
                    Array.Copy(c, 32 + 24, OutNonce = new byte[24], 0, 24);
                    Array.Copy(c, 32 + 48, Decrypted, 0, Decrypted.Length);

                    for (int i = 0; i < 24; i++)
                    {
                        if (SessionKey[i] != AuthSessionKey[i])
                        {
                            Logging.Error(typeof(PepperCrypto), "PepperLoginOpen() - Unable to decrypt pepper login. SessionKey != AuthSessionKey.");
                            return null;
                        }
                    }

                    return Decrypted;
                }

                Logging.Error(typeof(PepperCrypto), "PepperLoginOpen() - Unable to decrypt pepper login. curve25519xsalsa20poly1305.crypto_box_afternm != 0.");
            }

            return null;
        }

        /// <summary>
        /// Encryptes the login response message.
        /// </summary>
        internal static byte[] PepperLoginResponse(byte[] BlakeNonce, byte[] BlakePublicKey, byte[] BoxPublicKey, byte[] BoxSecretKey, byte[] OutNonce, byte[] OutKey, byte[] Data)
        {
            Blake2BHasher Blake2B = new Blake2BHasher();

            Blake2B.Update(BlakeNonce);
            Blake2B.Update(BoxPublicKey);
            Blake2B.Update(BlakePublicKey);

            byte[] m = new byte[Data.Length + 88];
            
            Array.Copy(OutNonce, 0, m, 32, 24);
            Array.Copy(OutKey, 0, m, 32 + 24, 32);
            Array.Copy(Data, 0, m, 32 + 24 + 32, Data.Length);
            
            if (curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Blake2B.Finish(), PepperCrypto.SecretKey(BoxPublicKey, BoxSecretKey)) == 0)
            {
                byte[] Encrypted = new byte[m.Length - 16];
                Buffer.BlockCopy(m, 16, Encrypted, 0, m.Length - 16);
                return Encrypted;
            }

            Logging.Error(typeof(PepperCrypto), "PepperLoginResponse() - Unable de encrypt pepper login response. curve25519xsalsa20poly1305.crypto_box_afternm != 0");

            return null;
        }

        internal static byte[] PepperLoginResponseOpen(byte[] BlakeNonce, byte[] BlakePublicKey, byte[] BoxPublicKey, byte[] BoxSecretKey, ref byte[] OutNonce, ref byte[] OutKey, byte[] Data)
        {
            Blake2BHasher Blake2B = new Blake2BHasher();

            Blake2B.Update(BlakeNonce);
            Blake2B.Update(BlakePublicKey);
            Blake2B.Update(BoxPublicKey);
            
            byte[] c = new byte[16].Concat(Data).ToArray();
            
            if (curve25519xsalsa20poly1305.crypto_box_open_afternm(c, c, Blake2B.Finish(), PepperCrypto.SecretKey(BoxPublicKey, BoxSecretKey)) == 0)
            {
                byte[] Decrypted = new byte[c.Length - 32 - 32 - 24];

                Array.Copy(c, 32, OutNonce = new byte[24], 0, 24);
                Array.Copy(c, 32 + 24, OutKey = new byte[32], 0, 32);
                Array.Copy(c, 32 + 24 + 32, Decrypted, 0, Decrypted.Length);
                
                return Decrypted;
            }

            Logging.Error(typeof(PepperCrypto), "PepperLoginResponseOpen() - Unable de decrypt pepper login response. curve25519xsalsa20poly1305.crypto_box_open_afternm != 0");

            return null;
        }
        
        /// <summary>
        /// Encryptes input with secret box.
        /// </summary>
        internal static byte[] SecretBox(byte[] Input, byte[] Nonce, byte[] SecretKey)
        {
            byte[] c = new byte[Input.Length + 32];
            Array.Copy(Input, 0, c, 32, Input.Length);

            xsalsa20poly1305.crypto_secretbox(c, c, c.Length, Nonce, SecretKey);

            byte[] output = new byte[c.Length - 16];
            Array.Copy(c, 16, output, 0, output.Length);
            return output;
        }

        /// <summary>
        /// Decyptes input with secret box.
        /// </summary>
        internal static byte[] SecretBoxOpen(byte[] Input, byte[] Nonce, byte[] SecretKey)
        {
            byte[] c = new byte[Input.Length + 16];
            Array.Copy(Input, 0, c, 16, Input.Length);

            if (xsalsa20poly1305.crypto_secretbox(c, c, c.Length, Nonce, SecretKey) == 0)
            {
                byte[] output = new byte[c.Length - 32];
                Array.Copy(c, 32, output, 0, output.Length);
                return output;
            }

            Logging.Error(typeof(PepperCrypto), "Unable to decrypt secret box message. xsalsa20poly1305.crypto_secretbox_open != 0");

            return null;
        }

        internal static byte[] SecretKey(byte[] PublicKey, byte[] SecretKey)
        {
            byte[] k = new byte[32];
            curve25519xsalsa20poly1305.crypto_box_beforenm(k, PublicKey, SecretKey);
            return k;
        }
    }
}