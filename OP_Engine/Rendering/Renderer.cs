using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Rendering
{
    public class Renderer : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;

        public bool SetRenderTarget_BeforeDraw;
        public bool ClearGraphics_BeforeDraw;
        public bool DrawToRenderTarget;
        public bool ClearRenderTarget_AfterDraw;
        public Microsoft.Xna.Framework.Color GraphicsClearColor;
        public GraphicsDevice? GraphicsDevice;
        public RenderTarget2D? RenderTarget;
        public BlendState BlendState = new();

        #endregion

        #region Constructors

        public Renderer()
        {
            
        }

        public Renderer(long id, string name) : this()
        {
            ID = id;
            Name = name;
        }

        #endregion

        #region Methods

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Microsoft.Xna.Framework.Point resolution)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new(GraphicsDevice, resolution.X, resolution.Y);
        }

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Microsoft.Xna.Framework.Point resolution, Microsoft.Xna.Framework.Color graphicsClearColor)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new(GraphicsDevice, resolution.X, resolution.Y);
            GraphicsClearColor = graphicsClearColor;
        }

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Microsoft.Xna.Framework.Point resolution, Microsoft.Xna.Framework.Color graphicsClearColor, BlendFunction alphaBlendFunction, Blend alphaSourceBlend, Blend alphaDestinationBlend, BlendFunction colorBlendFunction, Blend colorSourceBlend, Blend colorDestinationBlend)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new(GraphicsDevice, resolution.X, resolution.Y);
            GraphicsClearColor = graphicsClearColor;

            BlendState.AlphaBlendFunction = alphaBlendFunction;
            BlendState.AlphaSourceBlend = alphaSourceBlend;
            BlendState.AlphaDestinationBlend = alphaDestinationBlend;
            BlendState.ColorBlendFunction = colorBlendFunction;
            BlendState.ColorSourceBlend = colorSourceBlend;
            BlendState.ColorDestinationBlend = colorDestinationBlend;
        }

        public virtual void Update()
        {
            //Where you would handle lighting calculations
        }

        public virtual void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Point resolution)
        {
            if (SetRenderTarget_BeforeDraw)
            {
                GraphicsDevice?.SetRenderTarget(RenderTarget);
            }

            if (ClearGraphics_BeforeDraw)
            {
                GraphicsDevice?.Clear(GraphicsClearColor);
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, blendState: BlendState);

            CustomDraw(spriteBatch);

            if (DrawToRenderTarget)
            {
                spriteBatch.Draw(RenderTarget, new Microsoft.Xna.Framework.Rectangle(0, 0, resolution.X, resolution.Y), Microsoft.Xna.Framework.Color.White);
            }

            spriteBatch.End();

            if (ClearRenderTarget_AfterDraw)
            {
                GraphicsDevice?.SetRenderTarget(null);
            }
        }

        public virtual void CustomDraw(SpriteBatch spriteBatch)
        {
            //Where you would handle custom drawing to spritebatch
        }

        public virtual void Dispose()
        {
            GraphicsDevice = null;
            RenderTarget = null;
            BlendState?.Dispose();
        }

        #endregion
    }
}
