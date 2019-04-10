namespace GL.Servers.BB.Packets
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Packets.Commands.Client;
    using GL.Servers.BB.Packets.Messages.Client;
    using GL.Servers.BB.Packets.Messages.Client.Base;
    using GL.Servers.BB.Packets.Messages.Client.Player;
    using GL.Servers.Extensions.Binary;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<short, Type> Messages  = new Dictionary<short, Type>();
        internal static readonly Dictionary<int, Type> Commands    = new Dictionary<int, Type>();
        internal static readonly Dictionary<string, Type> Debugs   = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            Factory.LoadMessages();
            Factory.LoadCommands();
            Factory.LoadDebugs();
        }

        /// <summary>
        /// Loads the game messages.
        /// </summary>
        private static void LoadMessages()
        {
            Factory.Messages.Add(10099, typeof(Crypto_Error_Message));
            Factory.Messages.Add(10100, typeof(Client_Hello_Message));
            Factory.Messages.Add(10101, typeof(Authentification_Message));
            Factory.Messages.Add(10107, typeof(Client_Capabilities_Message));
            Factory.Messages.Add(10108, typeof(Keep_Alive_Message));
            Factory.Messages.Add(10113, typeof(Set_Device_Token_Message));

            Factory.Messages.Add(14102, typeof(End_Client_Turn_Message));
        }

        /// <summary>
        /// Loads the logic commands.
        /// </summary>
        private static void LoadCommands()
        {
            Factory.Commands.Add(500, typeof(Buy_Building_Command));
            Factory.Commands.Add(595, typeof(Mission_Progress_Command));
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {

        }

        internal static Message CreateMessage(short Type, Device Device, Reader Reader)
        {
            if (Factory.Messages.TryGetValue(Type, out Type TMessage))
            {
                return (Message) Activator.CreateInstance(TMessage, Device, Reader);
            }

            return null;
        }

        internal static Command CreateCommand(int Type)
        {
            if (Factory.Commands.TryGetValue(Type, out Type TCommand))
            {
                return (Command) Activator.CreateInstance(TCommand);
            }

            return null;
        }
    }
}