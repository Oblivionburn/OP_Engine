using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Controls;
using OP_Engine.Inventories;
using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class Tile : Something
    {
        #region Variables

        public long WorldID;
        public long MapID;
        public long LayerID;

        public bool BlocksMovement;
        public bool IsTall;
        public bool Animated;

        public Inventory Inventory;
        public ProgressBar ProgressBar;

        /*
        Using this Square object for Region, instead of the Rectangle 
        struct, to enable referencing the same Region for all overlapping
        tiles of the same size across multiple Layers. This enables moving
        them all at the same time with a single modification to the Region
        at the lowest Layer. It's an efficiency thing.
        */
        public new Region Region;
        
        #endregion

        #region Constructor

        public Tile()
        {
            Inventory = new Inventory();

            Location = new Vector3();
            ProgressBar = new ProgressBar();
            Region = new Region();
            Image = new Rectangle();
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

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    if (Region.X >= Region.Width * -2 &&
                        Region.X < resolution.X + Region.Width * 2)
                    {
                        if (Region.Y >= Region.Height * -2 &&
                            Region.Y < resolution.Y + Region.Height * 2)
                        {
                            if (DrawColor != new Color(0, 0, 0, 0))
                            {
                                spriteBatch.Draw(Texture, new Rectangle(Region.X, Region.Y, Region.Width, Region.Height), Image, DrawColor);
                            }
                            else
                            {
                                spriteBatch.Draw(Texture, new Rectangle(Region.X, Region.Y, Region.Width, Region.Height), Image, Color.White);
                            }
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    if (Region.X >= Region.Width * -2 &&
                        Region.X < resolution.X + Region.Width * 2)
                    {
                        if (Region.Y >= Region.Height * -2 &&
                            Region.Y < resolution.Y + Region.Height * 2)
                        {
                            if (DrawColor != new Color(0, 0, 0, 0))
                            {
                                spriteBatch.Draw(Texture, new Rectangle(Region.X, Region.Y, Region.Width, Region.Height), Image, DrawColor);
                            }
                            else
                            {
                                spriteBatch.Draw(Texture, new Rectangle(Region.X, Region.Y, Region.Width, Region.Height), Image, color);
                            }
                        }
                    }
                }
            }
        }

        public virtual void Animate()
        {
            int X = Image.X + Image.Height;
            if (X >= Texture.Width)
            {
                X = 0;
            }

            Image = new Rectangle(X, Image.Y, Image.Width, Image.Height);
        }

        public override void Dispose()
        {
            ProgressBar.Dispose();

            if (Region != null)
            {
                Region.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
