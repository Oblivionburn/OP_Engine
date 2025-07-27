using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public class LabelPart
    {
        #region Variables

        public SpriteFont Font;
        public string Text;
        public float Size = 1;

        public Texture2D Texture;
        public Rectangle Rectangle;
        public Rectangle Image;

        public Color Color = Color.White;
        public Region Region;

        #endregion

        #region Constructor

        public LabelPart()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text) &&
                Font != null)
            {
                spriteBatch.DrawString(Font, Text, new Vector2(Region.X, Region.Y), Color, 0f, default, Size, SpriteEffects.None, 0f);
            }
            else if (Texture != null)
            {
                spriteBatch.Draw(Texture, Region.ToRectangle, Image, Color);
            }
        }

        #endregion
    }
}
