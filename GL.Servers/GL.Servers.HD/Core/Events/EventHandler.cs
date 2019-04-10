namespace GL.Servers.HD.Core.Events
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    
    using GL.Servers.HD.Logic;
    using GL.Servers.Logic.Enums;

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

            Logging.Error(typeof(EventHandler), "Server is shutting down, warning player about the maintenance...");

            Task WTask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    if (Player.Device != null && Player.Device.State >= State.LOGGED)
                    {
                        // TODO Implement MaintenanceInboundMessage();
                        // new Maintenance_Inbound(Player.Device).Send();
                    }
                }

                Logging.Error(typeof(EventHandler), "Warned every players about the maintenance.");
            });

            bool SavePlayers = false;
            bool Faster = true;

            if (Resources.Players.Count > 0)
            {
                SavePlayers = true;
            }
            
            if (SavePlayers)
            {
                Task PTask = new Task(() => Resources.Players.Save());

                PTask.Start();
                PTask.Wait();

                Faster = false;
            }
            else
            {
                Logging.Error(typeof(EventHandler), "Server has nothing to save, shutting down immediatly.");
            }

            WTask.Wait(5000);
        }
    }
}