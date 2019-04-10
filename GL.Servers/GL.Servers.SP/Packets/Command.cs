namespace GL.Servers.SP.Packets
{
    using GL.Servers.SP.Logic;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SP.Logic.Mode;
    using Newtonsoft.Json.Linq;

    internal class Command
    {
        internal int ExecuteSubTick = -1;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal virtual int Type
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is a <see cref="ServerCommand"/>.
        /// </summary>
        internal bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(Reader Reader)
        {
            this.ExecuteSubTick = Reader.ReadInt32();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(ByteWriter Packet)
        {
            Packet.AddInt(this.ExecuteSubTick);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal virtual void Execute(GameMode GameMode)
        {
            // Execute.
        }

        /// <summary>
        /// Saves this instance from json.
        /// </summary>
        /// <returns></returns>
        internal virtual void Load(JToken Token)
        {
            // Load.
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        /// <returns></returns>
        internal virtual JObject Save()
        {
            return null;
        }

        /// <summary>
        /// Saves the base command to json.
        /// </summary>
        internal JObject SaveBase()
        {
            return new JObject
            {
                {
                    "t", this.ExecuteSubTick
                }
            };
        }
    }
}