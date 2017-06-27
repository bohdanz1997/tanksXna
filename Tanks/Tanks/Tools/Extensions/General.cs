using System;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Enums;

namespace Tanks.Tools.Extensions
{
    public static class General
    {
        internal static bool IsTurn(this Position position, Direction nextDir)
        {
            return position.direction != nextDir;
        }

        internal static bool InCell(this Position position, int cellSize)
        {
            return
                position.X % cellSize == 0 &&
                position.Y % cellSize == 0;
        }

        internal static bool CheckCollision(this Collision collision, Vector2 p, int distance)
        {
            return ((Math.Abs(collision.bounds.X - p.X) < distance) &&
                    (Math.Abs(collision.bounds.Y - p.Y) < distance));
        }
        
        internal static Direction ToDirection(this Vector2 v)
        {
            return (v.X == 0) ? ((v.Y == -1) ? Direction.Up : Direction.Down) :
                ((v.X == -1) ? Direction.Left : Direction.Right);
        }

        internal static Vector2 ToVector(this Direction d)
        {
            switch (d)
            {
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.Left:
                    return new Vector2(-1, 0);
                case Direction.Right:
                    return new Vector2(1, 0);
            }
            return Vector2.Zero;
        }

        internal static Vector2 Divide(this Vector2 v, float value)
        {
            return new Vector2((int)(v.X / value), (int)(v.Y / value));
        }

        internal static int DistanceTo(this Vector2 v1, Vector2 v2)
        {
            return (int)(Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2)));
        }

        internal static bool ArrayContains(EntityType[] array, object value)
        {
            foreach (var i in array)
            {
                if (i.ToString() == value.ToString())
                    return true;
            }
            return false;
        }

        internal static bool EntityTypeEquals(EntityType t, object value)
        {
            return t.ToString() == value.ToString();
        }
    }
}
