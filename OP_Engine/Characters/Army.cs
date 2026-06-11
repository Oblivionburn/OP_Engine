using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;
using OP_Engine.Enums;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;
using Region = OP_Engine.Utility.Region;

namespace OP_Engine.Characters
{
    public class Army : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Type;

        public Region? Region;
        public Texture2D? Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public Direction Direction;
        public Location? Location;
        public Location? Destination;

        public List<Squad> Squads = [];

        #endregion

        #region Constructor

        public Army()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible &&
                Texture != null &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, Color.White);
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible &&
                Texture != null &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, color);
                        }
                    }
                }
            }
        }

        public virtual Squad? GetSquad(long id)
        {
            int count = Squads.Count;
            for (int i = 0; i < count; i++)
            {
                Squad existing = Squads[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Squad? GetSquad(string name)
        {
            int count = Squads.Count;
            for (int i = 0; i < count; i++)
            {
                Squad existing = Squads[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual void Dispose()
        {
            foreach (Squad squad in Squads)
            {
                squad.Dispose();
            }
        }

        #endregion
    }
}
