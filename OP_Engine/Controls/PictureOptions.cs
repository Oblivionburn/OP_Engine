using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public struct PictureOptions
    {
        public long id;
        public string name;
        public Texture2D texture;
        public Region region;
        public Color color;
        public bool visible;
    }
}
