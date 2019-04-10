namespace GL.Servers.CoC.Packets.Debugs
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Messages.Server.Account;

    internal class Upgrade_All_Buildings_Debug : Debug
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Upgrade_All_Buildings_Debug"/> class.
        /// </summary>
        public Upgrade_All_Buildings_Debug(params string[] Parameters) : base(Parameters)
        {
            // Upgrade_All_Buildings_Debug.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override async void Process()
        {
            if (!string.IsNullOrEmpty(this.PlayerTag))
            {
                Player Player = await Resources.Accounts.LoadPlayerAsync(this.PlayerHighID, this.PlayerLowID);

                if (Player != null)
                {
                    while (Player.Level.GameObjectManager.TownHall.UpgradeAvailable)
                    {
                        Player.Level.GameObjectManager.TownHall.StartUpgrade();
                        Player.Level.GameObjectManager.TownHall.FinishConstruction();
                    }

                    foreach (Building Building in Player.Level.GameObjectManager.GameObjects[0][0])
                    {
                        while (Building.UpgradeAvailable)
                        {
                            Building.StartUpgrade();
                            Building.FinishConstruction();
                        }
                    }

                    if (Player.Connected)
                    {
                        new Disconnected_Message(Player.Level.GameMode.Device, 0).Send();
                    }
                }
            }
        }
    }
}