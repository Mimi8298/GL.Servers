namespace GL.Servers.CoC.Packets.Commands.Server
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Files.CSV_Logic.Client;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Diamonds_Added_Command : ServerCommand
    {
        internal int Count;
        internal bool AlliangeGift;

        internal BillingPackageData BillingPackageData;
        internal GemBundleData GemBundleData;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 7;
            }
        }

        internal override ChecksumEncoder Checksum
        {
            get
            {
                ChecksumEncoder Encoder = new ChecksumEncoder(true);

                Encoder.AddBoolean(this.AlliangeGift); 
                Encoder.AddInt(this.Count);
                Encoder.AddInt(this.BillingPackageData != null ? this.BillingPackageData.GlobalID : 0);
                Encoder.AddInt(this.GemBundleData      != null ? this.GemBundleData.GlobalID      : 0);
                
                return Encoder;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Diamonds_Added_Command"/> class.
        /// </summary>
        public Diamonds_Added_Command() : base()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Diamonds_Added_Command"/> class.
        /// </summary>
        public Diamonds_Added_Command(bool AlliangeGift, int Count, BillingPackageData BillingPackage = null, GemBundleData GemBundle = null) : base()
        {
            this.Count        = Count;
            this.AlliangeGift = AlliangeGift;

            this.BillingPackageData = BillingPackage;
            this.GemBundleData      = GemBundle;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.AlliangeGift       = Reader.ReadBoolean();

            this.Count              = Reader.ReadInt32();

            this.BillingPackageData = Reader.ReadData<BillingPackageData>();
            this.GemBundleData      = Reader.ReadData<GemBundleData>();

            Reader.ReadInt32();

            Reader.ReadString();

            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddBoolean(this.AlliangeGift);

            Packet.AddInt(this.Count);
            Packet.AddData(this.BillingPackageData);
            Packet.AddData(this.GemBundleData);
            Packet.AddInt(this.AlliangeGift ? 1 : 0);

            Packet.AddString(null); // TransactionID

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            Level.Player.AddDiamonds(this.Count);
        }
    }
}