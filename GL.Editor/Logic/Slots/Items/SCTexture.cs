namespace GL.Editor.Logic.Slots.Items
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    using GL.Editor.Extensions.Binary;
    using GL.Editor.Extensions.List;
    using GL.Editor.Logic.Enums;

    internal class SCTexture
    {
        internal SCFile SCFile;

        internal List<Bitmap> Images;
        internal List<Size> Sizes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SCTexture"/> class.
        /// </summary>
        /// <param name="File">The file used to load this instance.</param>
        internal SCTexture(SCFile File)
        {
            this.SCFile = File;

            this.Images = new List<Bitmap>();
            this.Sizes  = new List<Size>();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        internal void Load()
        {
            using (Reader Reader = new Reader(this.SCFile.SCTextureI.OpenRead()))
            {
                int Index = 0;

                while (!Reader.EndOfStream())
                {
                    byte PacketID       = Reader.ReadByte();
                    uint PacketSize     = Reader.ReadUInt32();

                    if (PacketSize > 0)
                    {
                        byte Pixel_Format = Reader.ReadByte();

                        ushort Width    = Reader.ReadUInt16();
                        ushort Height   = Reader.ReadUInt16();

                        bool Is32x32    = false;

                        switch (PacketID)
                        {
                            case 27:
                            case 28:
                                Is32x32 = true;
                                break;
                        }

                        this.Sizes.Add(new Size(Width, Height));
                        this.Images.Add(new Bitmap(Width, Height, PixelFormat.Format32bppArgb));

                        int ModWidth    = Width % 32;
                        int TimeWidth   = (Width - ModWidth) / 32;

                        int ModHeight   = Height % 32;
                        int TimeHeight  = (Height - ModHeight) / 32;

                        Color[,] Pixels = new Color[Height, Width];

                        if (Is32x32)
                        {
                            for (int TimeH = 0; TimeH < TimeHeight + 1; TimeH++)
                            {
                                int OffsetX = 0;
                                int OffsetY = 0;

                                int LineH = 32;

                                if (TimeH == TimeHeight)
                                {
                                    LineH = ModHeight;
                                }

                                for (int Time = 0; Time < TimeWidth; Time++)
                                {
                                    for (int PositionY = 0; PositionY < LineH; PositionY++)
                                    {
                                        for (int PositionX = 0; PositionX < 32; PositionX++)
                                        {
                                            OffsetX = Time * 32;
                                            OffsetY = TimeH * 32;

                                            Pixels[PositionY + OffsetY, PositionX + OffsetX] = this.GetColorForPixelFormat(Reader, Pixel_Format);
                                        }
                                    }
                                }

                                for (int PositionY = 0; PositionY < LineH; PositionY++)
                                {
                                    for (int PositionX = 0; PositionX < ModWidth; PositionX++)
                                    {
                                        OffsetX = TimeWidth * 32;
                                        OffsetY = TimeH * 32;

                                        Pixels[PositionY + OffsetY, PositionX + OffsetX] = this.GetColorForPixelFormat(Reader, Pixel_Format);
                                    }
                                }
                            }
                        }

                        for (int Row = 0; Row < Pixels.GetLength(0); Row++)
                        {
                            for (int Column = 0; Column < Pixels.GetLength(1); Column++)
                            {
                                Color _Color = Color.Red;

                                _Color = Is32x32 ? Pixels[Row, Column] : this.GetColorForPixelFormat(Reader, Pixel_Format);

                                this.Images[Index].SetPixel(Column, Row, _Color);
                            }
                        }

                        Index += 1;
                    }
                    else
                    {
                        Reader.ReadBytes(5);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the color for pixel format.
        /// </summary>
        /// <param name="PixelFormat">The tex reader.</param>
        /// <param name="TexReader">The tex pixel format.</param>
        internal Color GetColorForPixelFormat(BinaryReader TexReader, int PixelFormat = 0)
        {
            Color PColor    = Color.Red;
            Format Format   = (Format) PixelFormat;

            switch (Format)
            {
                case Format.RGBA_8888:
                {
                    byte ColorR = TexReader.ReadByte();
                    byte ColorG = TexReader.ReadByte();
                    byte ColorB = TexReader.ReadByte();
                    byte ColorA = TexReader.ReadByte();

                    PColor = Color.FromArgb((ColorA << 24) | (ColorR << 16) | (ColorG << 8) | ColorB);

                    break;
                }

                case Format.RGBA_4444:
                {
                    ushort RColor = TexReader.ReadUInt16();

                    int ColorR = ((RColor >> 12) & 0xF) << 4;
                    int ColorG = ((RColor >> 8) & 0xF) << 4;
                    int ColorB = ((RColor >> 4) & 0xF) << 4;
                    int ColorA = (RColor & 0xF) << 4;

                    PColor = Color.FromArgb(ColorA, ColorR, ColorG, ColorB);

                    break;
                }

                case Format.RGB_565:
                {
                    ushort _Color = TexReader.ReadUInt16();

                    int Red     = ((_Color >> 11) & 0x1F) << 3;
                    int Green   = ((_Color >> 5) & 0x3F) << 2;
                    int Blue    = (_Color & 0X1F) << 3;

                    PColor      = Color.FromArgb(Red, Green, Blue);

                    break;
                }

                case Format.LUMINANCE8_A8:
                {
                    ushort _Color   = TexReader.ReadUInt16();

                    int Alpha       = _Color & 0xFF;
                    int RGB         = _Color >> 8;

                    int Red         = RGB;
                    int Green       = RGB;
                    int Blue        = RGB;

                    PColor          = Color.FromArgb(Alpha, Red, Green, Blue);

                    break;
                }

                case Format.LUMINANCE8:
                {
                    ushort _Color   = TexReader.ReadByte();

                    int Red         = _Color;
                    int Green       = _Color;
                    int Blue        = _Color;

                    PColor          = Color.FromArgb(Red, Green, Blue);

                    break;
                }
            }

            return PColor;
        }
    }
}