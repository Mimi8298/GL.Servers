namespace GL.Servers.CR.Logic.Mode
{
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Utils;
    using GL.Servers.CR.Logic.Commands.Manager;
    using GL.Servers.CR.Logic.Mode.Enums;
    using GL.Servers.CR.Logic.Sector.Manager;
	
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.List;

    using Random = GL.Servers.CR.Logic.Random;

    internal class GameMode
    {
        internal Time Time;
        internal State State;

        internal Device Device;
        internal SectorManager SectorManager;
        internal CommandManager CommandManager;

        private Battle _Battle;
        private Player _Player;
        private Random _Random;
        private ChecksumEncoder _ChecksumEncoder;

        /// <summary>
        /// Gets the player.
        /// </summary>
        internal Battle Battle
        {
            get
            {
                return this._Battle;
            }
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        internal Player Player
        {
            get
            {
                return this._Player;
            }
        }

        /// <summary>
        /// Gets the home.
        /// </summary>
        internal Home Home
        {
            get
            {
                return this._Player?.Home;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the device is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.Device != null)
                {
                    return this.Device.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                if (this.Home != null)
                {
                    return this.Home.Checksum;
                }
                
                this._ChecksumEncoder.ResetChecksum();

                this.Encode(this._ChecksumEncoder);

                return this._ChecksumEncoder.Checksum;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMode"/> class.
        /// </summary>
        internal GameMode(Device Device)
        {
            this.Device  = Device;
            this._Random = new Random();
            this.SectorManager = new SectorManager(this);
            this.CommandManager = new CommandManager(this);
            this._ChecksumEncoder = new ChecksumEncoder(null);
        }

        /// <summary>
        /// Adds the player to battle.
        /// </summary>
        internal void AddPlayer(Player Player)
        {
            this._Battle.AddPlayer(Player);
        }

        /// <summary>
        /// Clears the client ticks.
        /// </summary>
        internal void ClearClientTicks()
        {
            if (this.Time.SubTick > 0)
            {
                this.FastForwardTime(this.Time.SubTick / 20 + (this.Time.SubTick % 20 > 0 ? 1 : 0));
                this.Time = new Time();
            }
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            if (Seconds > 0)
            {
                if (this.Home != null)
                {
                    this.Home.FastForward(Seconds);
                }
            }
        }

        /// <summary>
        /// Gets a random int.
        /// </summary>
        internal int GetRandomInt(int Max)
        {
            return this._Random.Rand(Max);
        }

        /// <summary>
        /// Loads home state.
        /// </summary>
        internal void LoadHomeState(Player Player, int SecondsSinceLastSave, int RandomSeed)
        {
            this.State = State.Home;

            this.ClearClientTicks();
            this.SetPlayer(Player);
            this.FastForwardTime(SecondsSinceLastSave);
            this.SetRandomSeed(RandomSeed);
            this.Home.LoadingFinished();
        }

        /// <summary>
        /// Loads home state.
        /// </summary>
        internal void LoadAttackState(Player Player1, Player Player2)
        {
            this.State = State.Attack;

            this.ClearClientTicks();

            if (this._Player != null)
            {
                Resources.Players.Save(this._Player);
                this._Player = null;
            }

            this._Battle = new Battle();
            this._Battle.AddPlayer(Player1);
            this._Battle.AddPlayer(Player2);
            this.SectorManager.LoadTileMap(null);
        }

        /// <summary>
        /// Sets the player.
        /// </summary>
        internal void SetPlayer(Player Player)
        {
            if (this._Player != null)
            {
                Resources.Players.Save(this._Player);
            }

            this._Player = Player;
            this._Player.GameMode = this;
            this._Player.Home.GameMode = this;
        }

        /// <summary>
        /// Sets the random seed.
        /// </summary>
        internal void SetRandomSeed(int Seed)
        {
            this._Random.Seed = Seed;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this._Player != null)
            {
                this.Home.Tick();
            }
            else
            {
                if (this._Battle != null)
                {
                    this._Battle.Tick();
                }
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(float DeltaTime)
        {
            this.Time.Update(DeltaTime);
        }

        /// <summary>
        /// Updates the sector ticks.
        /// </summary>
        internal void UpdateSectorTicks(float DeltaTime)
        {
            this.Update(DeltaTime);
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Packet)
        {
            Packet.ResetChecksum();
            
            Packet.AddVInt(this.Time.SubTick);
            Packet.AddVInt(Packet.Checksum);

            Packet.AddVInt(TimeUtil.Timestamp);
            Packet.AddVInt(11);
            
            this.Time.Encode(Packet);
            this._Random.Encode(Packet);

            Packet.AddVInt(1005459526); // ?

            if (this._Battle != null)
            {
                this._Battle.Encode(Packet);
            }
            else 
                this.Home.Encode(Packet.ByteStream);

            Packet.AddVInt(12);
            Packet.AddRange("00-00-00-91".HexaToBytes());
            Packet.AddVInt(Packet.Checksum);

            this.CommandManager.Encode(Packet);
        }
    }
}