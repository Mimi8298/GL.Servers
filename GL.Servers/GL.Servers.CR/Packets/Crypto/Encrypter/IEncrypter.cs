namespace GL.Servers.CR.Packets.Crypto.Encrypter
{
    internal interface IEncrypter
    {
        byte[] Decrypt(byte[] Packet);
        byte[] Encrypt(byte[] Packet);
    }
}