namespace GL.Servers.CoC.Extensions.Helper
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    public static class ByteStreamHelper
    {
        internal static void AddData(this ByteWriter Writer, Data Data)
        {
            Writer.AddInt(Data != null ? Data.GlobalID : 0);
        }

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