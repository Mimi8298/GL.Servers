namespace GL.Clients.BS.Logic.Slots
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Clients.BS.Core;
    using GL.Clients.BS.Packets.Messages.Client;

    internal class Bots : ConcurrentStack<Device>
    {
        internal int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bots"/> class.
        /// </summary>
        internal Bots()
        {
            if (File.Exists("credentials.txt"))
            {
                string[] Credentials    = File.ReadAllLines("credentials.txt");
                
                Parallel.ForEach(Credentials, Credential =>
                {
                    string[] Parameters = Credential.Split(':');

                    if (Parameters.Length == 3)
                    {
                        int HighID      = int.Parse(Parameters[0]);
                        int LowID       = int.Parse(Parameters[1]);

                        string Token    = Parameters[2];

                        // if (Token.Length != 40)
                        {
                            // Logging.Info(this.GetType(), "Error when reading a line from credentials file, token is corrupt.");
                        }
                        // else if (HighID < 0 || LowID <= 0)
                        {
                            // Logging.Info(this.GetType(), "Error when reading a line from credentials.txt, ids are corrupt.");
                        }
                        // else
                        {
                            Device Device   = new Device
                            {
                                BotId       = Interlocked.Increment(ref this.Seed),
                                Player      =
                                {
                                    HighID  = HighID,
                                    LowID   = LowID,
                                    Token   = Token
                                }
                            };

                            Logging.Info(this.GetType(), "Initializing #" + this.Seed + "..");

                            Device.Connect();

                            if (LowID == 0)
                            {
                                Device.Gateway.Send(new Go_Home(Device));
                            }

                            this.Put(Device);
                        }
                    }
                    else
                    {
                        Logging.Info(this.GetType(), "Error when reading a line from credentials.txt, line is corrupt.");
                    }
                });
            }
            else
            {
                Logging.Error(this.GetType(), "Error, credentials.txt don't exist.");
            }
        }

        /// <summary>
        /// Takes one unused instance of <see cref="Device"/>.
        /// </summary>
        internal Device Take()
        {
            Device Bot;

            if (!this.TryPop(out Bot))
            {
                /* Logging.Info(this.GetType(), "Warning, generating a new bot.");

                Bot = new Device
                {
                    BotId = Interlocked.Increment(ref this.Seed)
                };

                Bot.Connect(); */

                Bot = null;
            }

            return Bot;
        }

        /// <summary>
        /// Pushes a unused instance of
        /// <see cref="Device"/> in the stack.
        /// </summary>
        internal void Put(Device Bot)
        {
            if (Bot != null)
            {
                this.Push(Bot);
            }
        }
    }
}