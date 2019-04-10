namespace GL.Servers.CoC.Packets
{
    using System;
    
    using GL.Servers.Extensions;
    using GL.Servers.Logic.Enums;

    internal class Debug : IDisposable
    {
        /// <summary>
        /// Gets or sets the player tag.
        /// </summary>
        internal string PlayerTag;
        
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        internal string[] Parameters;

        /// <summary>
        /// Gets or sets the parameter offset.
        /// </summary>
        internal int ParameterOffset;

        /// <summary>
        /// Gets the next parameter.
        /// </summary>
        internal string NextParameter
        {
            get
            {
                if (this.ParameterOffset >= this.Parameters.Length)
                {
                    return this.Parameters[this.ParameterOffset++];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the player high identifier.
        /// </summary>
        internal int PlayerHighID
        {
            get
            {
                return (int) (Tags.ToId(this.PlayerTag) >> 32);
            }
        }

        /// <summary>
        /// Gets the player low identifier.
        /// </summary>
        internal int PlayerLowID
        {
            get
            {
                return (int) Tags.ToId(this.PlayerTag);
            }
        }

        /// <summary>
        /// Gets or sets the required rank.
        /// </summary>
        internal virtual Rank RequiredRank
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Debug"/> class.
        /// </summary>
        internal Debug(params string[] Parameters)
        {
            this.Parameters = Parameters;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode()
        {
            this.PlayerTag = this.NextParameter;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Process.
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la
        /// libération ou à la redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
            this.Parameters = null;
        }
    }
}