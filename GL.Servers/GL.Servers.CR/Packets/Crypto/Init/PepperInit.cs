namespace GL.Servers.CR.Packets.Crypto.Init
{
    public struct PepperInit
    {
        internal byte State;
        internal int KeyVersion;

        internal byte[] Nonce;
        internal byte[] SharedKey;
        internal byte[] SessionKey;
        internal byte[] ClientPublicKey;
        internal byte[] ServerPublicKey;
    }
}