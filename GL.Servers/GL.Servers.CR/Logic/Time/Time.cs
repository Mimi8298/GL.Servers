namespace GL.Servers.CR.Logic
{
    using GL.Servers.DataStream;

    internal struct Time
    {
        internal float Dust;
        internal int SubTick;

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(float DeltaTime)
        {
            int Ticks = (int) (20 * DeltaTime);

            this.SubTick += Ticks;
            this.Dust    += DeltaTime - Ticks / 20f;

            if (this.Dust / 20 > 0)
            {
                Ticks = (int) (20 * this.Dust);

                this.SubTick += Ticks;
                this.Dust    -= Ticks / 20f;
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Packet)
        {
            Packet.AddVInt(this.SubTick);
        }

        /// <summary>
        /// Returns the seconds converted in ticks.
        /// </summary>
        internal static int GetSecondsInTicks(int Seconds)
        {
            return 20 * Seconds;
        }
    }
}