using Microsoft.Xna.Framework.Design;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
    /// <summary>Defines a RectangleF.</summary>
    [TypeConverter(typeof(RectangleConverter))]
    [Serializable]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>Specifies the x-coordinate of the RectangleF.</summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float X;

        /// <summary>Specifies the y-coordinate of the RectangleF.</summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Y;

        /// <summary>Specifies the width of the RectangleF.</summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Width;

        /// <summary>Specifies the height of the RectangleF.</summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Height;

        private static RectangleF _empty = default(RectangleF);

        /// <summary>Returns the x-coordinate of the left side of the RectangleF.</summary>
        public float Left
        {
            get
            {
                return this.X;
            }
        }

        /// <summary>Returns the x-coordinate of the right side of the RectangleF.</summary>
        public float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        /// <summary>Returns the y-coordinate of the top of the RectangleF.</summary>
        public float Top
        {
            get
            {
                return this.Y;
            }
        }

        /// <summary>Returns the y-coordinate of the bottom of the RectangleF.</summary>
        public float Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }

        /// <summary>Gets or sets the upper-left value of the RectangleF.</summary>
        public Vector2 Location
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        /// <summary>Gets the Vector2 that specifies the center of the RectangleF.</summary>
        public Vector2 Center
        {
            get
            {
                return new Vector2(this.X + this.Width / 2, this.Y + this.Height / 2);
            }
        }

        /// <summary>Returns a RectangleF with all of its values set to zero.</summary>
        public static RectangleF Empty
        {
            get
            {
                return RectangleF._empty;
            }
        }

        /// <summary>Gets a value that indicates whether the RectangleF is empty.</summary>
        public bool IsEmpty
        {
            get
            {
                return this.Width == 0 && this.Height == 0 && this.X == 0 && this.Y == 0;
            }
        }

        /// <summary>Initializes a new instance of RectangleF.</summary>
        /// <param name="x">The x-coordinate of the RectangleF.</param>
        /// <param name="y">The y-coordinate of the RectangleF.</param>
        /// <param name="width">Width of the RectangleF.</param>
        /// <param name="height">Height of the RectangleF.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>Changes the position of the RectangleF.</summary>
        /// <param name="amount">The values to adjust the position of the RectangleF by.</param>
        public void Offset(Vector2 amount)
        {
            this.X += amount.X;
            this.Y += amount.Y;
        }

        /// <summary>Changes the position of the RectangleF.</summary>
        /// <param name="offsetX">Change in the x-position.</param>
        /// <param name="offsetY">Change in the y-position.</param>
        public void Offset(float offsetX, float offsetY)
        {
            this.X += offsetX;
            this.Y += offsetY;
        }

        /// <summary>Pushes the edges of the RectangleF out by the horizontal and vertical values specified.</summary>
        /// <param name="horizontalAmount">Value to push the sides out by.</param>
        /// <param name="verticalAmount">Value to push the top and bottom out by.</param>
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            this.X -= horizontalAmount;
            this.Y -= verticalAmount;
            this.Width += horizontalAmount * 2;
            this.Height += verticalAmount * 2;
        }

        /// <summary>Determines whether this RectangleF contains a specified Vector2 represented by its x- and y-coordinates.</summary>
        /// <param name="x">The x-coordinate of the specified Vector2.</param>
        /// <param name="y">The y-coordinate of the specified Vector2.</param>
        public bool Contains(float x, float y)
        {
            return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
        }

        /// <summary>Determines whether this RectangleF contains a specified Vector2.</summary>
        /// <param name="value">The Vector2 to evaluate.</param>
        public bool Contains(Vector2 value)
        {
            return this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height;
        }

        /// <summary>Determines whether this RectangleF contains a specified Vector2.</summary>
        /// <param name="value">The Vector2 to evaluate.</param>
        /// <param name="result">[OutAttribute] true if the specified Vector2 is contained within this RectangleF; false otherwise.</param>
        public void Contains(ref Vector2 value, out bool result)
        {
            result = (this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height);
        }

        /// <summary>Determines whether this RectangleF entirely contains a specified RectangleF.</summary>
        /// <param name="value">The RectangleF to evaluate.</param>
        public bool Contains(RectangleF value)
        {
            return this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height;
        }

        /// <summary>Determines whether this RectangleF entirely contains a specified RectangleF.</summary>
        /// <param name="value">The RectangleF to evaluate.</param>
        /// <param name="result">[OutAttribute] On exit, is true if this RectangleF entirely contains the specified RectangleF, or false if not.</param>
        public void Contains(ref RectangleF value, out bool result)
        {
            result = (this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height);
        }

        /// <summary>Determines whether a specified RectangleF intersects with this RectangleF.</summary>
        /// <param name="value">The RectangleF to evaluate.</param>
        public bool Intersects(RectangleF value)
        {
            return value.X < this.X + this.Width && this.X < value.X + value.Width && value.Y < this.Y + this.Height && this.Y < value.Y + value.Height;
        }

        /// <summary>Determines whether a specified RectangleF intersects with this RectangleF.</summary>
        /// <param name="value">The RectangleF to evaluate</param>
        /// <param name="result">[OutAttribute] true if the specified RectangleF intersects with this one; false otherwise.</param>
        public void Intersects(ref RectangleF value, out bool result)
        {
            result = (value.X < this.X + this.Width && this.X < value.X + value.Width && value.Y < this.Y + this.Height && this.Y < value.Y + value.Height);
        }

        /// <summary>Creates a RectangleF defining the area where one RectangleF overlaps with another RectangleF.</summary>
        /// <param name="value1">The first RectangleF to compare.</param>
        /// <param name="value2">The second RectangleF to compare.</param>
        public static RectangleF Intersect(RectangleF value1, RectangleF value2)
        {
            float num = value1.X + value1.Width;
            float num2 = value2.X + value2.Width;
            float num3 = value1.Y + value1.Height;
            float num4 = value2.Y + value2.Height;
            float num5 = (value1.X > value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y > value2.Y) ? value1.Y : value2.Y;
            float num7 = (num < num2) ? num : num2;
            float num8 = (num3 < num4) ? num3 : num4;
            RectangleF result;
            if (num7 > num5 && num8 > num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.Width = 0;
                result.Height = 0;
            }
            return result;
        }

        /// <summary>Creates a RectangleF defining the area where one RectangleF overlaps with another RectangleF.</summary>
        /// <param name="value1">The first RectangleF to compare.</param>
        /// <param name="value2">The second RectangleF to compare.</param>
        /// <param name="result">[OutAttribute] The area where the two first parameters overlap.</param>
        public static void Intersect(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
        {
            float num = value1.X + value1.Width;
            float num2 = value2.X + value2.Width;
            float num3 = value1.Y + value1.Height;
            float num4 = value2.Y + value2.Height;
            float num5 = (value1.X > value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y > value2.Y) ? value1.Y : value2.Y;
            float num7 = (num < num2) ? num : num2;
            float num8 = (num3 < num4) ? num3 : num4;
            if (num7 > num5 && num8 > num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
                return;
            }
            result.X = 0;
            result.Y = 0;
            result.Width = 0;
            result.Height = 0;
        }

        /// <summary>Creates a new RectangleF that exactly contains two other RectangleFs.</summary>
        /// <param name="value1">The first RectangleF to contain.</param>
        /// <param name="value2">The second RectangleF to contain.</param>
        public static RectangleF Union(RectangleF value1, RectangleF value2)
        {
            float num = value1.X + value1.Width;
            float num2 = value2.X + value2.Width;
            float num3 = value1.Y + value1.Height;
            float num4 = value2.Y + value2.Height;
            float num5 = (value1.X < value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y < value2.Y) ? value1.Y : value2.Y;
            float num7 = (num > num2) ? num : num2;
            float num8 = (num3 > num4) ? num3 : num4;
            RectangleF result;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
            return result;
        }

        /// <summary>Creates a new RectangleF that exactly contains two other RectangleFs.</summary>
        /// <param name="value1">The first RectangleF to contain.</param>
        /// <param name="value2">The second RectangleF to contain.</param>
        /// <param name="result">[OutAttribute] The RectangleF that must be the union of the first two RectangleFs.</param>
        public static void Union(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
        {
            float num = value1.X + value1.Width;
            float num2 = value2.X + value2.Width;
            float num3 = value1.Y + value1.Height;
            float num4 = value2.Y + value2.Height;
            float num5 = (value1.X < value2.X) ? value1.X : value2.X;
            float num6 = (value1.Y < value2.Y) ? value1.Y : value2.Y;
            float num7 = (num > num2) ? num : num2;
            float num8 = (num3 > num4) ? num3 : num4;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
        }

        /// <summary>Determines whether the specified Object is equal to the RectangleF.</summary>
        /// <param name="other">The Object to compare with the current RectangleF.</param>
        public bool Equals(RectangleF other)
        {
            return this.X == other.X && this.Y == other.Y && this.Width == other.Width && this.Height == other.Height;
        }

        /// <summary>Returns a value that indicates whether the current instance is equal to a specified object.</summary>
        /// <param name="obj">Object to make the comparison with.</param>
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is RectangleF)
            {
                result = this.Equals((RectangleF)obj);
            }
            return result;
        }

        /// <summary>Retrieves a string representation of the current object.</summary>
        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3}}}", new object[]
            {
                this.X.ToString(currentCulture),
                this.Y.ToString(currentCulture),
                this.Width.ToString(currentCulture),
                this.Height.ToString(currentCulture)
            });
        }

        /// <summary>Gets the hash code for this object.</summary>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Width.GetHashCode() + this.Height.GetHashCode();
        }

        /// <summary>Compares two RectangleFs for equality.</summary>
        /// <param name="a">Source RectangleF.</param>
        /// <param name="b">Source RectangleF.</param>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
        }

        /// <summary>Compares two RectangleFs for inequality.</summary>
        /// <param name="a">Source RectangleF.</param>
        /// <param name="b">Source RectangleF.</param>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
        }
    }
}
