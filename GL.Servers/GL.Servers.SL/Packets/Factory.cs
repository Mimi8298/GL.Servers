namespace GL.Servers.SL.Packets
{
    using GL.Servers.SL.Packets.Messages.Client;
    using System;
    using System.Collections.Generic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Packets.Commands.Client;

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
            Factory.Messages.Add(10101, typeof(Authentification));
            Factory.Messages.Add(10108, typeof(Keep_Alive));
            Factory.Messages.Add(10113, typeof(Set_Device_Token));

            Factory.Messages.Add(14102, typeof(End_Client_Turn));
            Factory.Messages.Add(14212, typeof(Bind_GameCenter_Account));
            Factory.Messages.Add(14405, typeof(Ask_For_Avatar_Stream_Data));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {
            Factory.Commands.Add(500, typeof(Start_Quest_Command));
            Factory.Commands.Add(501, typeof(Buy_Hero_Command));
            Factory.Commands.Add(502, typeof(Upgrade_Hero_Command));
            Factory.Commands.Add(513, typeof(Buy_Taunt_Command));
            Factory.Commands.Add(524, typeof(Buy_Energy_Package_Command));
            Factory.Commands.Add(600, typeof(Move_Character_Command));
            Factory.Commands.Add(800, typeof(Action_Seen_Command));
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {

        }

        internal static Command CreateCommand(int Type, Device Device)
        {
            if (Factory.Commands.TryGetValue(Type, out Type CType))
            {
                return (Command) Activator.CreateInstance(CType, Device);
            }
            
            Logging.Info(typeof(Factory), "Command " + Type + " not exist.");

            return null;
        }

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