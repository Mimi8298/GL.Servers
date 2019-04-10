namespace GL.Servers.GS.Logic.Structures
{
    using System;
    using System.Threading;

    internal static class StaticRandom
    {
        private static int Seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> Rnd = new ThreadLocal<Random>(StaticRandom.ValueFactory);

        private static Random ValueFactory()
        {
            return new Random(Interlocked.Increment(ref StaticRandom.Seed));
        }

        internal static Random Random
        {
            get
            {
                return StaticRandom.Rnd.Value;
            }
        }
    }
}