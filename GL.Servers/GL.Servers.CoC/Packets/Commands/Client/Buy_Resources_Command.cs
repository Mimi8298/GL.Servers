namespace GL.Servers.CoC.Packets.Commands.Client
{
    using System;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Buy_Resources_Command : Command
    {
        internal int Count;

        internal Command Command;
        internal ResourceData Data;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 518;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Count = Reader.ReadInt32();
            this.Data  = Reader.ReadData<ResourceData>();

            Reader.ReadInt32();

            if (Reader.ReadBoolean())
            {
                this.Command = CommandManager.DecodeCommand(Reader);
            }

            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.Data != null)
            {
                if (!this.Data.PremiumCurrency)
                {
                    // if (string.IsNullOrEmpty(this.Data.WarRefResource))
                    {
                        int Cost = GamePlayUtil.GetResourceCost(this.Data, this.Count, this.Data.VillageType);
                        
                        if (Level.Player.HasEnoughDiamonds(this.Count))
                        {
                            if (Level.Player.GetAvailableResourceStorage(this.Data) >= this.Count)
                            {
                                Level.Player.UseDiamonds(this.Count);
                                Level.Player.Resources.Add(this.Data, this.Count);

                                if (this.Command != null)
                                {
                                    if (this.Command.ExecuteSubTick == -1)
                                    {
                                        this.Command.ExecuteSubTick = this.ExecuteSubTick;

                                        try
                                        {
                                            if (!this.Command.IsServerCommand)
                                            {
                                                this.Command.Execute(Level);
                                            }
                                        }
                                        catch (Exception Exception)
                                        {
                                            Logging.Error(this.GetType(), "An error has been throwed when the process of command type " + this.Command.Type + ". " + Exception);
                                        }
                                    }
                                }
                            }
                        }
#if DEBUG
                        else
                            Logging.Error(this.GetType(), "Unable to buy resources. You don't have enough diamond.");
#endif
                    }
                }
#if DEBUG
                else
                    Logging.Error(this.GetType(), "Unable to buy resources. Premium resources is not buyable.");
#endif
            }
#if DEBUG
            else
                Logging.Error(this.GetType(), "Unable to buy resources. Data is null or invalid.");
#endif
        }
    }
}