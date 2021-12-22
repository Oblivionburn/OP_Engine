using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class Map : Something
    {
        #region Variables

        public int WorldID;
        public int Depth;

        public List<Layer> Layers = new List<Layer>();

        #endregion

        #region Constructor

        public Map()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            foreach (Layer layer in Layers)
            {
                layer.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                foreach (Layer layer in Layers)
                {
                    layer.Draw(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                foreach (Layer layer in Layers)
                {
                    layer.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Layer GetLayer(int id)
        {
            foreach (Layer layer in Layers)
            {
                if (layer.ID == id)
                {
                    return layer;
                }
            }

            return null;
        }

        public virtual Layer GetLayer(string name)
        {
            foreach (Layer layer in Layers)
            {
                if (layer.Name == name)
                {
                    return layer;
                }
            }

            return null;
        }

        public override void Dispose()
        {
            foreach (Layer layer in Layers)
            {
                layer.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
