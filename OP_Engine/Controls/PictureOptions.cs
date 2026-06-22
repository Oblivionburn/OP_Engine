using Microsoft.Xna.Framework.Graphics;
using Region = OP_Engine.Utility.Region;
using Color = Microsoft.Xna.Framework.Color;

namespace OP_Engine.Controls
{
    public struct PictureOptions
    {
        public long id;
        public string? name;
        public Texture2D? texture;
        public Region? region;
        public Color color;
        public bool visible;
    }
}
