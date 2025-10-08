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

        public Effect Shader;

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

                if (Shader != null)
                {
                    Shader.CurrentTechnique.Passes[0].Apply();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Shader != null)
                {
                    Shader.CurrentTechnique.Passes[0].Apply();
                }

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

        public virtual Tile GetTile(Point screen_point, Point tileSize)
        {
            //This only works if the tiles are sorted by Y,X
            //Can use Sort_ForDrawing() method to pre-sort them by Y,X

            int min_x = (int)Tiles[0].Region.X;
            int max_x = min_x + (tileSize.X * Columns);
            int min_y = (int)Tiles[0].Region.Y;
            int max_y = min_y + (tileSize.Y * Rows);

            int y_pos = 0;
            for (int y = min_y; y < max_y; y += tileSize.Y)
            {
                if (screen_point.Y == y)
                {
                    break;
                }

                y_pos++;
            }

            int x_pos = 0;
            for (int x = min_x; x < max_x; x += tileSize.X)
            {
                if (screen_point.X == x)
                {
                    break;
                }

                x_pos++;
            }

            return GetTile(new Vector2(x_pos, y_pos));
        }

        public virtual Tile WithinTile(Point screen_point, Point tileSize)
        {
            //This only works if the tiles are sorted by Y,X
            //Can use Sort_ForDrawing() method to pre-sort them by Y,X

            int min_x = (int)Tiles[0].Region.X;
            int max_x = min_x + (tileSize.X * Columns);
            int min_y = (int)Tiles[0].Region.Y;
            int max_y = min_y + (tileSize.Y * Rows);

            int y_pos = 0;
            for (int y = min_y; y < max_y; y += tileSize.Y)
            {
                if (screen_point.Y >= y &&
                    screen_point.Y < y + tileSize.Y)
                {
                    break;
                }

                y_pos++;
            }

            int x_pos = 0;
            for (int x = min_x; x < max_x; x += tileSize.X)
            {
                if (screen_point.X >= x &&
                    screen_point.X < x + tileSize.X)
                {
                    break;
                }

                x_pos++;
            }

            return GetTile(new Vector2(x_pos, y_pos));
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
            if (Shader != null)
            {
                Shader.Dispose();
            }

            foreach (Tile tile in Tiles)
            {
                tile.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
