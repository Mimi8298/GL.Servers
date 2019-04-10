namespace GL.Servers.CoC.Packets.Cryptography.State
{
    public class PepperState
    {
        public bool Logged;
        public bool Authentified;

        public byte[] OriginalPublicKey;

        public byte[] PublicKey;
        public byte[] SecretKey;

        public byte[] ClientNonce;
        public byte[] SessionKey;

        public byte[] SharedKey;

        /// <summary>
        /// Loads the key.
        /// </summary>
        internal bool LoadKey(int KeyIndex)
        {
            return PepperEncrypter.Keys.PublicKeys.TryGetValue(KeyIndex, out this.OriginalPublicKey);
        }
    }
}