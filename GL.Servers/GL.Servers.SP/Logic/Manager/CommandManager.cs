namespace GL.Servers.SP.Logic.Manager
{
    using System.Threading;
    using System.Collections.Generic;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Packets;
    using GL.Servers.SP.Logic.Mode.Enums;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class CommandManager
    {
        internal int NextServerCommandId;
        
        internal Dictionary<int, ServerCommand> ServerCommands;
        
        public CommandManager()
        {
            this.ServerCommands = new Dictionary<int, ServerCommand>(512);
        }

        internal void AddCommand(ServerCommand Command, bool OnClient = false)
        {
            if (Command.IsServerCommand && !OnClient)
            {
                this.ServerCommands.Add(Command.Id = Interlocked.Increment(ref this.NextServerCommandId), Command);
            }
        }

        internal static Command DecodeCommand(Reader Reader)
        {
            Command Command = Factory.CreateCommand(Reader.ReadInt32());

            if (Command != null)
            {
                Command.Decode(Reader);
            }

            return Command;
        }

        internal static void EncodeCommand(Command Command, ByteWriter Packet)
        {
            Packet.AddInt(Command.Type);
            Command.Encode(Packet);
        }
    }
}