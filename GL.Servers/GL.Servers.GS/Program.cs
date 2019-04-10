namespace GL.Servers.GS
{
    using System;
    using System.Drawing;
    using System.Reflection;

    using GL.Servers.Core.Consoles;

    using GL.Servers.GS.Core;
    using GL.Servers.GS.Packets;

    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.Title = $"[GGS] {Assembly.GetExecutingAssembly().GetName().Name} - Gun Developer - {DateTime.Now.Year} ©";

            Console.SetOut(new Prefixed());
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            Servers.Core.Consoles.Colorful.Console.WriteWithGradient(@"
                      ________      ___.          .__  .__       .____                       .___
                     /  _____/  ____\_ |__   ____ |  | |__| ____ |    |   _____    ____    __| _/
                    /   \  ___ /  _ \| __ \_/ __ \|  | |  |/    \|    |   \__  \  /    \  / __ | 
                    \    \_\  (  <_> ) \_\ \  ___/|  |_|  |   |  \    |___ / __ \|   |  \/ /_/ | 
                     \______  /\____/|___  /\___  >____/__|___|  /_______ (____  /___|  /\____ | 
                            \/           \/     \/             \/        \/    \/     \/      \/ 
                                                                                          V1.9.0
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

            HotKeyManager.Initialize();
        }
    }
}