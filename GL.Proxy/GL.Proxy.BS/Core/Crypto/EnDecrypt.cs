namespace GL.Proxy.BS.Core.Crypto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using GL.Proxy.BS.Core.Network;
    using GL.Proxy.BS.Logic.Enums;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.Library;

    public class EnDecrypt
    {
        private Status State    = Status.Disconnected;

        private Rjindael CRjindael;
        private Rjindael SRjindael;

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
            this.CRjindael = new Rjindael();
            this.SRjindael = new Rjindael();
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
                this.CRjindael.Decrypt(ref DecryptedData);
            }
            else if (Message.Identifier == 20103 || Message.Identifier == 20104)
            {
                this.SRjindael.Decrypt(ref DecryptedData);

                if (Message.Identifier == 20104)
                {
                    this.State = Status.Authentified;
                }
            }
            else
            {
                if (Message.Destination == Destination.FROM_CLIENT)
                {
                    this.CRjindael.Decrypt(ref DecryptedData);
                }
                else
                {
                    this.SRjindael.Decrypt(ref DecryptedData);
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
                this.CRjindael.Encrypt(ref EncryptedData);
            }
            else if (Message.Identifier == 20103 || Message.Identifier == 20104)
            {
                this.SRjindael.Encrypt(ref EncryptedData);
            }
            else
            {
                if (Message.Destination == Destination.FROM_CLIENT)
                {
                    this.CRjindael.Encrypt(ref EncryptedData);
                }
                else
                {
                    this.SRjindael.Encrypt(ref EncryptedData);
                }
            }

            return EncryptedData;
        }
    }
}