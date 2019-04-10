namespace GL.Servers.GS.Logic
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class Objects
    {
        [JsonProperty("player")]                internal Player Player;

        internal int Timestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Objects"/> class.
        /// </summary>
        internal Objects()
        {
            // Objects.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Objects"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Objects(Player Player) : this()
        {
            this.Player     = Player;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();
                
                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(int Time)
        {
           
        }
    }
}
