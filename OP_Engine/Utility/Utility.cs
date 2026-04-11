using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Utility
{
    public static class Utility
    {
        #region Variables



        #endregion

        #region Methods

        public static float ConvertValueToRange(float value, float old_min, float old_max, float new_min, float new_max)
        {
            //Convert a value from one number range to another (e.g. local space to screen space)

            float old_range = old_max - old_min;
            float new_range = new_max - new_min;

            float new_value = (((value - old_min) * new_range) / old_range) + new_min;

            return new_value;
        }

        public static int DiceRoll(int sides)
        {
            CryptoRandom random = new CryptoRandom();
            return random.Next(1, sides + 1);
        }

        public static bool RandomPercent(float value)
        {
            CryptoRandom random = new CryptoRandom();
            int chance = random.Next(1, 101);
            if (chance <= value)
            {
                return true;
            }

            return false;
        }

        public static bool RegionsOverlapping(Region origin, Region target)
        {
            if (((origin.X >= target.X && origin.X < target.X + target.Width) ||
                 (origin.X + origin.Width > target.X && origin.X + origin.Width <= target.X + target.Width)) &&
                ((origin.Y >= target.Y && origin.Y < target.Y + target.Height) ||
                 (origin.Y + origin.Height > target.Y && origin.Y + origin.Height <= target.Y + target.Height)))
            {
                return true;
            }

            return false;
        }

        public static List<Point> GetLine(Point p1, Point p2)
        {
            int x0 = p1.X, y0 = p1.Y;
            int x1 = p2.X, y1 = p2.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            // Pre-allocate capacity based on the longer axis for speed
            var points = new List<Point>(Math.Max(dx, dy) + 1);

            while (true)
            {
                points.Add(new Point(x0, y0));

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
            return points;
        }

        #endregion
    }
}
