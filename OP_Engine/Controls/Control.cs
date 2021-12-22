using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class Control : Picture
    {
        #region Variables

        public bool Selected;
        public bool Enabled;

        public Texture2D Texture_Highlight;
        public Texture2D Texture_Disabled;

        #endregion

        #region Constructors

        public Control()
        {
            Enabled = true;
            Opacity = 1;
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Enabled)
                {
                    if (Selected &&
                        Texture_Highlight != null)
                    {
                        spriteBatch.Draw(Texture_Highlight, Region, Image, DrawColor * Opacity);
                    }
                    else if (Texture != null)
                    {
                        spriteBatch.Draw(Texture, Region, Image, DrawColor * Opacity);
                    }
                }
                else if (Texture_Disabled != null)
                {
                    spriteBatch.Draw(Texture_Disabled, Region, Image, DrawColor * Opacity);
                }
            }
        }

        public override void Dispose()
        {
            Texture_Highlight = null;
            Texture_Disabled = null;

            base.Dispose();
        }

        #endregion
    }
}
