namespace GL.Servers.SP.Packets
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SP.Packets.Commands.Debug;
    using GL.Servers.SP.Packets.Messages.Client.Account;
    using GL.Servers.SP.Packets.Messages.Client.Home;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<short, Type> Messages = new Dictionary<short, Type>();
        internal static readonly Dictionary<int, Type> Commands   = new Dictionary<int, Type>();
        internal static readonly Dictionary<string, Type> Debugs  = new Dictionary<string, Type>();

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
            Factory.Messages.Add(10101, typeof(Authentification_Message));
            Factory.Messages.Add(10108, typeof(Keep_Alive_Message));
            Factory.Messages.Add(10113, typeof(Set_Device_Token_Message));

            Factory.Messages.Add(14102, typeof(End_Client_Turn_Message));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {
            Factory.Commands.Add(1000, typeof(Debug_Command));
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {

        }

        internal static Command CreateCommand(int Type)
        {
            if (Factory.Commands.TryGetValue(Type, out Type CType))
            {
                return (Command) Activator.CreateInstance(CType);
            }
            
            Logging.Info(typeof(Factory), "Command " + Type + " not exist.");

            return null;
        }

        /*
        internal static Debug CreateDebug(string Message, Device Device)
        {
            string[] Parameters = Message.Remove(0, 1).Split(' ');

            if (Factory.Debugs.ContainsKey(Parameters[0]))
            {
                Debug Debug = (Debug) Activator.CreateInstance(Factory.Debugs[Parameters[0]], Device, Parameters.Skip(1).ToArray());

                if (Device.Player.Rank >= Debug.RequiredRank)
                {
                    return Debug;
                }
            }

            return null;
        }
        */

        internal static Message CreateMessage(short Type, Device Device, Reader Reader)
        {
            if (Factory.Messages.TryGetValue(Type, out Type MType))
            {
                return (Message) Activator.CreateInstance(MType, Device, Reader);
            }

            Logging.Info(typeof(Factory), "Can't handle the following message : ID " + Type + ".");

            return null;
        }
    }
}