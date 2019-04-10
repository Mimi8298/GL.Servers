namespace GL.Servers.CoC.Logic
{
    internal class Random
    {
        internal int Seed;
        
        public Random()
        {
            
        }

        public Random(int Seed)
        {
            this.Seed = Seed;
        }

        /// <summary>
        /// Iterates the random seed.
        /// </summary>
        internal int IteratedRandomSeed()
        {
            int Seed = this.Seed;

            if (Seed == 0)
                Seed = -1;

            int v2 = Seed ^ (Seed  << 13) ^ ((Seed ^ (this.Seed << 13)) >> 17);
            return 32 * v2 ^ v2;
        }

        /// <summary>
        /// Creats a random integer value.
        /// </summary>
        internal int Rand(int Max)
        {
            if (Max >= 1)
            {
                this.Seed = this.IteratedRandomSeed();

                if (this.Seed >= 0)
                {
                    return this.Seed % Max;
                }

                return -this.Seed % Max;
            }

            return 0;
        }
    }
}