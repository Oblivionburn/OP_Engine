using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Enums;
using OP_Engine.Utility;
using Region = OP_Engine.Utility.Region;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace OP_Engine.Tiles
{
    public class Map : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Type;
        public bool Visible;

        public World? World;
        public List<Layer> Layers = [];

        public int Depth;
        public Direction Direction;
        public Location? Location;

        public Texture2D? Texture;
        public Rectangle Image;
        public Region? Region;
        public Color DrawColor;

        public Effect? Shader;

        #endregion

        #region Constructor

        public Map()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layers[i]?.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Texture != null)
            {
                if (Visible)
                {
                    Shader?.CurrentTechnique.Passes[0].Apply();

                    if (Region != null)
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
            if (Texture != null)
            {
                if (Visible)
                {
                    Shader?.CurrentTechnique.Passes[0].Apply();

                    if (Region != null)
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

        public virtual void Draw_Layers(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                Shader?.CurrentTechnique.Passes[0].Apply();

                int count = Layers.Count;
                for (int i = 0; i < count; i++)
                {
                    Layers[i].Draw(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw_Layers(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                Shader?.CurrentTechnique.Passes[0].Apply();

                int count = Layers.Count;
                for (int i = 0; i < count; i++)
                {
                    Layers[i].Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Layer? GetLayer(long id)
        {
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layer existing = Layers[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Layer? GetLayer(string name)
        {
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layer existing = Layers[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual void Dispose()
        {
            foreach (Layer layer in Layers)
            {
                layer.Dispose();
            }

            Location?.Dispose();
            Shader?.Dispose();
        }

        #endregion
    }
}
