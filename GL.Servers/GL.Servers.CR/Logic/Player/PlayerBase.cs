﻿namespace GL.Servers.CR.Logic
{
    public class PlayerBase
    {
        /// <summary>
        /// Gets the checksum of this instance.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return 0;
            }
        }

        internal virtual bool IsNpcPlayer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBase"/> class.
        /// </summary>
        public PlayerBase()
        {
            // PlayerBase.
        }
    }
}