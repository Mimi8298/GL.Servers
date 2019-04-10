namespace GL.Proxy.BB.Core.Crypto
{
    using System;
    using System.Linq;

    using GL.Proxy.BB.Core.Network;
    using GL.Proxy.BB.Logic.Enums;

    public class EnDecrypt
    {
        private Status State    = Status.Disconnected;

        private enum Status
        {
            Disconnected,
            Session,
            Authentification,
            Authentified
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnDecrypt"/> class.
        /// </summary>
        public EnDecrypt()
        {

        }

        /// <summary>
        /// Decrypts the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public byte[] Decrypt(Packet Message)
        {
            byte[] EncryptedData    = Message.Payload;
            byte[] DecryptedData    = new byte[Message.Length];

            Array.Copy(EncryptedData, DecryptedData, EncryptedData.Length);

            if (Message.Identifier == 10101)
            {
                // Decrypt.
            }
            else if (Message.Identifier == 20103 || Message.Identifier == 20104)
            {
                // Decrypt.

                if (Message.Identifier == 20104)
                {
                    this.State = Status.Authentified;
                }
            }
            else
            {
                if (Message.Destination == Destination.FROM_CLIENT)
                {
                    // Decrypt.
                }
                else
                {
                    // Decrypt.
                }
            }

            return DecryptedData;
        }

        /// <summary>
        /// Encrypts the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public byte[] Encrypt(Packet Message)
        {
            byte[] DecryptedData = Message.Decrypted_Data.ToArray();
            byte[] EncryptedData = new byte[DecryptedData.Length];

            Array.Copy(DecryptedData, EncryptedData, DecryptedData.Length);

            if (Message.Identifier == 10101)
            {
                // Encrypt.
            }
            else if (Message.Identifier == 20103 || Message.Identifier == 20104)
            {
                // Encrypt.
            }
            else
            {
                if (Message.Destination == Destination.FROM_CLIENT)
                {
                    // Encrypt.
                }
                else
                {
                    // Encrypt.
                }
            }

            return EncryptedData;
        }
    }
}