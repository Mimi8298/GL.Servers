namespace GL.Servers.CR.Logic.Sector.Manager
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Packets.Messages.Server.Sector;

    internal class SectorManager
    {
        internal GameMode GameMode;
        internal DateTime LastUpdate;
        internal System.Timers.Timer Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectorManager"/> class.
        /// </summary>
        public SectorManager(GameMode GameMode)
        {
            this.Timer    = new System.Timers.Timer();
            this.GameMode = GameMode;
            
            this.Timer.Interval = 500;
            this.Timer.Elapsed += (sender, args) =>
            {
                this.UpdateLogicTick(0.5f);

                if (this.GameMode.Time.SubTick >= 20)
                {
                    this.SendSectorHeartbeat();
                }

                this.LastUpdate = DateTime.UtcNow;
            };
        }

        /// <summary>
        /// Loads tilemap.
        /// </summary>
        internal void LoadTileMap(LocationData Data)
        {
            // TODO Implement SectorManager::loadTimeMap().
            this.SendSectorState();
            this.Timer.Start();
        }
        
        /// <summary>
        /// Sends a sector state.
        /// </summary>
        internal void SendSectorState()
        {
            new Sector_State_Message(this.GameMode.Device, this.GameMode).Send();
        }

        /// <summary>
        /// Sends a sector heatbeat.
        /// </summary>
        internal void SendSectorHeartbeat()
        {
            new Sector_Hearbeat_Message(this.GameMode.Device, this.GameMode.Time.SubTick / 10, this.GameMode.Checksum, new List<Command>(0)).Send();
            Console.WriteLine("Sector Heatbeat: Turn:" + this.GameMode.Time.SubTick / 10 + " Checksum:" + this.GameMode.Checksum);
        }

        /// <summary>
        /// Updates the logic tick.
        /// </summary>
        internal void UpdateLogicTick(float DeltaTime)
        {
            this.GameMode.UpdateSectorTicks(DeltaTime);
        }
    }
}