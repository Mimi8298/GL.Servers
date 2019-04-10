namespace GL.Clients.BS.Packets.Messages.Client
{
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Ask_Battle_Result : Message
    {
        internal int Unknown1;
        internal int Unknown2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_Battle_Result"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Ask_Battle_Result(Device Device) : base(Device)
        {
            this.Identifier = 14110;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            // 00  00  00  C4-81-BC-57-02  06-10-01-00-00-01-00-00-00-06-42-65-72-6B-61-6E-10-00-00-00-00-00-00-00-01-31-10-08-00-00-00-00-00-00-01-32-10-01-00-01-00-00-00-00-01-33-10-09-00-01-00-00-00-00-01-34-10-08-00-01-00-00-00-00-01-35
            
            this.Data.AddVInt(0);
            this.Data.AddVInt(0);
            this.Data.AddVInt(0);

            this.Data.AddHexa("C4-81-BC-57-02"); // Event ID

            this.Data.AddVInt(6);
            {
                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(0);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(1);
                    this.Data.AddVInt(1);
                    this.Data.AddString("Berkan");
                }

                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(10);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);

                    this.Data.AddString("1"); // Bot #1
                }

                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(02);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);

                    this.Data.AddString("2"); // Bot #2
                }

                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(9);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);
                    this.Data.AddVInt(0);

                    this.Data.AddString("3"); // Bot #3
                }

                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(1);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(1);
                    this.Data.AddVInt(0);

                    this.Data.AddString("4"); // Bot #4
                }

                {
                    this.Data.AddVInt(16);
                    this.Data.AddVInt(7);

                    this.Data.AddVInt(0);
                    this.Data.AddVInt(1);
                    this.Data.AddVInt(0);

                    this.Data.AddString("5"); // Bot #5
                }
            }
        }
    }
}