namespace GL.Editor.Logic.Slots.Items
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using GL.Editor.Extensions.Binary;
    using GL.Editor.Extensions.List;

    internal class SCInfo
    {
        internal SCFile SCFile;
        internal FileInfo Informations;

        internal Exports Exports;
        internal Temporary Temporary;

        internal uint ShapeCount;
        internal uint MovieClipCount;
        internal uint TextureCount;
        internal uint TextFieldCount;
        internal uint MatrixCount;
        internal uint ColorTransformCount;
        internal uint ExportCount;
        internal uint Tag16;

        /// <summary>
        /// Initializes a new instance of the <see cref="SCInfo"/> class.
        /// </summary>
        /// <param name="SCFile">The sc file used to load this instance.</param>
        internal SCInfo(SCFile SCFile)
        {
            this.SCFile     = SCFile;

            this.Exports    = new Exports();
            this.Temporary  = new Temporary();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        internal void Load()
        {
            using (Reader Reader = new Reader(this.SCFile.SCInfoI.OpenRead()))
            {
                this.ShapeCount             = Reader.ReadUInt16();
                this.MovieClipCount         = Reader.ReadUInt16();
                this.TextureCount           = Reader.ReadUInt16();
                this.TextFieldCount         = Reader.ReadUInt16();
                this.MatrixCount            = Reader.ReadUInt16();
                this.ColorTransformCount    = Reader.ReadUInt16();

                Reader.Seek(5, SeekOrigin.Current);

                this.ExportCount = Reader.ReadUInt16();

                for (uint Index = 0; Index < this.ExportCount; Index++)
                {
                    uint Identifier = Reader.ReadUInt16();

                    if (!this.Exports.ContainsKey(Identifier))
                    {
                        this.Exports.Add(new Export(Identifier));
                    }
                    else
                    {
                        this.Exports[Identifier].SubCount += 1;
                    }
                }

                foreach (KeyValuePair<uint, Export> Export in this.Exports)
                {
                    Export.Value.Name = Reader.ReadASCII();

                    for (int Index = 0; Index < Export.Value.SubCount; Index++)
                    {
                        Export.Value.Name += ", " + Reader.ReadASCII();
                    }
                }

                while (!Reader.EndOfStream())
                {
                    byte Type   = Reader.ReadByte();
                    uint Length = Reader.ReadUInt32();

                    switch (Type)
                    {
                        case 0x00:
                        case 0x17:
                        case 0x1A:
                        {
                            break;
                        }

                        case 0x01:
                        case 0x18:
                        {
                            byte Format = Reader.ReadByte();
                            uint Width  = Reader.ReadUInt16();
                            uint height = Reader.ReadUInt16();

                            break;
                        }

                        case 0x08:
                        {
                            uint[] Points = new uint[6];

                            for (int Index = 0; Index < 6; Index++)
                            {
                                Points[Index] = Reader.ReadUInt32();
                            }

                            // Matrix _Matrix = new Matrix(Points[0], Points[1], Points[2], Points[3], Points[4], Points[5]);

                            break;
                        }

                        case 0x09:
                        {
                            byte MultR = Reader.ReadByte();
                            byte MultG = Reader.ReadByte();
                            byte MultB = Reader.ReadByte();

                            byte ColorA = Reader.ReadByte();
                            byte ColorR = Reader.ReadByte();
                            byte ColorG = Reader.ReadByte();
                            byte ColorB = Reader.ReadByte();

                            break;
                        }

                        case 0x0B:
                        {
                            Reader.ReadBytes((int) Length);

                            break;
                        }

                        case 0x0C:
                        {
                            uint ClipID = Reader.ReadUInt16();
                            uint ClipFPS = Reader.ReadByte();
                            uint ClipFCount = Reader.ReadUInt16();

                            Export _Export = new Export(ClipID);

                            if (!this.Exports.ContainsKey(ClipID))
                            {
                                this.Exports.Add(_Export);
                            }
                            else
                            {
                                _Export = this.Exports[ClipID];
                            }

                            _Export.FPS = ClipFPS;
                            _Export.FrameCount = ClipFCount;

                            /* SPIRITES */

                            uint ClipSpirites = Reader.ReadUInt32();

                            for (int Index = 0; Index < ClipSpirites; Index++)
                            {
                                uint _Identifier = Reader.ReadUInt16();

                                _Export.Spirites.Add(new Spirite(_Identifier));

                                Reader.ReadUInt16();
                                Reader.ReadUInt16();
                            }

                            /* END - SPIRITES */

                            /* SHAPES */

                            uint ClipShapes = Reader.ReadUInt16();

                            for (int Index = 0; Index < ClipShapes; Index++)
                            {
                                uint _Identifier = Reader.ReadUInt16();

                                if (this.Temporary.Shapes.ContainsKey(_Identifier))
                                {
                                    _Export.Shapes.Add(this.Temporary.Shapes[_Identifier]);
                                }
                                else
                                {
                                    _Export.Shapes.Add(new Shape(_Identifier));
                                }
                            }

                            Reader.Seek(ClipShapes, SeekOrigin.Current);

                            foreach (KeyValuePair<uint, Shape> _Shape in _Export.Shapes)
                            {
                                _Shape.Value.Name = Reader.ReadASCII();
                            }

                            if (_Export.Shapes.Count < ClipShapes)
                            {
                                for (int Index = 0; Index < ClipShapes - _Export.Shapes.Count; Index++)
                                {
                                    Reader.ReadASCII();
                                }
                            }

                            /* END - SHAPES */

                            for (int Index = 0; Index < ClipFCount; Index++)
                            {
                                Reader.ReadByte();
                                Reader.ReadUInt32();
                                Reader.ReadUInt16();
                                Reader.ReadASCII();
                            }

                            Reader.ReadBytes(5);

                            break;
                        }

                        case 0x07:
                        case 0x19:
                        case 0x0F:
                        {
                            uint FontID         = Reader.ReadUInt16();
                            string FontName     = Reader.ReadASCII();

                            this.Temporary.Fonts.Add(new Font(FontID, FontName));

                            Reader.ReadBytes((int) Length - (FontName.Length + 3));
                            break;
                        }

                        case 0x12:
                        {
                            uint ShapeID        = Reader.ReadUInt16();
                            uint PolyCount      = Reader.ReadUInt16();
                            uint PointsCount    = Reader.ReadUInt16();

                            this.Temporary.Shapes.Add(new Shape(ShapeID, PolyCount, PointsCount));

                            break;
                        }

                        case 0x16:
                        {
                            int TexID = Reader.Read();
                            int Vertex = Reader.Read();

                            Image Image = this.SCFile.SCTexture.Images[TexID];

                            List<PointF> PointsXY = new List<PointF>(Vertex);
                            List<PointF> PointsUV = new List<PointF>(Vertex);

                            for (int Index = 0; Index < Vertex; Index++)
                            {
                                float X = (float) Reader.ReadUInt32() / -20;
                                float Y = (float) Reader.ReadUInt32() / 20;

                                PointsXY.Add(new PointF(X, Y));
                            }

                            for (int Index = 0; Index < Vertex; Index++)
                            {
                                float U = (float) Reader.ReadUInt16() / ushort.MaxValue * Image.Width;
                                float V = (float) Reader.ReadUInt16() / ushort.MaxValue * Image.Height;

                                PointsUV.Add(new PointF(U, V));
                            }

                            GraphicsPath Polygon = new GraphicsPath();
                            Polygon.AddPolygon(PointsUV.ToArray());

                            int PWidth = Rectangle.Round(Polygon.GetBounds()).Width;
                            int PHeight = Rectangle.Round(Polygon.GetBounds()).Height;

                            if (PWidth > 0 && PHeight > 0)
                            {
                                var Chunk = new Bitmap(PWidth, PHeight);

                                int ChunkX = Rectangle.Round(Polygon.GetBounds()).X;
                                int ChunkY = Rectangle.Round(Polygon.GetBounds()).Y;

                                using (Graphics Graphic = Graphics.FromImage(Chunk))
                                {
                                    Polygon.Transform(new Matrix(1, 0, 0, 1, -ChunkX, -ChunkY));
                                    Graphic.SetClip(Polygon);
                                    Graphic.DrawImage(Image, -ChunkX, -ChunkY);
                                }

                                this.Temporary.Shapes.Last().Value.Chunks.Add(new Chunk(this.Tag16, PointsXY, PointsUV));
                                this.Temporary.Shapes.Last().Value.Image = Chunk;

                                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Images\\" + this.SCFile.SCTextureI.Name);
                                Chunk.Save(Directory.GetCurrentDirectory() + "\\Images\\" + this.SCFile.SCTextureI.Name + "\\shape_" + (this.Temporary.Shapes.Last().Key.ToString() + "_" + this.Temporary.Shapes.Count) + ".png", ImageFormat.Png);
                            }

                            break;
                        }

                        default:
                        {
                            Console.WriteLine("Unknown :");
                            Console.WriteLine("   - ID   : 0x" + Type.ToString("X"));
                            Console.WriteLine("   - Len. : " + Length);

                            Reader.Seek(Length, SeekOrigin.Current);

                            return;
                        }
                    }
                }
            }
        }
    }
}