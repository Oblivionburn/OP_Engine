using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Enums;
using Region = OP_Engine.Utility.Region;
using Color = Microsoft.Xna.Framework.Color;

namespace OP_Engine.Controls
{
    public struct LabelOptions
    {
        public long id;
        public SpriteFont font;
        public string name;
        public string text;
        public Color text_color;
        public Alignment alignment_verticle;
        public Alignment alignment_horizontal;
        public Texture2D texture;
        public Region region;
        public Color draw_color;
        public float opacity;
        public bool visible;
    }
}
