using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OP_Engine.Characters;
using OP_Engine.Tiles;

namespace OP_Engine.Utility
{
    public class Pathing : Something
    {
        #region Variables

        

        #endregion

        #region Constructor

        public Pathing()
        {
            
        }

        #endregion

        #region Methods

        public virtual List<ALocation> Get_Path(Layer ground, Character character, Tile tile, int max_distance, bool stop_next_to_tile)
        {
            List<ALocation> result = new List<ALocation>();

            List<ALocation> path = new List<ALocation>();
            ALocation target = new ALocation((int)tile.Location.X, (int)tile.Location.Y);

            ALocation start = new ALocation((int)character.Location.X, (int)character.Location.Y);
            List<ALocation> open = new List<ALocation>();
            path.Add(start);
            ALocation last_min = start;

            bool reached = false;
            for (int i = 0; i < max_distance; i++)
            {
                if (last_min != null)
                {
                    List<ALocation> locations = GetLocations(ground, last_min);
                    foreach (ALocation location in locations)
                    {
                        if (!HasLocation(path, location))
                        {
                            location.Distance_ToStart = GetDistance(new Vector2(location.X, location.Y), new Vector2(start.X, start.Y));
                            location.Distance_ToDestination = GetDistance(new Vector2(location.X, location.Y), new Vector2(target.X, target.Y));
                            location.Parent = last_min;
                            open.Add(location);
                        }
                    }

                    if (open.Count > 0)
                    {
                        ALocation min = Get_MinLocation_Target(open, target, last_min);
                        open.Clear();
                        path.Add(min);
                        last_min = min;

                        if (DestinationReached(min, tile, stop_next_to_tile))
                        {
                            reached = true;
                            break;
                        }
                    }
                    else
                    {
                        last_min = last_min.Parent;
                    }
                }
                else
                {
                    break;
                }
            }

            if (reached)
            {
                result = Optimize_Path(path, start);
            }

            return result;
        }

        public virtual List<ALocation> GetLocations(Layer ground, ALocation location)
        {
            List<ALocation> locations = new List<ALocation>();

            ALocation North = new ALocation(location.X, location.Y - 1);
            if (Walkable(ground, North))
            {
                locations.Add(North);
            }

            ALocation East = new ALocation(location.X + 1, location.Y);
            if (Walkable(ground, East))
            {
                locations.Add(East);
            }

            ALocation South = new ALocation(location.X, location.Y + 1);
            if (Walkable(ground, South))
            {
                locations.Add(South);
            }

            ALocation West = new ALocation(location.X - 1, location.Y);
            if (Walkable(ground, West))
            {
                locations.Add(West);
            }

            return locations;
        }

