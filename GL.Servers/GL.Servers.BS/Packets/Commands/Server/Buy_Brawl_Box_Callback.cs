namespace GL.Servers.BS.Packets.Commands.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.Binary;

    internal class Buy_Brawl_Box_Callback : Command
    {
        internal BrawlBox Box;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buy_Brawl_Box_Callback"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Buy_Brawl_Box_Callback(Device Device, BrawlBox Box) : base(Device)
        {
            this.Identifier = 203;
            this.Box = Box;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Buy_Brawl_Box_Callback"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Buy_Brawl_Box_Callback(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {

        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Box = new BrawlBox();
            this.Box.Decode(this.Reader);
            this.ReadHeader();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Box.Encode(this.Data);
            this.EncodeHeader();
        }

        internal override void Process()
        {
            if (this.Box.CardData != null)
            {
                if (this.Device.Player.Deck.ContainsKey(Box.CardData.GlobalID))
                {
                    this.Device.Player.Resources.Plus(Logic.Enums.Resource.Upgradium, Reward.RefundCardReward(Box.CardData));
                    return;
                }

                Card Card = new Card(this.Box.CardData.GlobalID);
                Card.Competences.Add(new Competence(this.Box.CardData.GlobalID));

                this.Device.Player.Deck.Add(Card);
            }

            if (this.Box.ResourceData != null)
            {
                this.Device.Player.Resources.Plus((Logic.Enums.Resource) this.Box.ResourceData.GetID(), this.Box.Count);
            }
        }
    }
}
 