namespace GL.Servers.BB.Files
{
    using System.IO;
    using System.Text;

    internal class Update
    {
        internal static string Json;

        internal static void Initialize()
        {
            Update.Json = File.ReadAllText("Gamefiles/update.json", Encoding.UTF8);
        }
    }
}
