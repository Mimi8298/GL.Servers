namespace GL.Clients.CR.Packets.Messages.Client
{
    using System.Linq;

    using GL.Clients.CR.Logic;
    using GL.Clients.CR.Logic.Enums;

    using GL.Servers.Core;
    using GL.Servers.Extensions.List;
    using GL.Servers.Library;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.Sodium;

    internal class Authentification : Message
    {
        internal int HighID             = 0;
        internal int LowID              = 0;

        internal string Token           = null;
        internal string MasterHash      = "622384571aafa79a8453424fb4907c5f1e4268ce";

        internal string AdvertiseID     = "00000000-0000-0000-0000-000000000000";
        internal string OpenUDID        = "00000000-0000-0000-0000-000000000000";

        internal int Major              = 3;
        internal int Minor              = 377;
        internal int Revision           = 0;

        internal string Model           = "iPhone9,3";
        internal string OSVersion       = "10.3.1";
        internal string Region          = "fr-FR";

        internal string MACAddress      = null;

        internal bool Android           = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification(Device Device) : base(Device)
        {
            this.Identifier     = 10101;
            this.Device.State   = State.LOGIN;
            this.Version        = 3;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddRange(this.Device.SessionKey);
            this.Data.AddRange(new byte[24]);

            this.Data.AddHexa("00-00-00-02-00-00-37-EE-00-00-00-28-68-77-38-72-37-32-78-66-6B-6D-6E-67-65-72-39-6A-72-77-6E-74-77-6B-77-68-61-33-6B-73-61-73-32-79-77-72-62-63-61-66-77-6A-03-00-B9-05-00-00-00-28-36-32-32-33-38-34-35-37-31-61-61-66-61-37-39-61-38-34-35-33-34-32-34-66-62-34-39-30-37-63-35-66-31-65-34-32-36-38-63-65-00-00-00-00-00-00-00-10-34-38-31-32-30-32-31-37-30-31-30-31-36-33-32-32-FF-FF-FF-FF-00-00-00-05-4E-38-30-31-30-00-00-00-24-30-31-36-36-33-32-37-62-2D-37-35-63-36-2D-34-36-65-32-2D-62-32-36-66-2D-37-63-32-64-38-61-35-33-66-39-63-34-00-00-00-05-34-2E-34-2E-34-01-00-00-00-00-00-00-00-10-34-38-31-32-30-32-31-37-30-31-30-31-36-33-32-32-00-00-00-05-65-6E-2D-47-42-01-00-00-00-00-00-01-00-00-00-00-02-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00");
            return;

            this.Data.AddInt(this.HighID);
            this.Data.AddInt(this.LowID);

            this.Data.AddString(this.Token);

            this.Data.AddVInt(this.Major);
            this.Data.AddVInt(this.Revision);
            this.Data.AddVInt(this.Minor);

            this.Data.AddString(this.MasterHash);

            this.Data.AddString(null);

            this.Data.AddString(null); // Android ID
            this.Data.AddString(this.MACAddress);
            this.Data.AddString(this.Model);

            this.Data.AddVInt(0);

            this.Data.AddString(this.Region);
            this.Data.AddString(this.OpenUDID);
            this.Data.AddString(this.OSVersion);

            this.Data.AddBool(this.Android);

            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddBool(false);

            this.Data.AddString(null);
        }

        /// <summary>
        /// Encrypts the message, using the Sodium cryptography.
        /// </summary>
        internal override void Encrypt()
        {
            Blake2BHasher Blake2B   = new Blake2BHasher();

            Blake2B.Update(Keys.Sodium.PublicKey);
            Blake2B.Update("AC30DCBEA27E213407519BC05BE8E9D930E63F873858479946C144895FA3A26B".HexaToBytes());

            byte[] Nonce            = Blake2B.Finish();

            Crypto CryptoKeys       = new Crypto();
            CryptoKeys.SNonce       = new byte[24];
            CryptoKeys.RNonce       = new byte[24];
            CryptoKeys.PublicKey    = Keys.Sodium.PublicKey;

            Sodium Sodium           = new Sodium(CryptoKeys);

            byte[] Encrypted        = Sodium.Encrypt(this.Data.ToArray(), Nonce);
            Encrypted               = CryptoKeys.PublicKey.Concat(Encrypted).ToArray();

            this.Data.Clear();
            this.Data.AddRange(Encrypted);

            this.Length             = (uint) Encrypted.Length;
        }
    }
}