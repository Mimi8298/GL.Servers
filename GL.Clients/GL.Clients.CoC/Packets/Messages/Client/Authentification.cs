namespace GL.Clients.CoC.Packets.Messages.Client
{
    using System.Linq;

    using GL.Clients.CoC.Logic;
    using GL.Clients.CoC.Logic.Enums;
    using GL.Servers.Core;
    using GL.Servers.Extensions.List;
    using GL.Servers.Library;
    using GL.Servers.Library.Blake2B;
    using GL.Servers.Library.Sodium;

    internal class Authentification : Message
    {
        // 7eb15f65bdd576619abbd0b0650c45db1020f5ec969fcf48a828424f1bc8d809

        // internal int HighID             = 1;
        // internal int LowID              = 2304;

        internal int HighID             = 0;
        internal int LowID              = 0;

        internal string Token           = null; // "erzkw78p83t73sbm6c4ms2ww8894fynmzc3xyntf";
        internal string MasterHash      = null; // "fe24585723b95a43a7b98b60f69ab2807013a1b5";

        internal string AdvertiseID     = "00000000-0000-0000-0000-000000000000";
        internal string OpenUDID        = "00000000-0000-0000-0000-000000000000";

        internal int Major              = 9;
        internal int Minor              = 105;
        internal int Revision           = 9;

        internal string Model           = "iPhone9,3";
        internal string OSVersion       = "10.3.1";
        internal string Region          = "fr-FR";

        internal string MACAddress      = null;

        internal bool Android           = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification(Device Device) : base(Device)
        {
            this.Identifier     = 10101;
            this.Device.State   = State.LOGIN;
            this.Version        = 8;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddRange(this.Device.SessionKey);
            this.Data.AddRange(this.Device.SessionKey);

            this.Data.AddInt(this.HighID);
            this.Data.AddInt(this.LowID);

            this.Data.AddString(this.Token);

            this.Data.AddRange("0000000900000000000000690000002831666637393563666136623338333665623966376638363962323530396662626136303238636134000000000000001034633363346630623835346630623361ffffffff000000074c472d48383530001e84840000000569742d49540000002431636261356432312d336139352d343830322d623035392d32353436303331303333343800000003372e30010000000000000010346333633466306238353466306233610000002465633066336232362d666161392d346534312d383361342d636330633939376666386462010000000010ee02f51d000000000000000000000008392e3130352e3130000000000000000000".HexaToBytes());

            return;

            this.Data.AddInt(this.Major);
            this.Data.AddInt(this.Revision);
            this.Data.AddInt(this.Minor);

            this.Data.AddString(this.MasterHash);

            this.Data.AddString(null);

            this.Data.AddString(null); // Android ID
            this.Data.AddString(this.MACAddress);
            this.Data.AddString(this.Model);

            this.Data.AddInt(2000001);

            this.Data.AddString(this.Region);
            this.Data.AddString(this.OpenUDID);
            this.Data.AddString(this.OSVersion);

            this.Data.AddBool(this.Android);

            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddBool(false);

            this.Data.AddString(null);

            this.Data.AddInt(0); // Seed

            this.Data.Add(0x00);

            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddString("9.105.9");

            /* this.Data.AddInt(this.HighID);
            this.Data.AddInt(this.LowID);

            this.Data.AddString(this.Token);

            this.Data.AddInt(this.Major);
            this.Data.AddInt(this.Revision);
            this.Data.AddInt(this.Minor);

            this.Data.AddString(this.MasterHash);

            this.Data.AddInt(0);

            this.Data.AddString(this.OpenUDID);
            this.Data.AddString(this.MACAddress);
            this.Data.AddString(this.Model);

            this.Data.AddBool(this.Android);

            this.Data.AddString(this.Region);
            this.Data.AddString(this.AdvertiseID);
            this.Data.AddString(this.OSVersion);

            this.Data.AddHexa("00-00-00-00-00-00-00-00-00-00-00-00-00-01-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00"); */
        }

        /// <summary>
        /// Encrypts the message, using the Sodium cryptography.
        /// </summary>
        internal override void Encrypt()
        {
            KeyPairGL KeyPair       = new KeyPairGL(Keys.Sodium.PublicKey, Keys.Sodium.PrivateKey);
            Blake2BHasher Blake2B   = new Blake2BHasher();

            Blake2B.Update(KeyPair.PublicKey);
            Blake2B.Update("7eb15f65bdd576619abbd0b0650c45db1020f5ec969fcf48a828424f1bc8d809".HexaToBytes());

            byte[] Nonce            = Blake2B.Finish();

            Crypto CryptoKeys       = new Crypto
            {
                PublicKey           = "7eb15f65bdd576619abbd0b0650c45db1020f5ec969fcf48a828424f1bc8d809".HexaToBytes()
            };

            CryptoKeys.Sodium       = new Sodium(CryptoKeys);

            byte[] Encrypted        = CryptoKeys.Sodium.Encrypt(this.Data.ToArray(), Nonce);
            Encrypted               = KeyPair.PublicKey.Concat(Encrypted).ToArray();

            this.Data.Clear();
            this.Data.AddRange(Encrypted);

            this.Length             = (uint) this.Data.Count;
        }
    }
}