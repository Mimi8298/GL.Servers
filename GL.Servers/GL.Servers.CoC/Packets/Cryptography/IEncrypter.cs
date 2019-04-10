namespace GL.Servers.CoC.Packets.Cryptography
{
    public interface IEncrypter
    {
        /// <summary>
        /// Gets a value indicating whether the encrypter is Rc4 encrypter.
        /// </summary>
        bool IsRC4
        {
            get;
        }

        /// <summary>
        /// Decryptes the message data.
        /// </summary>
        byte[] Decrypt(int MessageType, byte[] Data);

        /// <summary>
        /// Encryptes the message data.
        /// </summary>
        byte[] Encrypt(int MessageType, byte[] Data);
    }
}