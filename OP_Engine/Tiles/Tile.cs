using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Enums;
using OP_Engine.Inventories;
using OP_Engine.Utility;
using Region = OP_Engine.Utility.Region;
using ProgressBar = OP_Engine.Controls.ProgressBar;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace OP_Engine.Tiles
{
    public class Tile : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Type;
        public float Value;
        public Direction Direction;
        public float Duration;
        public TimeSpan Time;
        public Dimension2 Dimensions;
        public Location? Location;

        public World? World;
        public Map? Map;
        public Layer? Layer;

        public List<string> Tags = [];

        public bool InView;
        public bool InSight;
        public bool BlocksMovement;
        public bool BlocksSight;
        public bool CanMove;
        public bool CanUse;
        public bool IsTall;
        public bool Animated;

        public bool IsLightSource;
        public Color LightColor;

        public string? Sound;
        public int SoundRange;

        public Inventory? Inventory;
        public ProgressBar? ProgressBar;
        public Effect? Shader;

        public Region? Region;
        public Texture2D? Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        #endregion

        #region Constructor

        public Tile()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Animated)
            {
                Animate();
            }
        }

        public virtual void Update(Point resolution)
        {
            InView = false;

            if (Texture != null &&
                Region != null)
            {
                if (Visible)
                {
                    float x = Region.X;
                    float width = Region.Width;

                    if (x >= 0 - width - 1)
                    {
                        if (x < resolution.X + width + 1)
                        {
                            float y = Region.Y;
                            float height = Region.Height;

                            if (y >= 0 - height - 1)
                            {
                                if (y < resolution.Y + height + 1)
                                {
                                    InView = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Texture != null &&
                Region != null)
            {
                if (Visible)
                {
                    if (DrawColor != new Color(0, 0, 0, 0))
                    {
                        spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                    }
                    else
                    {
                        spriteBatch.Draw(Texture, Region.ToRectangle, Image, Color.White);
                    }

                    if (Shader != null)
                    {
                        Shader.CurrentTechnique.Passes[0].Apply();
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Texture != null &&
                Region != null)
            {
                if (Visible)
                {
                    Shader?.CurrentTechnique.Passes[0].Apply();

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

        public virtual void Animate()
        {
            if (Texture != null)
            {
                int X = Image.X + Image.Height;
                if (X >= Texture.Width)
                {
                    X = 0;
                }

                Image = new Rectangle(X, Image.Y, Image.Width, Image.Height);
            }
        }

        public virtual void Dispose()
        {
            World = null;
            Map = null;
            Layer = null;

            Location?.Dispose();
            Inventory?.Dispose();
            ProgressBar?.Dispose();

            Shader = null;
            Region = null;
            Texture = null;
        }

        #endregion
    }
}
