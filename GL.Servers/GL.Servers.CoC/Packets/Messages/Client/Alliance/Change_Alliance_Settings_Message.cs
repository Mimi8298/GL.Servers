namespace GL.Servers.CoC.Packets.Messages.Client.Alliance
{
    using System.Linq;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Packets.Commands.Server;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Change_Alliance_Settings_Message : Message
    {
        internal string Description;

        internal int AllianceBadge;
        internal int RequiredScore;
        internal int WarFrequency;

        internal bool PublicWarLog;
        internal bool AmicalWar;

        internal Hiring AllianceType;
        internal RegionData Origin;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14316;
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
        /// Initializes a new instance of the <see cref="Change_Alliance_Settings_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Change_Alliance_Settings_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Change_Alliance_Settings_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Description = this.Reader.ReadString();
            this.Reader.ReadString();

            this.AllianceBadge = this.Reader.ReadInt32();
            this.RequiredScore = this.Reader.ReadInt32();
            this.WarFrequency  = this.Reader.ReadInt32();

            this.Origin = this.Reader.ReadData<RegionData>();

            this.PublicWarLog = this.Reader.ReadBoolean();
            this.AmicalWar    = this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (!this.Device.GameMode.Level.Player.InAlliance)
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
                        
                            Alliance.Description          = this.Description;
                            Alliance.Header.Badge         = this.AllianceBadge;
                            Alliance.Header.Type          = this.AllianceType;
                            Alliance.Header.Origin        = this.Origin?.GlobalID ?? 0;
                            Alliance.Header.PublicWarLog  = this.PublicWarLog;
                            Alliance.Header.RequiredScore = this.RequiredScore;
                            Alliance.Header.AmicalWar     = this.AmicalWar;

                            foreach (Player Player in Alliance.Members.Connected.Values.ToArray())
                            {
                                if (Player.Connected)
                                {
                                    Player.Level.GameMode.CommandManager.AddCommand(new Alliance_Settings_Changed_Command(Alliance.AllianceID, Alliance.Header.Badge));
                                }
                                else
                                    Alliance.Members.Connected.TryRemove(Player.PlayerID, out _);
                            }
                        }
                    }
                }
            }
        }
    }
}