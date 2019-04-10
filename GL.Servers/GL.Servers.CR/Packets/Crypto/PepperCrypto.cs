namespace GL.Servers.CR.Packets.Crypto
{
    using System;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Packets.Crypto.Encrypter;
    using GL.Servers.CR.Packets.Crypto.Init;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.TweetNaCl;

    public static class PepperCrypto
    {
        internal static byte[] HandlePepperAuthentification(ref PepperInit Init, byte[] Packet)
        {
            ++Init.State;
            return Packet;
        }

        internal static byte[] HandlePepperLogin(ref PepperInit Init, byte[] Packet)
        {
            if (Packet.Length >= 32)
            {
                ++Init.State;

                Array.Copy(Packet, Init.ClientPublicKey = new byte[32], 32);
                
                Blake2BHasher Blake2B = new Blake2BHasher();

                Blake2B.Update(Init.ClientPublicKey);
                Blake2B.Update(Init.ServerPublicKey);

                byte[] c = new byte[Packet.Length - 16];
                Array.Copy(Packet, 32, c, 16, Packet.Length - 32);
                
                curve25519xsalsa20poly1305.crypto_box_beforenm(Init.SharedKey = new byte[32], Init.ClientPublicKey, PepperFactory.ServerSecretKeys[Init.KeyVersion]);

                if (curve25519xsalsa20poly1305.crypto_box_afternm(c, c, Blake2B.Finish(), Init.SharedKey) == 0)
                {
                    byte[] sk = new byte[24];
                    Array.Copy(c, 32, sk, 0, 24);
                    
                    for (int i = 0; i < 24; i++)
                    {
                        if (sk[i] != Init.SessionKey[i])
                        {
                            Logging.Error(typeof(PepperCrypto), "HandlePepperLogin() - Session Key is not valid.");
                            return null;
                        }
                    }

                    Array.Copy(c, 56, Init.Nonce = new byte[24], 0, 24);

                    byte[] decrypted = new byte[c.Length - 80];
                    Array.Copy(c, 80, decrypted, 0, decrypted.Length);
                    return decrypted;
                }

                Logging.Error(typeof(PepperCrypto), "HandlePepperLogin() - Unable to handle pepper login. curve25519xsalsa20poly1305.crypto_box_afternm != 0.");
            }

            return null;
        }

        /// <summary>
        /// Encryptes the login response message.
        /// </summary>
        internal static byte[] SendPepperLoginResponse(ref PepperInit Init, out IEncrypter SendEncrypter, out IEncrypter ReceiveEncrypter, byte[] Data)
        {
            ++Init.State;

            Blake2BHasher Blake2 = new Blake2BHasher();

            Blake2.Update(Init.Nonce);
            Blake2.Update(Init.ClientPublicKey);
            Blake2.Update(Init.ServerPublicKey);

            byte[] m = new byte[Data.Length + 88];

            byte[] SendNonce = new byte[24];
            byte[] SecretKey = new byte[32];

            Resources.Random.NextBytes(SendNonce);
            Resources.Random.NextBytes(SecretKey);

            SendEncrypter = new PepperEncrypter(SendNonce, SecretKey);
            ReceiveEncrypter = new PepperEncrypter(Init.Nonce, SecretKey);

            Buffer.BlockCopy(SendNonce, 0, m, 32, 24);
            Buffer.BlockCopy(SecretKey, 0, m, 56, 32);
            Buffer.BlockCopy(Data, 0, m, 88, Data.Length);
            
            if (curve25519xsalsa20poly1305.crypto_box_afternm(m, m, Blake2.Finish(), Init.SharedKey) == 0)
            {
                byte[] Encrypted = new byte[m.Length - 16];
                Buffer.BlockCopy(m, 16, Encrypted, 0, m.Length - 16);
                return Encrypted;
            }

            Logging.Error(typeof(PepperCrypto), "Unable de send pepper login response.");

            return null;
        }

        /// <summary>
        /// Encryptes the authentification message.
        /// </summary>
        internal static byte[] SendPepperAuthentificationResponse(ref PepperInit Init, byte[] Data)
        {
            ++Init.State;
            return Data;
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

            xsalsa20poly1305.crypto_secretbox(c, c, c.Length, Nonce, SecretKey);

            byte[] output = new byte[c.Length - 32];
            Array.Copy(c, 32, output, 0, output.Length);
            return output;
        }
    }
}