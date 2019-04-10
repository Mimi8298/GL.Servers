namespace GL.Servers.SP.Logic
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
        
        internal int Rand(int Max)
        {
            if (Max >= 1)
            {
                int Seed = this.Seed;
                if (Seed == 0)
                    Seed = -1;
                int v4 = Seed ^ (Seed << 13) ^ ((Seed ^ (Seed << 13)) >> 17);

                this.Seed = v4 ^ 32 * v4;

                if (this.Seed <= -1)
                    return -this.Seed - -this.Seed / Max * Max;
                return this.Seed - this.Seed / Max * Max;
            }

            return 0;
        }
    }
}