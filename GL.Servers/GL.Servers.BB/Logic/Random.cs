namespace GL.Servers.BB.Logic
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

        internal int Rand()
        {
            int Seed = this.Seed;

            if (Seed == 0)
                Seed = -1;

            int v2 = Seed ^ (Seed << 13) ^ ((Seed ^ (Seed << 13)) >> 17);
            return 32 * v2 ^ 32;
        }

        internal int Rand(int Max)
        {
            if (Max > 0)
            {
                this.Seed = this.Rand();

                if (this.Seed >= 0)
                    return this.Seed % Max;
                return -this.Seed % Max;
            }

            return 0;
        }
    }
}