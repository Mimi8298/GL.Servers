namespace GL.Servers.CR.Packets
{
    using System;
    using System.Collections.Generic;
	
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets.Messages.Client.Account;
    using GL.Servers.CR.Packets.Messages.Client.Avatar;
    using GL.Servers.CR.Packets.Messages.Client.Home;
    using GL.Servers.CR.Packets.Messages.Client.Matchmaking;
    using GL.Servers.CR.Packets.Messages.Client.Socials;
    using GL.Servers.CR.Packets.Messages.Client.Tournament;

    using GL.Servers.DataStream;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<short, Type> Messages = new Dictionary<short, Type>();
        internal static readonly Dictionary<string, Type> Debugs  = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            Factory.LoadMessages();
            Factory.LoadDebugs();
        }

        /// <summary>
        /// Loads the game messages.
        /// </summary>
        private static void LoadMessages()
        {
            Factory.Messages.Add(10100, typeof(Client_Hello_Message));
            Factory.Messages.Add(10101, typeof(Login_Message));
            Factory.Messages.Add(10107, typeof(Client_Capabilities_Message));
            Factory.Messages.Add(10108, typeof(Keep_Alive_Message));
            Factory.Messages.Add(10113, typeof(Set_Device_Token_Message));
            Factory.Messages.Add(10119, typeof(Report_User_Message));
            Factory.Messages.Add(10212, typeof(Change_Avatar_Name_Message));
            Factory.Messages.Add(10503, typeof(Ask_For_Friends_Invite_Message));
            Factory.Messages.Add(10504, typeof(Ask_For_Friends_List_Message));
            Factory.Messages.Add(10512, typeof(Ask_For_Playing_Gamecenter_Friends_Message));
            Factory.Messages.Add(10513, typeof(Ask_For_Playing_Facebook_Friends_Message));

            Factory.Messages.Add(14102, typeof(End_Client_Turn_Message));
            Factory.Messages.Add(14101, typeof(Go_Home_Message));
            Factory.Messages.Add(14107, typeof(Cancel_Matchmake_Message));
            Factory.Messages.Add(14113, typeof(Visit_Home_Message));
            
            Factory.Messages.Add(14201, typeof(Bind_Facebook_Account));
            Factory.Messages.Add(14212, typeof(Bind_Gamecenter_Account));
            Factory.Messages.Add(14262, typeof(Bind_Google_Account));

            Factory.Messages.Add(14405, typeof(Ask_For_Avatar_Stream_Message));
            Factory.Messages.Add(14406, typeof(Ask_For_Battle_Replay_Stream_Message));

            Factory.Messages.Add(14600, typeof(Avatar_Name_Check_Request_Message));

            Factory.Messages.Add(16103, typeof(Ask_For_Joinable_Tournament_Message));
        }
        
        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {
            // LoadDebugs.
        }

        /// <summary>
        /// Creates a message with the specified type.
        /// </summary>
        internal static Message CreateMessage(short Type, Device Device, ByteStream Packet)
        {
            if (Factory.Messages.TryGetValue(Type, out Type TMessage))
            {
                return (Message) Activator.CreateInstance(TMessage, Device, Packet);
            }

            Logging.Info(typeof(Factory), "Warning, message type " + Type + " does not exist.");

            return null;
        }
    }
}