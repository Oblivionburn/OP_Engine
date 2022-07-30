using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public class Label : Picture
    {
        #region Variables

        public string Text;
        public Color TextColor;

        public Vector2 Size;
        public Vector2 Position;
        public Alignment Alignment_Verticle;
        public Alignment Alignment_Horizontal;
        public float Scale;

        public SpriteFont Font;

        #endregion

        #region Constructor

        public Label() : base()
        {
            Size = default;
            Position = default;
        }

        #endregion

        #region Methods

        public override void Update()
        {
            if (!string.IsNullOrEmpty(Text) &&
                Font != null &&
                Region != null)
            {
                Size = Font.MeasureString(Text);

                float xScale = Region.Width / Size.X;
                float yScale = Region.Height / Size.Y;
                Scale = Math.Min(xScale, yScale) - 0.10f;

                int strWidth = (int)Math.Round(Size.X * Scale);
                int strHeight = (int)Math.Round(Size.Y * Scale);

                if (Alignment_Verticle == Alignment.Top)
                {
                    Position.Y = Region.Y + 8;
                }
                else if (Alignment_Verticle == Alignment.Center)
                {
                    Position.Y = Region.Y + (Region.Height / 2) - (strHeight / 2);
                }
                else if (Alignment_Verticle == Alignment.Bottom)
                {
                    Position.Y = Region.Y + Region.Height - strHeight - 8;
                }

                if (Alignment_Horizontal == Alignment.Left)
                {
                    Position.X = Region.X + 8;
                }
                else if (Alignment_Horizontal == Alignment.Center)
                {
                    Position.X = Region.X + (Region.Width / 2) - (strWidth / 2);
                }
                else if (Alignment_Horizontal == Alignment.Right)
                {
                    Position.X = Region.X + Region.Width - strWidth - 8;
                }
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
