using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Controls
{
    public struct ProgressBarOptions
    {
        public long id;
        public string name;
        public int max;
        public int value;
        public int increment;
        public Texture2D base_texture;
        public Texture2D bar_texture;
        public Region region;
        public Color draw_color;
        public bool visible;
    }
}
