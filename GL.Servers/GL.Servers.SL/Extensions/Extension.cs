namespace GL.Servers.SL.Extensions
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Files.CSV_Helpers;

    internal static class Extension
    {
        internal static Data ReadData(this Reader Reader)
        {
            int GlobalID = Reader.ReadInt32();
            return CSV.Tables.GetWithGlobalID(GlobalID);
        }

        internal static T ReadData<T>(this Reader Reader) where T : Data
        {
            int GlobalID = Reader.ReadInt32();
            return CSV.Tables.GetWithGlobalID(GlobalID) as T;
        }
    }
}