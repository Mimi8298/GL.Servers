namespace GL.Clients.CR
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    using GL.Clients.CR.Logic;
    using GL.Clients.CR.Packets;
    using GL.Servers.Core.Consoles;

    internal class Program
    {
        private static List<Client> Clients;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            Console.Title = $"[GRC] {Assembly.GetExecutingAssembly().GetName().Name} - Developer - {DateTime.Now.Year} ©";

            Console.SetOut(new Prefixed());

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(@"    ________      ___.          .__  .__       .____                       .___ ");
            Console.WriteLine(@"   /  _____/  ____\_ |__   ____ |  | |__| ____ |    |   _____    ____    __| _/ ");
            Console.WriteLine(@"  /   \  ___ /  _ \| __ \_/ __ \|  | |  |/    \|    |   \__  \  /    \  / __ |  ");
            Console.WriteLine(@"  \    \_\  (  <_> ) \_\ \  ___/|  |_|  |   |  \    |___ / __ \|   |  \/ /_/ |  ");
            Console.WriteLine(@"   \______  /\____/|___  /\___  >____/__|___|  /_______ (____  /___|  /\____ |  ");
            Console.WriteLine(@"          \/           \/     \/             \/        \/    \/     \/      \/  ");
            Console.WriteLine(@"                                                                        V1.8.1  ");

            Console.ResetColor();

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("GobelinLand's programs are protected by our policies, available on our main website.");
            Console.WriteLine("GobelinLand's programs are under the 'CC Non-Commercial-NoDerivs 3.0 Unported' license.");
            Console.WriteLine("GobelinLand is NOT affiliated to 'Supercell Oy', 'Tencent', or 'Riot Games'.");
            Console.WriteLine("GobelinLand does NOT own 'Clash of Clans', 'Boom Beach', 'Clash Royale', and 'Hay Day'.");
            Console.WriteLine();
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + " is now starting..." + Environment.NewLine);

            Console.WriteLine("-------------------------------------" + Environment.NewLine);

            Program.Clients = new List<Client>(50);

            new MessageFactory();
            new CommandFactory();

            for (int i = 0; i < 1; i++)
            {
                Program.Clients.Add(new Client());
            }

            Thread.Sleep(-1);
        }
    }
}