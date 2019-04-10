namespace GL.Servers.SP.Core.Events
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Runtime.InteropServices;
    
    using GL.Servers.SP.Logic;

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

            Task WTask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    if (Player.Connected)
                    {
                        // new Maintenance_Inbound_Message(Player.Level.GameMode.Device).Send();
                    }
                }

                Logging.Info(typeof(EventHandler), "Warned every players about the maintenance.");
            });

            bool SavePlayers = false;
            bool SaveClans = false;
            bool Faster = true;

            if (Resources.Players.Count > 0)
            {
                SavePlayers = true;
            }

            if (Resources.Clans.Count > 0)
            {
                SaveClans = true;
            }

            if (SavePlayers || SaveClans)
            {
                Task PTask = new Task(() => Resources.Players.Save());
                Task CTask = new Task(() => Resources.Clans.Save());

                if (SavePlayers)
                    PTask.Start();
                if (SaveClans)
                    CTask.Start();

                if (SavePlayers)
                    PTask.Wait();

                if (SaveClans)
                    CTask.Wait();

                Faster = false;
            }
            else
            {
                Logging.Error(typeof(EventHandler), "Server has nothing to save, shutting down immediatly.");
            }

            WTask.Wait(5000);

            // Thread.Sleep(5000 - (Faster ? 3000 : 0));

            Environment.Exit(0);
        }
    }
}