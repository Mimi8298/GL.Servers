namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Debug_Command : Command
    {
        internal int Command;
        internal int Args;

        internal string StringArgs;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 509;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Command = Reader.ReadInt32();
            this.StringArgs = Reader.ReadString();

            Reader.ReadInt32();
            Reader.ReadInt32();

            this.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            switch (this.Command)
            {
                case 0:
                {
                    Logging.Info(this.GetType(), "Fast forward 1 hour.");                  
                    Level.FastForwardTime(3600);            
                    break;
                }

                case 1:
                {
                    Logging.Info(this.GetType(), "Fast forward 24 hours.");
                    Level.FastForwardTime(86400);
                    break;
                }

                case 9:
                {
                    Logging.Info(this.GetType(), "Fast forward 1 min.");
                    Level.FastForwardTime(60);
                    break;
                }

                case 3:
                case 31:
                {
                    Logging.Info(this.GetType(), "Add resources.");

                    if (this.Command != 31 && 50000 - Level.Player.Diamonds > 0)
                    {
                        Level.Player.Diamonds = 50000;
                        Level.Player.FreeDiamonds = 50000;
                    }

                    foreach (ResourceData Data in CSV.Tables.Get(Gamefile.Resource).Datas)
                    {
                        if (!Data.PremiumCurrency && string.IsNullOrEmpty(Data.WarRefResource))
                        {
                            Level.Player.Resources.Set(Data, Data.Name == "Dark Elixir" ? 200000 : 8000000);
                        }
                    }

                    break;
                }

                case 4:
                {
                    if (Level.Player.ExpLevel >= CSV.Tables.Get(Gamefile.ExperienceLevel).Datas.Count)
                    {
                        Logging.Info(this.GetType(), "Level cap reached.");
                        return;
                    }

                    Level.Player.ExpLevel++;
                    Level.Player.ExpPoints = 0;

                    break;
                }

                case 5:
                {
                    Level.GameObjectManager.GameObjects[0][0].ForEach(GameObject =>
                    {
                        Building Building = (Building) GameObject;

                        if (!Building.Constructing)
                        {
                            Building.StartUpgrade();
                        }

                        if (Building.Constructing)
                        {
                            Building.FinishConstruction();
                        }
                    });

                    break;
                }

                case 7:
                {
                    CSV.Tables.Get(Gamefile.Npc).Datas.ForEach(Data =>
                    {
                        if (Level.Player.NpcMapProgress.GetCountByData(Data) == 0)
                        {
                            Level.Player.NpcMapProgress.Set(Data, 1);
                        }
                    });

                    break;
                }

                case 10:
                {
                    Level.Player.Score += 5;

                    break;
                }

                case 11:
                {
                    Level.Player.Score -= 5;

                    break;
                }

                case 19:
                {
                    Level.Player.Score += 100;

                    break;
                }

                case 20:
                {
                    Level.Player.Score -= 100;

                    break;
                }
            }
        }
    }
}