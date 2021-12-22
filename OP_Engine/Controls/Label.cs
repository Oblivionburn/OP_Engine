using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class Label : Picture
    {
        #region Variables

        public string Text;
        public Color TextColor;
        public Color SelectedTextColor;

        public Vector2 Size;
        public Vector2 Position;
        public float Scale;

        public SpriteFont Font;

        #endregion

        #region Constructor

        public Label() : base()
        {
            Size = default;
            Position = default;
            Opacity = 1;
        }

        #endregion

        #region Methods

        public override void Update()
        {
            if (!string.IsNullOrEmpty(Text) &&
                Font != null)
            {
                Size = Font.MeasureString(Text);

                float xScale = Region.Width / Size.X;
                float yScale = Region.Height / Size.Y;
                Scale = Math.Min(xScale, yScale) - 0.10f;

                int strWidth = (int)Math.Round(Size.X * Scale);
                int strHeight = (int)Math.Round(Size.Y * Scale);

                Position.X = (Region.Width - strWidth) / 2 + Region.X;
                Position.Y = (Region.Height - strHeight) / 2 + Region.Y + 2.5f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible &&
                !string.IsNullOrEmpty(Text) &&
                Font != null)
            {
                spriteBatch.DrawString(Font, Text, Position, TextColor * Opacity, 0f, default, Scale, SpriteEffects.None, 0f);
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
