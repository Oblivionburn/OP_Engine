using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public struct LabelOptions
    {
        public SpriteFont font;
        public long id;
        public string name;
        public string text;
        public Color text_color;
        public Texture2D texture;
        public Region region;
        public Color draw_color;
        public bool visible;
    }
}
