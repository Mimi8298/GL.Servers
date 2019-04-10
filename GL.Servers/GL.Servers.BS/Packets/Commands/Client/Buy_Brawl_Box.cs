namespace GL.Servers.BS.Packets.Commands.Client
{
    using System;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Commands.Server;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Buy_Brawl_Box : Command
    {
        internal bool UseGold;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buy_Brawl_Box"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Buy_Brawl_Box(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Buy_Brawl_Box.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();
            this.UseGold = this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.UseGold)
            {
                if (this.Device.Player.Resources.Get(5000001) < 100)
                {
                    Console.WriteLine("Gold");
                    return;
                }

                this.Device.Player.Resources.Minus(5000001, 100);
            }
            else
            {
                if (this.Device.Player.Diamonds < 10)
                {
                    Console.WriteLine("Diamonds");
                    return;
                }

                this.Device.Player.Diamonds -= 10;
            }
            
            new Server_Commands(this.Device, new Buy_Brawl_Box_Callback(this.Device, Reward.RandomizeBox(this.Device.Player))).Send();
        }
    }
}
 