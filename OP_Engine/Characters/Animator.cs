using System;
using Microsoft.Xna.Framework;
using OP_Engine.Utility;
using OP_Engine.Enums;

namespace OP_Engine.Characters
{
    public class Animator : IDisposable
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

        public virtual void Update(Something something)
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

        public virtual void Animate(Something something)
        {
            int X = something.Image.X + something.Image.Width;
            if (X >= something.Texture.Width)
            {
                X = 0;
            }

            something.Image = new Rectangle(X, something.Image.Y, something.Image.Width, something.Image.Height);
        }

        public virtual void Reset(Something something)
        {
            something.Image = new Rectangle(0, something.Image.Y, something.Image.Width, something.Image.Height);
        }

        public virtual void Dispose()
        {
            
        }

        #endregion
    }
}
