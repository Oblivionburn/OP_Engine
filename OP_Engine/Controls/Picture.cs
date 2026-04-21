using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public class Picture : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public Location Location;

        public float Fade;
        public string HoverText;
        public float Opacity;

        public float Value;
        public float Min_Value;
        public float Max_Value;
        public float Rate;

        public Region Region;
        public Texture2D Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public Effect Shader;

        #endregion

        #region Constructors

        public Picture()
        {
            Location = new Location();
            Region = new Region();
            Image = new Rectangle();
            Opacity = 1;
        }

        #endregion

        #region Methods

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Texture != null &&
                    Region != null)
                {
                    spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor * Opacity);

                    if (Shader != null)
                    {
                        Shader.CurrentTechnique.Passes[0].Apply();
                    } 
                }
            }
        }

        public virtual void Dispose()
        {
            Shader?.Dispose();
        }

        #endregion
    }
}
