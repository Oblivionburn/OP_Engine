﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class Button : Label
    {
        #region Variables

        public bool Selected;
        public bool Enabled;

        public Color TextColor_Selected;
        public Color TextColor_Disabled;

        public Texture2D Texture_Highlight;
        public Texture2D Texture_Disabled;

        #endregion

        #region Constructor

        public Button() : base()
        {
            Enabled = true;
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Enabled)
                {
                    if (Selected)
                    {
                        if (Texture_Highlight != null)
                        {
                            spriteBatch.Draw(Texture_Highlight, Region, Image, DrawColor * Opacity);
                        }

                        if (!string.IsNullOrEmpty(Text) &&
                            Font != null)
                        {
                            spriteBatch.DrawString(Font, Text, Position, TextColor_Selected * Opacity, 0f, default, Scale, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        if (Texture != null)
                        {
                            spriteBatch.Draw(Texture, Region, Image, DrawColor * Opacity);
                        }

                        if (!string.IsNullOrEmpty(Text) &&
                            Font != null)
                        {
                            spriteBatch.DrawString(Font, Text, Position, TextColor * Opacity, 0f, default, Scale, SpriteEffects.None, 0f);
                        }
                    }
                }
                else if (Texture_Disabled != null)
                {
                    spriteBatch.Draw(Texture_Disabled, Region, Image, DrawColor * Opacity);

                    if (!string.IsNullOrEmpty(Text) &&
                        Font != null)
                    {
                        spriteBatch.DrawString(Font, Text, Position, TextColor_Disabled * Opacity, 0f, default, Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override void Dispose()
        {
            Font = null;
            base.Dispose();
        }

        #endregion
    }
}
