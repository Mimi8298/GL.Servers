namespace GL.Servers.BS.Logic
{
    using System.Collections.Generic;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Randomizer
    {
        internal int Seed;

        internal int Rand(int Max)
        {
            if (Max > 0)
            {
                if (this.Seed == 0)
                {
                    this.Seed = -1;
                }

                int v4      = this.Seed ^ (this.Seed << 13) ^ ((this.Seed ^ (this.Seed << 13)) >> 17);
                int v5      = v4 ^ 32 * v4;
                this.Seed   = v5;
                int rand    = v5 <= -1 ? -v5 : v5;
                
                return rand % Max;
            }

            return 0;
        }

        internal void Decode(Reader Reader)
        {
            this.Seed = Reader.ReadVInt();
        }

        internal void Encode(List<byte> Data)
        {
            Data.AddVInt(this.Seed);
        }
    }
}