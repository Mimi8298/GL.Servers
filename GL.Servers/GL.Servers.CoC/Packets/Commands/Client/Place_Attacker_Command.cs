namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Items;
    using GL.Servers.CoC.Logic.Mode.Enums;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    using Newtonsoft.Json.Linq;

    internal class Place_Attacker_Command : Command
    {
        internal int X;
        internal int Y;

        internal CharacterData Character;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 700;
            }
        }

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.X = Reader.ReadInt32();
            this.Y = Reader.ReadInt32();
            this.Character = Reader.ReadData<CharacterData>();

            base.Decode(Reader);
        }

        internal override void Execute(Level Level)
        {
            if (this.Character != null)
            {
                Item Unit = Level.Player.Units.GetByData(this.Character);

                if (Unit != null)
                {
                    if (Unit.Count > 0)
                    {
                        if (Level.GameMode.State == State.Attack)
                        {
                            /*if (!this.Device.Player.BattleManager.Stopped)
                            {
                                Unit.Count--;

                                if (this.Device.Player.BattleManager.Started)
                                {
                                    this.Device.Player.BattleManager.EndAttackPreparation();
                                }

                                this.Device.Player.BattleManager.Replay.RecordCommand(this);
                                this.PlaceAttacker();
                            }*/
                        }
                        else
                        {
                            Unit.Count--;
                        }
                    }
                }
            }
        }

        internal void PlaceAttacker(Level Level)
        {
            if (Level.GameMode.State == State.Attack)
            {
                // Level.Player.BattleManager.BattleLog.IncrementDeployedAttackerUnits(this.Character);
            }
        }

        internal override JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("base", this.SaveBase());
            Json.Add("x", this.X);
            Json.Add("y", this.Y);
            Json.Add("d", this.Character.GlobalID);

            return Json;
        }
    }
}