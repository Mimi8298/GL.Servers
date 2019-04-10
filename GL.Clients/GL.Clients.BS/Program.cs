namespace GL.Clients.BS
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Clients.BS.Core;
    using GL.Clients.BS.Packets;
    using GL.Clients.BS.Packets.Messages.Client;

    using GL.Servers.Core.Consoles;

    internal class Program
    {
        private const int Width     = 120;
        private const int Height    = 30;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            Console.Title = $"[GBC] {Assembly.GetExecutingAssembly().GetName().Name} - Developer - {DateTime.Now.Year} ©";

            Console.SetOut(new Prefixed());

            Console.SetWindowSize(Program.Width, Program.Height);
            Console.SetBufferSize(Program.Width, Program.Height);

            Servers.Core.Consoles.Colorful.Console.WriteWithGradient(@"
                      ________      ___.          .__  .__       .____                       .___
                     /  _____/  ____\_ |__   ____ |  | |__| ____ |    |   _____    ____    __| _/
                    /   \  ___ /  _ \| __ \_/ __ \|  | |  |/    \|    |   \__  \  /    \  / __ | 
                    \    \_\  (  <_> ) \_\ \  ___/|  |_|  |   |  \    |___ / __ \|   |  \/ /_/ | 
                     \______  /\____/|___  /\___  >____/__|___|  /_______ (____  /___|  /\____ | 
                            \/           \/     \/             \/        \/    \/     \/      \/ 
                                                                                          V0.4.7
            ", Color.Yellow, Color.Fuchsia, 14);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("GobelinLand's programs are protected by our policies, available on our main website.   ");
            Console.WriteLine("GobelinLand's programs are under the 'CC Non-Commercial-NoDerivs 3.0 Unported' license.");
            Console.WriteLine("GobelinLand is NOT affiliated to 'Supercell Oy', 'Tencent', or 'Riot Games'.           ");
            Console.WriteLine("GobelinLand does NOT own 'Clash of Clans', 'Boom Beach', 'Clash Royale', or 'Hay Day'. ");
            Console.WriteLine();
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + " is now starting..." + Environment.NewLine);

            Factory.Initialize();
            Resources.Initialize();

            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            Thread.Sleep(1000);

            while (true)
            {
                var Bot = Resources.Bots.Take();

                if (Bot != null)
                {
                    Task.Run(() =>
                    {
                        int HighID  = 1;

                        Logging.Info(typeof(Program), "We are crawing the server #" + HighID + "..");

                        int Min     = (Bot.BotId - 1) * 55000;          // (1 - 1) * 55000 = 0;
                        int Max     = (Bot.BotId - 1) * 55000 + 55000;  // (1 - 1) * 55000 + 55000 = 55000

                        if (Min == 0)
                        {
                            Min     = 1;
                        }

                        for (int LowID = Min; LowID < Max; LowID++)
                        {
                            Bot.Gateway.Send(new Ask_Profile(Bot, HighID, LowID));
                            Thread.Sleep(300);
                        }

                        Logging.Info(typeof(Program), "Warning, the bot #" + Bot.BotId + " has done his job.");
                    });
                }
                else
                {
                    break;
                }
            }

            Thread.Sleep(-1);
        }
    }
}