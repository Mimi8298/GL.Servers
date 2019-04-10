namespace GL.Servers.SL.Packets.Messages.Client
{
    using System.Collections.Generic;
    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;

    internal class End_Client_Turn : Message
    {
        internal int SubTick;
        internal int Checksum;
        internal int CommandCount;

        internal List<Command> Commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="End_Client_Turn"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public End_Client_Turn(Device Device, Reader Reader) : base(Device, Reader)
        {
            // End_Client_Turn.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.SubTick      = this.Reader.ReadInt32();
            this.Checksum     = this.Reader.ReadInt32();
            this.CommandCount = this.Reader.ReadInt32();

            if (this.CommandCount <= 512)
            {
                if (this.CommandCount > 0)
                {
                    this.Commands = new List<Command>(this.CommandCount);

                    for (int i = 0; i < this.CommandCount; i++)
                    {
                        Command Command = Factory.CreateCommand(this.Reader.ReadInt32(), this.Device);

                        if (Command != null)
                        {
                            Command.Decode(this.Reader);
                            this.Commands.Add(Command);
                        }
                        else this.ShowBuffer();
                    }
                }
            }
        }

        internal override void Process()
        {
            if (this.Commands != null)
            {
                while (this.Commands.Count > 0)
                {
                    Command Command = this.Commands[0];

                    if (Command.ExecuteSubTick != -1)
                    {
                        if (Command.ExecuteSubTick <= this.SubTick)
                        {
                            if (this.Device.Player.Time.ClientSubTick <= Command.ExecuteSubTick)
                            {
                                this.Device.Player.Time.ClientSubTick = Command.ExecuteSubTick;
                                this.Device.Player.Tick();

                                Command.Process();
                            }
                        }
                        else Logging.Error(this.GetType(), this.Device, "Execute command failed! Command should already executed. (type=" + Command.Type + ", server_tick");
                    }
                    else Logging.Error(this.GetType(), this.Device, "Execute command failed! subtick is not valid.");

                    this.Commands.RemoveAt(0);
                }
            }

            this.Device.Player.Time.ClientSubTick = this.SubTick;
            this.Device.Player.Tick();

            Logging.Info(this.GetType(), "Client Time : MS:" + this.Device.Player.Time.TotalMS + "  SECS:" + this.Device.Player.Time.TotalSecs + ".");
            Logging.Info(this.GetType(), "Checksum : CLIENT:" + (this.Checksum - this.Device.Player.Time.ClientSubTick) + "  SERVER:" + this.Device.Player.Checksum + ".");
        }
    }
}