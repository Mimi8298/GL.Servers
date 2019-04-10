namespace GL.Servers.CR.Core.Events
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets.Messages.Server.Account;

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
            EventsHandler.EHandler += Process;
            EventsHandler.SetConsoleCtrlHandler(EventsHandler.EHandler, true);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal static void Process()
        {
            if (Program.isShuttingDown)
            {
                return;
            }

            Program.isShuttingDown = true;

            Console.WriteLine("- SHUTDOWN -");

            Logging.Info(typeof(EventHandler), "Server is shutting down, warning player about the maintenance...");

            Task WTask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    if (Player.Connected)
                    {
                        new Server_Shutdown_Message(Player.GameMode.Device).Send();
                    }
                }

                Logging.Info(typeof(EventHandler), "Warned every player about the maintenance.");
            });

            Task PTask = Task.Run(() =>
            {
                Resources.Players.Save();
            });

            Task CTask = Task.Run(() =>
            {
                Resources.Clans.Save();
            });


            PTask.Wait();
            CTask.Wait();
            WTask.Wait(5000);
            
            Environment.Exit(0);
        }
    }
}