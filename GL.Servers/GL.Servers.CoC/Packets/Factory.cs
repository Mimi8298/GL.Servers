namespace GL.Servers.CoC.Packets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;

    using GL.Servers.CoC.Packets.Debugs;

    using GL.Servers.CoC.Packets.Commands.Client;
    using GL.Servers.CoC.Packets.Commands.Server;
    using GL.Servers.CoC.Packets.Messages.Client.Clan;
    using GL.Servers.CoC.Packets.Messages.Client.Game;
    using GL.Servers.CoC.Packets.Messages.Client.Home;
    using GL.Servers.CoC.Packets.Messages.Client.Avatar;
    using GL.Servers.CoC.Packets.Messages.Client.Account;
    using GL.Servers.CoC.Packets.Messages.Client.Alliance;
    using GL.Servers.CoC.Packets.Messages.Client.GameCenter;
    using GL.Servers.CoC.Packets.Messages.Client.GlobalChat;
    using GL.Servers.Extensions.Binary;

    internal static class Factory
    {
        internal const string RC4Key = "fhsd6f86f67rt8fw78fw789we78r9789wer6re";

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
            Factory.Messages.Add(10100, typeof(Client_Hello_Message));
            Factory.Messages.Add(10101, typeof(Authentification_Message));
            Factory.Messages.Add(10108, typeof(Keep_Alive_Message));
            Factory.Messages.Add(10113, typeof(Set_Device_Token_Message));
            Factory.Messages.Add(10212, typeof(Change_Avatar_Name_Message));
            Factory.Messages.Add(10905, typeof(Inbox_Opened_Message));

            Factory.Messages.Add(14101, typeof(Go_Home_Message));
            Factory.Messages.Add(14102, typeof(End_Client_Turn_Message));
            Factory.Messages.Add(14134, typeof(Attack_Npc_Message));
            Factory.Messages.Add(14212, typeof(Bind_GameCenter_Account_Message));
            Factory.Messages.Add(14301, typeof(Create_Alliance_Message));
            Factory.Messages.Add(14302, typeof(Ask_For_Alliance_Data_Message));
            Factory.Messages.Add(14303, typeof(Ask_For_Joinable_Alliance_List_Message));
            Factory.Messages.Add(14315, typeof(Chat_To_Alliance_Stream_Message));
            Factory.Messages.Add(14316, typeof(Change_Alliance_Settings_Message));
            Factory.Messages.Add(14325, typeof(Ask_For_Avatar_Profile_Message));
            Factory.Messages.Add(14405, typeof(Ask_For_Avatar_Stream_Message));

            Factory.Messages.Add(14510, typeof(Battle_End_Client_Turn_Message));

            Factory.Messages.Add(14715, typeof(Send_Global_Chat_Line_Message));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {
            Factory.Commands.Add(1, typeof(Join_Alliance_Command));
            Factory.Commands.Add(2, typeof(Leave_Alliance_Command));
            Factory.Commands.Add(3, typeof(Change_Avatar_Name_Command));
            Factory.Commands.Add(6, typeof(Alliance_Settings_Changed_Command));
            Factory.Commands.Add(7, typeof(Diamonds_Added_Command));

            Factory.Commands.Add(500, typeof(Buy_Building_Command));
            Factory.Commands.Add(501, typeof(Move_GameObject_Command));
            Factory.Commands.Add(502, typeof(Upgrade_Building_Command));
            Factory.Commands.Add(504, typeof(Speed_Up_Construction_Command));
            //Factory.Commands.Add(505, typeof(Cancel_Construction_Command));
            Factory.Commands.Add(506, typeof(Collect_Resources_Command));
            //Factory.Commands.Add(507, typeof(Remove_Obstacle_Command));
            Factory.Commands.Add(508, typeof(Train_Unit_Command));
            Factory.Commands.Add(509, typeof(Cancel_Unit_Production_Command));
            Factory.Commands.Add(518, typeof(Buy_Resources_Command));
            Factory.Commands.Add(519, typeof(Mission_Progress_Command));
            Factory.Commands.Add(520, typeof(Unlock_Building_Command));
            Factory.Commands.Add(523, typeof(Claim_Achievement_Reward_Command));
            Factory.Commands.Add(532, typeof(New_Shop_Seen_Command));
            Factory.Commands.Add(537, typeof(Send_Alliance_Mail_Command));
            Factory.Commands.Add(539, typeof(New_Seen_Command));
            Factory.Commands.Add(577, typeof(Swap_GameObject_Command));
            //Factory.Commands.Add(600, typeof(Gear_Up_Command));

            Factory.Commands.Add(700, typeof(Place_Attacker_Command));

            Factory.Commands.Add(800, typeof(Matchmaking_Command));
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {
            Factory.Debugs.Add("clearobstacles", typeof(Clear_Obstacles_Debug));
            Factory.Debugs.Add("fastforward", typeof(Fast_Forward_Debug));
            Factory.Debugs.Add("addunits", typeof(Add_Units_Debug));
            Factory.Debugs.Add("adddiamonds", typeof(Add_Diamonds_Debug));
            Factory.Debugs.Add("upgradeallbuildings", typeof(Upgrade_All_Buildings_Debug));
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
        
        internal static Debug CreateDebug(string Message, Player Player)
        {
            string[] Parameters = Message.Remove(0, 1).Split(' ');

            if (Factory.Debugs.ContainsKey(Parameters[0]))
            {
                Debug Debug = (Debug) Activator.CreateInstance(Factory.Debugs[Parameters[0]], Parameters.Skip(1).ToArray());

                if (Player.Rank >= Debug.RequiredRank)
                {
                    return Debug;
                }
            }

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
