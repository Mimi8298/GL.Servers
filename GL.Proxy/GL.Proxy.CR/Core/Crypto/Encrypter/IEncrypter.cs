namespace GL.Proxy.CR.Core.Crypto.Encrypter
{
    internal interface IEncrypter
    {
        byte[] Decrypt(byte[] Packet);
        byte[] Encrypt(byte[] Packet);
    }
}