        public virtual List<ALocation> Optimize_Path(List<ALocation> possible, ALocation start)
        {
            List<ALocation> path = new List<ALocation>();

            ALocation min = possible[possible.Count - 1];
            List<ALocation> open = new List<ALocation>();
            path.Add(min);
            ALocation last_min = min;

            bool reached = false;
            int path_max = possible.Count;
            for (int i = 0; i < path_max; i++)
            {
                List<ALocation> locations = Get_ClosedLocations(possible, last_min);
                foreach (ALocation location in locations)
                {
                    if (!HasLocation(path, location))
                    {
                        open.Add(location);
                    }
                }

                if (open.Count > 0)
                {
                    min = Get_MinLocation_Start(open);
                    open.Clear();
                    possible = Path_RemoveTile(possible, min);
                    path.Add(min);
                    last_min = min;

                    if (min.X == start.X &&
                        min.Y == start.Y)
                    {
                        reached = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (reached)
            {
                return path;
            }

            return null;
        }

        public virtual List<ALocation> Get_ClosedLocations(List<ALocation> possible, ALocation location)
        {
            List<ALocation> locations = new List<ALocation>();

            ALocation North = new ALocation(location.X, location.Y - 1);
            foreach (ALocation existing in possible)
            {
                if (existing.X == North.X &&
                    existing.Y == North.Y)
                {
                    locations.Add(existing);
                    break;
                }
            }

            ALocation East = new ALocation(location.X + 1, location.Y);
            foreach (ALocation existing in possible)
            {
                if (existing.X == East.X &&
                    existing.Y == East.Y)
                {
                    locations.Add(existing);
                    break;
                }
            }

            ALocation South = new ALocation(location.X, location.Y + 1);
            foreach (ALocation existing in possible)
            {
                if (existing.X == South.X &&
                    existing.Y == South.Y)
                {
                    locations.Add(existing);
                    break;
                }
            }

            ALocation West = new ALocation(location.X - 1, location.Y);
            foreach (ALocation existing in possible)
            {
                if (existing.X == West.X &&
                    existing.Y == West.Y)
                {
                    locations.Add(existing);
                    break;
                }
            }

            return locations;
        }

        public virtual bool HasLocation(List<ALocation> locations, ALocation location)
        {
            foreach (ALocation existing in locations)
            {
                if (existing.X == location.X &&
                    existing.Y == location.Y)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual ALocation Get_MinLocation_Target(List<ALocation> locations, ALocation target, ALocation previous)
        {
            ALocation current = locations[0];

            float current_near = current.Distance_ToDestination - current.Priority;
            float current_far = current.Distance_ToStart + current.Priority;

            foreach (ALocation location in locations)
            {
                float pref_near = location.Distance_ToDestination - location.Priority;
                float pref_far = location.Distance_ToStart + location.Priority;

                bool equal = false;
                if (pref_near == current_near &&
                    pref_far == current_far)
                {
                    equal = true;

                    bool preferred = false;
                    if (target.X < previous.X)
                    {
                        if (target.Y < previous.Y)
                        {
                            //NorthWest
                            if (location.X <= current.X ||
                                location.Y <= current.Y)
                            {
                                CryptoRandom random = new CryptoRandom();
                                int chance = random.Next(0, 2);
                                if (chance == 0)
                                {
                                    preferred = true;
                                }
                            }
                        }
                        else if (target.Y > previous.Y)
                        {
                            //SouthWest
                            if (location.X <= current.X ||
                                location.Y >= current.Y)
                            {
                                CryptoRandom random = new CryptoRandom();
                                int chance = random.Next(0, 2);
                                if (chance == 0)
                                {
                                    preferred = true;
                                }
                            }
                        }
                        else if (target.Y == previous.Y)
                        {
                            //West
                            if (location.X <= current.X)
                            {
                                preferred = true;
                            }
                        }
                    }
                    else if (target.X > previous.X)
                    {
                        if (target.Y < previous.Y)
                        {
                            //NorthEast
                            if (location.X >= current.X ||
                                location.Y <= current.Y)
                            {
                                CryptoRandom random = new CryptoRandom();
                                int chance = random.Next(0, 2);
                                if (chance == 0)
                                {
                                    preferred = true;
                                }
                            }
                        }
                        else if (target.Y > previous.Y)
                        {
                            //SouthEast
                            if (location.X >= current.X ||
                                location.Y >= current.Y)
                            {
                                CryptoRandom random = new CryptoRandom();
                                int chance = random.Next(0, 2);
                                if (chance == 0)
                                {
                                    preferred = true;
                                }
                            }
                        }
                        else if (target.Y == previous.Y)
                        {
                            //East
                            if (location.X >= current.X)
                            {
                                preferred = true;
                            }
                        }
                    }
                    else if (target.X == previous.X)
                    {
                        if (target.Y < previous.Y)
                        {
                            //North
                            if (location.Y <= current.Y)
                            {
                                preferred = true;
                            }
                        }
                        else if (target.Y > previous.Y)
                        {
                            //South
                            if (location.Y >= current.Y)
                            {
                                preferred = true;
                            }
                        }
                    }

                    if (preferred)
                    {
                        current = location;
                        current_near = pref_near;
                        current_far = pref_far;
                    }
                }

                if (!equal)
                {
                    if ((pref_near <= current_near && pref_far > current_far) ||
                         pref_near < current_near)
                    {
                        current = location;
                        current_near = pref_near;
                        current_far = pref_far;
                    }
                }
            }

            return current;
        }

        public virtual ALocation Get_MinLocation_Start(List<ALocation> locations)
        {
            ALocation current = locations[0];

            float current_far = current.Distance_ToStart - current.Priority;

            foreach (ALocation location in locations)
            {
                float pref_far = location.Distance_ToStart - location.Priority;
                if (pref_far < current_far)
                {
                    current = location;
                    current_far = pref_far;
                }
            }

            return current;
        }

        public virtual List<ALocation> Path_RemoveTile(List<ALocation> path, ALocation tile)
        {
            foreach (ALocation location in path)
            {
                if (location.X == tile.X &&
                    location.Y == tile.Y)
                {
                    path.Remove(location);
                    break;
                }
            }

            return path;
        }

        public virtual bool DestinationReached(ALocation location, Tile target_tile, bool stop_next_to_tile)
        {
            if (stop_next_to_tile)
            {
                if ((location.X == target_tile.Location.X - 1 && location.Y == target_tile.Location.Y) ||
                    (location.X == target_tile.Location.X + 1 && location.Y == target_tile.Location.Y) ||
                    (location.X == target_tile.Location.X && location.Y == target_tile.Location.Y - 1) ||
                    (location.X == target_tile.Location.X && location.Y == target_tile.Location.Y + 1))
                {
                    return true;
                }
            }
            else
            {
                if (location.X == target_tile.Location.X &&
                    location.Y == target_tile.Location.Y)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool Walkable(Layer ground, ALocation location)
        {
            Tile tile = ground.GetTile(new Vector2(location.X, location.Y));
            if (tile != null)
            {
                if (tile.BlocksMovement)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public virtual int GetDistance(Vector2 origin, Vector2 location)
        {
            int x_diff = (int)origin.X - (int)location.X;
            if (x_diff < 0)
            {
                x_diff *= -1;
            }

            int y_diff = (int)origin.Y - (int)location.Y;
            if (y_diff < 0)
            {
                y_diff *= -1;
            }

            return x_diff + y_diff;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
