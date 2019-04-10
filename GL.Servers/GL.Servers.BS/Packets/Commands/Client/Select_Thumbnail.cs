namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Select_Thumbnail : Command
    {
        private int ThumbnailType;
        private int ThumbnailID;

        /// <summary>
        /// Gets the global identifier.
        /// </summary>
        internal int GlobalID
        {
            get
            {
                return (this.ThumbnailType * 1000000) + this.ThumbnailID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Select_Thumbnail"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Select_Thumbnail(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Select_Thumbnail.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();

            this.ThumbnailType  = this.Reader.ReadVInt();
            this.ThumbnailID    = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Player_Thumbnails Thumbnail = CSV.Tables.GetWithGlobalID(this.GlobalID) as Player_Thumbnails;

            if (Thumbnail != null)
            {
                if (this.Device.Player.Info.Trophies >= Thumbnail.RequiredTotalTrophies)
                {
                    if (string.IsNullOrEmpty(Thumbnail.RequiredHero))
                    {
                        this.Device.Player.Info.Thumbnail = this.GlobalID;
                        this.Device.Player.Tick();
                    }
                    else
                    {
                        Characters Character = CSV.Tables.Get(Gamefile.Characters).GetData(Thumbnail.RequiredHero) as Characters;

                        if (Character != null)
                        {
                            if (this.Device.Player.Deck.ContainsKey(Character.GlobalID))
                            {
                                this.Device.Player.Info.Thumbnail = this.GlobalID;
                                this.Device.Player.Tick();
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "Error when selecting the thumbnail n°" + this.GlobalID + ", the player don't have the required hero.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Error when selecting the thumbnail n°" + this.GlobalID + ", the Characters instance was null.");
                        }
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Player don't have enough trophies to use the specified thumbnail.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error when selecting the thumbnail n°" + this.GlobalID + ", the Player_Thumbnails instance was null.");
            }
        }
    }
}