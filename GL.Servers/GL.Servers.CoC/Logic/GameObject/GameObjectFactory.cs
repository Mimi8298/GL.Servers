namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Files.CSV_Helpers;

    internal static class GameObjectFactory
    {
        internal static GameObject CreateGameObject(Data Data, Level Level)
        {
            switch (Data.GetDataType())
            {
                case 1:
                    return new Building(Data, Level);
                case 8:
                    return new Obstacle(Data, Level);

                default:
                    return null;
            }
        }
    }
}