using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class Layer : Something
    {
        #region Variables

        public long WorldID;
        public long MapID;

        public int Rows;
        public int Columns;
        public int Depth;

        public List<Tile> Tiles = new List<Tile>();

        #endregion

        #region Constructor

        public Layer()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            int count = Tiles.Count;
            for (int i = 0; i < count; i++)
            {
                Tiles[i]?.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                int count = Tiles.Count;
                for (int i = 0; i < count; i++)
                {
                    Tiles[i]?.Draw(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                int count = Tiles.Count;
                for (int i = 0; i < count; i++)
                {
                    Tiles[i]?.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Tile GetTile(long id)
        {
            int count = Tiles.Count;
            for (int i = 0; i < count; i++)
            {
                Tile existing = Tiles[i];
                if (existing != null)
                {
                    if (existing.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Tile GetTile(string name)
        {
            int count = Tiles.Count;
            for (int i = 0; i < count; i++)
            {
                Tile existing = Tiles[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Tile GetTile(Vector2 location)
        {
            //This only works if there's no gaps in the grid (every location has a Tile),
            //otherwise the indexing will be off
            int index = ((int)location.Y * Columns) + (int)location.X;
            if (index >= 0 && index < Tiles.Count)
            {
                return Tiles[index];
            }
            else
            {
                return null;
            }
        }

        public virtual Tile GetTile(Vector3 location)
        {
            int count = Tiles.Count;
            for (int i = 0; i < count; i++)
            {
                Tile existing = Tiles[i];
                if (existing != null)
                {
                    if (existing.Location.X == location.X &&
                        existing.Location.Y == location.Y &&
                        existing.Location.Z == location.Z)
                    {
                        return existing;
                    }
                } 
            }

            return null;
        }

        public virtual Tile GetTile(Rectangle region)
        {
            //This only works if the tiles are sorted by Y,X
            //Can use Sort_ForDrawing() method to pre-sort them by Y,X

            Tile result = null;

            int min = 0;
            int max = Tiles.Count - 1;

            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (mid < Tiles.Count)
                {
                    if (region.Y == Tiles[mid].Region.Y)
                    {
                        if (region.X == Tiles[mid].Region.X)
                        {
                            result = Tiles[mid];
                            break;
                        }
                        else if (region.X < Tiles[mid].Region.X)
                        {
                            max = mid - 1;
                        }
                        else
                        {
                            min = mid + 1;
                        }
                    }
                    else if (region.Y < Tiles[mid].Region.Y)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        public virtual void Sort_ForDrawing()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                for (int j = 0; j < Tiles.Count - 1; j++)
                {
                    if (Tiles[j].Region.Y > Tiles[j + 1].Region.Y)
                    {
                        Tile temp = Tiles[j + 1];
                        Tiles[j + 1] = Tiles[j];
                        Tiles[j] = temp;
                    }
                    else if (Tiles[j].Region.Y == Tiles[j + 1].Region.Y &&
                             Tiles[j].Region.X > Tiles[j + 1].Region.X)
                    {
                        Tile temp = Tiles[j + 1];
                        Tiles[j + 1] = Tiles[j];
                        Tiles[j] = temp;
                    }
                }
            }
        }

        public override void Dispose()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
