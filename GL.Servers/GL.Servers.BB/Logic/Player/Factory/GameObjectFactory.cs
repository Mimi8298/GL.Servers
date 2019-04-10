namespace GL.Servers.BB.Logic.Factory
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Logic.GameObject;

    internal class GameObjectFactory
    {
        internal static GameObject CreateGameObject(Data Data, Home Home)
        {
            switch (Data.GetDataType())
            {
                case 0:
                    return new Building(Data, Home);
                case 3:
                    return null; // TODO Implement Character GameObject.
                case 5:
                    return null; // TODO Implement Projectile GameObject.
                case 7:
                    return new Obstacle(Data, Home);

                case 11:
                    return null; // TODO Implement Trap GameObject.
                case 17:
                    return null; // TODO Implement Deco GameObject.
                case 25:
                    return null; // TODO Implement Spell GameObject.
                case 27:
                    return null; // TODO Implement LandingShip GameObject.

                case 34:
                    return null; // TODO Implement ResourceShip GameObject.
                case 35:
                    return null; // TODO Implement LootBox GameObject.
                case 41:
                    return null; // TODO Implement Footstep GameObject.

                default:
                    return null;
            }
        }
    }
}