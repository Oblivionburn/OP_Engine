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

        public static List<Point> GetLine(Point starting_point, Point destination_point)
        {
            return BresenhamLine(starting_point.X, starting_point.Y, destination_point.X, destination_point.Y);
        }

        private static List<Point> BresenhamLine(int x0, int y0, int x1, int y1)
        {
            List<Point> result = new List<Point>();

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = 0;
            int ystep;
            int y = y0;

            if (y0 < y1)
            {
                ystep = 1;
            }
            else
            {
                ystep = -1;
            }

            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                {
                    result.Add(new Point(y, x));
                }
                else
                {
                    result.Add(new Point(x, y));
                }

                error += deltay;
                if (2 * error >= deltax)
                {
                    y += ystep;
                    error -= deltax;
                }
            }

            return result;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        #endregion
    }
}
