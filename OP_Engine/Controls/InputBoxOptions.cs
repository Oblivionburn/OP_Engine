using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public struct InputBoxOptions
    {
        public SpriteFont font;
        public long id;
        public int max_length;
        public string name;
        public string text;
        public Color text_color;
        public Color draw_color;
        public Texture2D texture;
        public Region region;
        public float opacity;
        public bool active;
        public bool visible;
    }
}
