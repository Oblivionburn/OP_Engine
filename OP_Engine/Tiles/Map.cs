using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Enums;
using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class Map : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Type;
        public bool Visible;

        public World World;
        public List<Layer> Layers;

        public int Depth;
        public Direction Direction;
        public Location Location;

        public Texture2D Texture;
        public Rectangle Image;
        public Region Region;
        public Color DrawColor;

        public Effect Shader;

        #endregion

        #region Constructor

        public Map()
        {
            Layers = new List<Layer>();
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

                if (Shader != null)
                {
                    Shader.CurrentTechnique.Passes[0].Apply();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Shader != null)
                {
                    Shader.CurrentTechnique.Passes[0].Apply();
                }

                int count = Layers.Count;
                for (int i = 0; i < count; i++)
                {
                    Layers[i]?.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual void AddLayer(Layer layer)
        {
            layer.World = World;
            layer.Map = this;
            Layers.Add(layer);
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

        public virtual void Dispose()
        {
            foreach (Layer layer in Layers)
            {
                layer.Dispose();
            }

            Location?.Dispose();
            Shader?.Dispose();
        }

        #endregion
    }
}
