using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class Map : Something
    {
        #region Variables

        public long WorldID;
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
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layers[i]?.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                int count = Layers.Count;
                for (int i = 0; i < count; i++)
                {
                    Layers[i]?.Draw(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                int count = Layers.Count;
                for (int i = 0; i < count; i++)
                {
                    Layers[i]?.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Layer GetLayer(long id)
        {
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layer existing = Layers[i];
                if (existing != null)
                {
                    if (existing.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Layer GetLayer(string name)
        {
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layer existing = Layers[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        return existing;
                    }
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
