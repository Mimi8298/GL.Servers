namespace GL.Servers.CoC.Logic
{
    public class Vector2
    {
        internal int X;
        internal int Y;

        internal int HashCode
        {
            get
            {
                return this.Y + 31 * this.X;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> class.
        /// </summary>
        public Vector2()
        {
            // Vector2.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> class.
        /// </summary>
        public Vector2(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Adds a vector2.
        /// </summary>
        internal void Add(Vector2 Vector2)
        {
            this.X += Vector2.X;
            this.Y += Vector2.Y;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        internal Vector2 Clone()
        {
            return new Vector2(this.X, this.Y);
        }

        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        internal int Dot(Vector2 Vector2)
        {
            return (this.X * Vector2.X) + (this.Y * Vector2.Y);
        }

        /// <summary>
        /// Returns the unsigned angle in degrees between from and to.
        /// </summary>
        internal int GetAngle()
        {
            // TODO Implement LogicVector2::getAngle.
            return 0;
        }

        /// <summary>
        /// Returns the distance between this vector and specified vector.
        /// </summary>
        internal int GetDistance(Vector2 Vector2)
        {
            int X = this.X - Vector2.X;
            int Distance = 0x7FFFFFFF;

            if (X + 46340 <= 0x16A08)
            {
                int Y = this.Y - Vector2.Y;

                if (Y + 46340 <= 0x16A08)
                {
                    Distance = X * X + Y * Y;
                }
            }

            return Math.Sqrt(Distance);
        }

        /// <summary>
        /// Returns the distance between this vector and specified vector.
        /// </summary>
        internal int GetDistanceSquared(Vector2 Vector2)
        {
            int X = this.X - Vector2.X;
            int Distance = 0x7FFFFFFF;

            if (X + 46340 <= 0x16A08)
            {
                int Y = this.Y - Vector2.Y;

                if (Y + 46340 <= 0x16A08)
                {
                    Distance = X * X + Y * Y;
                }
            }

            return Distance;
        }

        /// <summary>
        /// Returns the distance between this vector and specified vector.
        /// </summary>
        internal int GetDistanceSquaredHelper(int X, int Y)
        {
            int Distance = 0x7FFFFFFF;

            X += this.X;

            if (X + 46340 <= 0x16A08)
            {
                Y += this.Y;

                if (Y + 46340 <= 0x16A08)
                {
                    Distance = X * X + Y * Y;
                }
            }

            return Distance;
        }

        /// <summary>
        /// Calculates the length of the vector.
        /// </summary>
        internal int GetLength()
        {
            int Length = 0x7FFFFFFF;

            if (46340 - this.X <= 0x16A08)
            {
                if (46340 - this.Y <= 0x16A08)
                {
                    Length = this.X * this.X + this.Y * this.Y;
                }
            }

            return Math.Sqrt(Length);
        }

        /// <summary>
        /// Calculates the length of the vector.
        /// </summary>
        internal int GetLengthSquared()
        {
            int Length = 0x7FFFFFFF;

            if (46340 - this.X <= 0x16A08)
            {
                if (46340 - this.Y <= 0x16A08)
                {
                    Length = this.X * this.X + this.Y * this.Y;
                }
            }

            return Length;
        }

        /// <summary>
        /// Returns if the given vector is exactly equal to this vector.
        /// </summary>
        internal bool IsEqual(Vector2 Vector2)
        {
            if (Vector2 != null)
            {
                return this.X == Vector2.X && this.Y == Vector2.Y;
            }

            return false;
        }

        /// <summary>
        /// Returns if the vector is int area.
        /// </summary>
        internal bool IsInArea(int MinX, int MinY, int MaxX, int MaxY)
        {
            if (this.X >= MinX && this.Y >= MinY)
            {
                return this.X < MinX + MaxX && this.Y < MaxY + MinY;
            }

            return false;
        }

        /// <summary>
        /// Multiplies the components of two vectors by one another.
        /// </summary>
        internal void Multiply(Vector2 Vector2)
        {
            this.X *= Vector2.X;
            this.Y *= Vector2.Y;
        }

        /// <summary>
        /// Turns the current vector into a unit vector. The result is a vector one unit in length pointing in the same direction as the original vector.
        /// </summary>
        internal void Normalize(int Value)
        {
            int Length = this.GetLengthSquared();

            if (Length > 0)
            {
                this.X = this.X * Value / Length;
                this.Y = this.Y * Value / Length;
            }
        }

        /// <summary>
        /// Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
        /// </summary>
        internal void Rotate(int Degrees)
        {
            this.X = Math.GetRotatedX(this.X, this.Y, Degrees);
            this.Y = Math.GetRotatedY(this.X, this.Y, Degrees);
        }

        /// <summary>
        /// Sets this vector position.
        /// </summary>
        internal void Set(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Subtracts a vector from a vector.
        /// </summary>
        internal void Substract(Vector2 Vector2)
        {
            this.X -= Vector2.X;
            this.Y -= Vector2.Y;
        }

        public override string ToString()
        {
            return "LogicVector2(" + this.X + "," + this.Y + ")";
        }
    }
}