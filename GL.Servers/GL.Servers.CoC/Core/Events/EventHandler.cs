namespace GL.Servers.CoC.Core.Events
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Messages.Server;
    using GL.Servers.Logic.Enums;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using State = GL.Servers.CoC.Logic.Mode.Enums.State;

    internal class EventsHandler
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler Handler, bool Enabled);

        private static EventHandler EHandler;
        private delegate void EventHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsHandler"/> class.
        /// </summary>
        internal static void Initialize()
        {
            EventsHandler.EHandler += EventsHandler.Process;
            EventsHandler.SetConsoleCtrlHandler(EventsHandler.EHandler, true);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal static void Process()
        {
            Console.WriteLine("- SHUTDOWN -");

            Logging.Info(typeof(EventHandler), "Server is shutting down, warning players about the maintenance...");

            Task Message = Task.Run(() =>
            {
                foreach (Player Player in Resources.Accounts.Players.Values.ToArray())
                {
                    if (Player.Connected && Player.Level.GameMode.State != State.Defend)
                    {
                        new Maintenance_Inbound_Message(Player.Level.GameMode.Device).Send();
                    }
                }
            });

            Task AccSave = Task.Run(() => Resources.Accounts.Saves());
            Task AllSave = Task.Run(() => Resources.Clans.Save());

            AccSave.Wait();
            AllSave.Wait();
            Message.Wait();
        }
    }
}