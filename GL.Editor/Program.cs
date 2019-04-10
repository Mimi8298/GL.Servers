namespace GL.Editor
{
    using System;
    using System.Windows.Forms;

    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface.Interface());
        }
    }
}