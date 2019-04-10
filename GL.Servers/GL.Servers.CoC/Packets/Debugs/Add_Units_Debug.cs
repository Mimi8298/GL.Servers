namespace GL.Servers.CoC.Packets.Debugs
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    
    using GL.Servers.Logic.Enums;

    internal class Add_Units_Debug : Debug
    {
        /// <summary>
        /// Gets a value indicating the required rank.
        /// </summary>
        internal override Rank RequiredRank
        {
            get
            {
                return Rank.Moderator;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add_Units_Debug"/> class.
        /// </summary>
        public Add_Units_Debug(params string[] Parameters) : base(Parameters)
        {
            
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
                    foreach (CharacterData Data in CSV.Tables.Get(Gamefile.Character).Datas)
                    {
                        if (!Data.DisableProduction)
                        {
                            if (Data.UnitOfType == 0)
                            {
                                Player.Units.Add(Data, 500);
                            }
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