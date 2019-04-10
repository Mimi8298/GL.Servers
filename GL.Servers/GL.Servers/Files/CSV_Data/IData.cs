namespace GL.Servers.Files.CSV_Data
{
    public interface IData
    {
        int GlobalID
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }
    }
}