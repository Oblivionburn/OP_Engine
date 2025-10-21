using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public class Label : Picture
    {
        #region Variables

        List<LabelPart> Parts = new List<LabelPart>();

        public SpriteFont Font;
        
        public string Text;
        public Color TextColor;

        public Alignment Alignment_Verticle;
        public Alignment Alignment_Horizontal;

        public bool AutoScale = true;
        public Vector2 Size;
        public Vector2 Position;
        public float Scale;
        public float Margin = 10;

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
            if (Parts.Count > 0)
            {
                Size = Font.MeasureString(" ");

                float xScale = Region.Width / Size.X;
                float yScale = Region.Height / Size.Y;
                float scale = Math.Min(xScale, yScale) - 0.10f;

                if (AutoScale)
                {
                    Scale = scale;
                }

                int strWidth = (int)Math.Round(Size.X * scale);
                int strHeight = (int)Math.Round(Size.Y * scale);

                for (int i = 0; i < Parts.Count; i++)
                {
                    if (Alignment_Verticle == Alignment.Top)
                    {
                        Position.Y = Region.Y + Margin;
                    }
                    else if (Alignment_Verticle == Alignment.Center)
                    {
                        Position.Y = Region.Y + (Region.Height / 2) - (strHeight / 2);
                    }
                    else if (Alignment_Verticle == Alignment.Bottom)
                    {
                        Position.Y = Region.Y + Region.Height - strHeight - Margin;
                    }

                    if (Alignment_Horizontal == Alignment.Left)
                    {
                        Position.X = Region.X + Margin + (Size.X * i);
                    }
                    else if (Alignment_Horizontal == Alignment.Center)
                    {
                        Position.X = Region.X + (Region.Width / 2) - (strWidth / 2) + (Size.X * i);
                    }
                    else if (Alignment_Horizontal == Alignment.Right)
                    {
                        Position.X = Region.X + Region.Width - strWidth - Margin - (Size.X * i);
                    }

                    Parts[i].Region = new Region(Position.X, Position.Y, Size.X, Size.Y);
                }
            }
            else if (!string.IsNullOrEmpty(Text) &&
                     Font != null &&
                     Region != null)
            {
                Size = Font.MeasureString(Text);

                float xScale = Region.Width / Size.X;
                float yScale = Region.Height / Size.Y;
                float scale = Math.Min(xScale, yScale) - 0.10f;

                if (AutoScale)
                {
                    Scale = scale;
                }

                int strWidth = (int)Math.Round(Size.X * scale);
                int strHeight = (int)Math.Round(Size.Y * scale);

                if (Alignment_Verticle == Alignment.Top)
                {
                    Position.Y = Region.Y + Margin;
                }
                else if (Alignment_Verticle == Alignment.Center)
                {
                    Position.Y = Region.Y + (Region.Height / 2) - (strHeight / 2);
                }
                else if (Alignment_Verticle == Alignment.Bottom)
                {
                    Position.Y = Region.Y + Region.Height - strHeight - Margin;
                }

                if (Alignment_Horizontal == Alignment.Left)
                {
                    Position.X = Region.X + Margin;
                }
                else if (Alignment_Horizontal == Alignment.Center)
                {
                    Position.X = Region.X + (Region.Width / 2) - (strWidth / 2);
                }
                else if (Alignment_Horizontal == Alignment.Right)
                {
                    Position.X = Region.X + Region.Width - strWidth - Margin;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                if (Parts.Count > 0)
                {
                    for (int i = 0; i < Parts.Count; i++)
                    {
                        Parts[i].Draw(spriteBatch);
                    }
                }
                else if (!string.IsNullOrEmpty(Text) &&
                         Font != null)
                {
                    spriteBatch.DrawString(Font, Text, Position, TextColor * Opacity, 0f, default, Scale, SpriteEffects.None, 0f);
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
