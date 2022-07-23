﻿using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public class Picture : Something
    {
        #region Variables

        public float Fade;
        public string HoverText;
        public float Opacity;

        #endregion

        #region Constructors

        public Picture()
        {
            Location = default;
            Image = default;
            Region = default;
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
                if (Texture != null)
                {
                    spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor * Opacity);
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
