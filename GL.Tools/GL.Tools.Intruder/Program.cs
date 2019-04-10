namespace GL.Tools.Intruder
{
    using System;

    using GL.Servers.Core.Consoles;

    using GL.Tools.Intruder.Core;

    internal class Program
    {
        internal static Client _Client;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.Title = "[GIT]" + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + "Developer" + " - " + DateTime.Now.Year + " ©";

            Console.WriteLine(@"

               ________      ___.          .__  .__       .____                       .___
              /  _____/  ____\_ |__   ____ |  | |__| ____ |    |   _____    ____    __| _/
             /   \  ___ /  _ \| __ \_/ __ \|  | |  |/    \|    |   \__  \  /    \  / __ |
             \    \_\  (  <_> ) \_\ \  ___/|  |_|  |   |  \    |___ / __ \|   |  \/ /_/ |
              \______  /\____/|___  /\___  >____/__|___|  /_______ (____  /___|  /\____ |
                     \/           \/     \/             \/        \/    \/     \/      \/

            ");

            Console.SetOut(new Prefixed());

            Console.WriteLine("GobelinLand's programs are protected by our policies, available on our main website.");
            Console.WriteLine("GobelinLand's programs are under the 'CC Non-Commercial-NoDerivs 3.0 Unported' license.");
            Console.WriteLine("GobelinLand is NOT affiliated to 'Supercell Oy', 'Tencent', or 'Riot Games'.");
            Console.WriteLine("GobelinLand does NOT own 'Clash of Clans', 'Boom Beach', 'Clash Royale', and 'Hay Day'.");
            Console.WriteLine();
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " is now starting...\n");

            Program._Client = new Client();

            Console.ReadKey(false);
        }
    }
}