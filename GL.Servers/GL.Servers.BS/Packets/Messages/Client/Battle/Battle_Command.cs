namespace GL.Servers.BS.Packets.Messages.Client
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Battle_Command : Message
    {
        private int Tick;
        private int Checksum;
        private int Count;

        private byte[] Commands;

        private List<Command> Historic;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Command"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Battle_Command(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Battle_Command.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Tick       = this.Reader.ReadVInt();
            this.Checksum   = this.Reader.ReadVInt();
            this.Count      = this.Reader.ReadVInt();

            if (this.Count > 0)
            {
                if (this.Count <= 512)
                {
                    this.Commands = this.Reader.ReadBytes((int)(this.Reader.BaseStream.Length - this.Reader.BaseStream.Position));
                    this.Historic = new List<Command>(this.Count);
                }
                else
                {
                    Logging.Error(this.GetType(), this.Device, "Error when executing commands, the limit has been reached.");
                }
            }
            else
            {
                if (!this.Reader.EndOfStream)
                {
                    this.ShowBuffer();
                }
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Count > 0 && this.Count <= 512)
            {
                using (Reader Reader = new Reader(this.Commands))
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        ushort CommandIndex = (ushort) Reader.ReadVInt();
                        ushort CommandID    = (ushort) Reader.ReadVInt();

                        if (Factory.Commands.ContainsKey(CommandID))
                        {
                            Command Command = Activator.CreateInstance(Factory.Commands[CommandID], Reader, this.Device, CommandID) as Command;

                            if (Command != null)
                            {
                                Command.Decode();
                                Command.Process();

                                this.Historic.Add(Command);
                            }
                        }
                        else
                        {
                            Logging.Info(this.GetType(), "Battle Command " + CommandID + " is not handled. [" + (i + 1) + "/" + this.Count + "] " + (this.Historic.Count > 0 ? "Last command was " + this.Historic[i - 1].GetType().Name : string.Empty));
                            break;
                        }
                    }
                }

                this.Historic = null;
            }
        }

        /*  A6-03  37  01  [  01  |  01  A4-01  B2-15  AA-83-01  ]
            A6-03  0D  01  [  02  |  01  A4-01  8E-1F  89-83-01  ]

            BA-03  31  00

            90-04  1E  01  [  03  |  01  A4-01  95-1F  9E-7C     ]
            91-04  11  01  [  04  |  01  A4-01  B4-1F  8D-83-01  ]

            A5-04  21  00
            BC-04  0F  00

            90-05  22  01  [  05  |  01  A4-01  BD-28  96-85-01  ]
            91-05  10  01  [  06  |  01  A4-01  BE-1F  8F-83-01  ]

            A7-05  20  00
            BB-05  21  00

            8D-06  21  01  [  09  |  01  A4-01  90-29  81-84-01  ]
            98-06  10  01  [  0B  |  01  A4-01  B7-2C  8A-83-01  ]

            A1-06  21  01  [  0D  |  01  A4-01  97-2D  8C-83-01  ]
            A5-06  21  01  [  0E  |  01  A4-01  97-2D  8E-83-01  ]
            A9-06  10  01  [  0F  |  01  A4-01  97-2D  8E-83-01  ]
            B2-06  1F  01  [  11  |  01  A4-01  96-2D  8E-83-01  ]

            B6-06  21  01  [  12  |  01  A4-01  96-2D  8E-83-01  ]
            BA-06  20  01  [  13  |  01  A4-01  97-2D  8E-83-01  ]
            BE-06  22  01  [  14  |  01  A4-01  97-2D  8F-83-01  ]
            85-07  20  01  [  15  |  01  A4-01  97-2D  90-83-01  ]
            89-07  10  01  [  16  |  01  A4-01  97-2D  90-83-01  ]
            8D-07  20  01  [  17  |  01  A4-01  97-2D  90-83-01  ]
            8F-07  0F  01  [  18  |  01  A4-01  B7-26  AC-84-01  ]
            A2-07  31  00
            B9-07  31  00
            8D-08  21  00
            A4-08  10  00
            B8-08  31  00
            8F-09  21  00
        */
    }
}
 