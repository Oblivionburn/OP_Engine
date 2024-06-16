using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public struct ButtonOptions
    {
        public long id;
        public string name;
        public string text;
        public string hover_text;
        public SpriteFont font;
        public Color text_color;
        public Color text_selected_color;
        public Color text_disabled_color;
        public Color draw_color_selected;
        public Color draw_color_disabled;
        public Texture2D texture;
        public Texture2D texture_highlight;
        public Texture2D texture_disabled;
        public Region region;
        public Color draw_color;
        public bool enabled;
        public bool selected;
        public bool visible;
    }
}
