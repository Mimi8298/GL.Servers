namespace GL.Servers.CoC.Packets.Messages.Client.Alliance
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Packets.Commands.Server;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Create_Alliance_Message : Message
    {
        internal string Name;
        internal string Description;

        internal int AllianceBadge;
        internal int RequiredScore;
        internal int WarFrequency;

        internal Hiring AllianceType;
        internal RegionData Origin;

        internal bool PublicWarLog;
        internal bool AmicalWar;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14301;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Alliance_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Create_Alliance_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Create_Alliance_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name           = this.Reader.ReadString();
            this.Description    = this.Reader.ReadString();

            this.AllianceBadge  = this.Reader.ReadInt32();
            this.AllianceType   = (Hiring) this.Reader.ReadInt32();
            this.RequiredScore  = this.Reader.ReadInt32();
            this.WarFrequency   = this.Reader.ReadInt32();

            this.Origin         = this.Reader.ReadData<RegionData>();
            
            this.PublicWarLog   = this.Reader.ReadBoolean();
            this.AmicalWar      = this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (!this.Device.GameMode.Level.Player.InAlliance)
            {
                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    if (this.Name.Length <= 16)
                    {
                        this.Name = Resources.Regex.Replace(this.Name, " ");

                        if (this.Name.StartsWith(" "))
                        {
                            this.Name = this.Name.Remove(0, 1);
                        }

                        if (this.Name.Length >= 2)
                        {
                            if (this.Description != null)
                            {
                                this.Description = Resources.Regex.Replace(this.Description, " ");

                                if (this.Description.StartsWith(" "))
                                {
                                    this.Description = this.Description.Remove(0, 1);
                                }

                                if (this.Description.Length > 128)
                                {
                                    this.Description = this.Description.Substring(0, 128);
                                }
                            }

                            AllianceBadgeLayerData Background = (AllianceBadgeLayerData) CSV.Tables.Get(Gamefile.AllianceBadgeLayer).GetDataWithInstanceID(this.AllianceBadge % 0x100);
                            AllianceBadgeLayerData Middle = (AllianceBadgeLayerData) CSV.Tables.Get(Gamefile.AllianceBadgeLayer).GetDataWithInstanceID(this.AllianceBadge     % 0x1000000 / 0x100);
                            AllianceBadgeLayerData Foreground = (AllianceBadgeLayerData) CSV.Tables.Get(Gamefile.AllianceBadgeLayer).GetDataWithInstanceID(this.AllianceBadge / 0x1000000);

                            if (Background != null)
                            {
                                if (Middle != null)
                                {
                                    if (Foreground != null)
                                    {
                                        Alliance Alliance = new Alliance();

                                        Alliance.Header.Name          = this.Name;
                                        Alliance.Description          = this.Description;
                                        // Alliance.Header.Locale        = this.Device.Player.Locale;
                                        Alliance.Header.Badge         = this.AllianceBadge;
                                        Alliance.Header.Type          = this.AllianceType;

                                        if (this.Origin != null)
                                        {
                                            Alliance.Header.Origin    = this.Origin.GlobalID;
                                        }

                                        Alliance.Header.PublicWarLog  = this.PublicWarLog;
                                        Alliance.Header.RequiredScore = this.RequiredScore;
                                        Alliance.Header.AmicalWar     = this.AmicalWar;

                                        if (Alliance.Members.Join(this.Device.GameMode.Level.Player, out Member Member))
                                        {
                                            Member.Role = Role.Leader;
                                            Resources.Clans.New(Alliance);
                                            this.Device.GameMode.CommandManager.AddCommand(new Join_Alliance_Command(Alliance.AllianceID, Alliance.Header.Name, Alliance.Header.Badge, Alliance.Header.ExpLevel, true));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}