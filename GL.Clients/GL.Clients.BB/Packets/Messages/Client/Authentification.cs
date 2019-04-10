namespace GL.Clients.BB.Packets.Messages.Client
{
    using GL.Clients.BB.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Authentification : Message
    {
        // Public Key : f6e07c02b728dfee9a849204b2ac1a3004ae3ce191092ae9ae5188db41768528
        // New Crypto Obfuscated.

        internal int HighID             = 1;
        internal int LowID              = 2304;

        internal string Token           = "erzkw78p83t73sbm6c4ms2ww8894fynmzc3xyntf"; // "erzkw78p83t73sbm6c4ms2ww8894fynmzc3xyntf";
        internal string MasterHash      = "ab63c47720ad8052e95a6302825eaaf7df7ad3cc"; // "ab63c47720ad8052e95a6302825eaaf7df7ad3cc";

        internal string AdvertiseID     = "00000000-0000-0000-0000-000000000000";
        internal string OpenUDID        = "00000000-0000-0000-0000-000000000000";

        internal int Major              = 4;
        internal int Minor              = 7;
        internal int Revision           = 0;

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
            this.Version        = 8;
            this.Device.State   = State.LOGIN;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.HighID);
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

            this.Data.AddHexa("00-00-00-00 00-00-00-00 00-00-00-00 00-00 00-00-00-24  32-34-36-45-42-34-36-33-2D-45-37-31-43-2D-34-33-33-39-2D-42-32-34-35-2D-32-39-46-45-45-35-46-31-35-41-44-45 FF-FF-FF-FF 00-00-00-00 00-00-00-00 00-00-00-00 00-00-00-00 00-00-00-00 00-00");
        }
    }
}