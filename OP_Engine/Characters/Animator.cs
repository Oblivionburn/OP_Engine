using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Animator
    {
        #region Variables

        public string Animation;
        public int Frames;

        #endregion

        #region Constructor

        public Animator()
        {

        }

        #endregion

        #region Methods

        public virtual void Update(Character character)
        {
            
        }

        public virtual void FaceNorth(Character character)
        {
            character.Image = new Rectangle(character.Image.X, (character.Texture.Height / 4) * 3, character.Texture.Width / Frames, character.Texture.Height / 4);
            character.Direction = Direction.Up;
        }

        public virtual void FaceEast(Character character)
        {
            character.Image = new Rectangle(character.Image.X, (character.Texture.Height / 4) * 2, character.Texture.Width / Frames, character.Texture.Height / 4);
            character.Direction = Direction.Right;
        }

        public virtual void FaceSouth(Character character)
        {
            character.Image = new Rectangle(character.Image.X, 0, character.Texture.Width / Frames, character.Texture.Height / 4);
            character.Direction = Direction.Down;
        }

        public virtual void FaceWest(Character character)
        {
            character.Image = new Rectangle(character.Image.X, (character.Texture.Height / 4) * 1, character.Texture.Width / Frames, character.Texture.Height / 4);
            character.Direction = Direction.Left;
        }

        public virtual void Animate(Character character)
        {
            int X = character.Image.X + character.Image.Width;
            if (X >= character.Texture.Width)
            {
                X = 0;
            }

            character.Image = new Rectangle(X, character.Image.Y, character.Image.Width, character.Image.Height);
        }

        public virtual void Reset(Character character)
        {
            character.Image = new Rectangle(0, character.Image.Y, character.Image.Width, character.Image.Height);
        }

        #endregion
    }
}
