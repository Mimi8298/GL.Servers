namespace GL.Servers.CoC.Logic.Attack
{
    internal class NpcAttack
    {
        internal Level Level;
        internal NpcPlayer NpcPlayer;

        internal int NextAttack;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcAttack"/> class.
        /// </summary>
        public NpcAttack(Level Level)
        {
            this.Level = Level;
            this.NpcPlayer = Level.NpcPlayer;
        }

        /// <summary>
        /// Places one unit.
        /// </summary>
        internal void PlaceOneUnit()
        {
            // TODO Implement NpcAttack::placeOneUnit().
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.NpcPlayer.Units.Count > 0)
            {
                this.NextAttack -= 64;

                if (this.NextAttack <= 0)
                {
                    this.PlaceOneUnit();
                    this.NextAttack = 200;
                }
            }
        }
    }
}