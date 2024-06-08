using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class InputBox : Button
    {
        #region Variables

        public int MaxLength;
        public string Caret;
        public int CarrotDelay;

        private bool delayed;
        private int delay;

        #endregion

        #region Constructor

        public InputBox() : base()
        {
            
        }

        #endregion

        #region Methods

        public override void Update()
        {
            base.Update();

            Caret = "";
            if (Active)
            {
                if (delayed)
                {
                    if (delay < CarrotDelay)
                    {
                        delay++;
                    }
                    else
                    {
                        delayed = false;
                    }
                }
                else
                {
                    if (delay > 0)
                    {
                        if (!string.IsNullOrEmpty(Text))
                        {
                            for (int i = 0; i < Text.Length; i++)
                            {
                                Caret += " ";
                            }
                        }
                        
                        Caret += "|";
                        delay--;
                    }
                    else
                    {
                        delayed = true;
                    }
                }
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                base.Draw(spriteBatch);

                if (Active &&
                    !string.IsNullOrEmpty(Caret))
                {
                    spriteBatch.DrawString(Font, Caret, Position, TextColor, 0f, default, Scale, SpriteEffects.None, 0f);
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