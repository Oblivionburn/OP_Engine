using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Rendering
{
    public class Renderer : Something
    {
        #region Variables

        public bool SetRenderTarget_BeforeDraw;
        public bool ClearGraphics_BeforeDraw;
        public bool DrawToRenderTarget;
        public bool ClearRenderTarget_AfterDraw;
        public Color GraphicsClearColor;
        public GraphicsDevice GraphicsDevice;
        public RenderTarget2D RenderTarget;
        public BlendState BlendState;

        #endregion

        #region Constructors

        public Renderer() : base()
        {
            BlendState = new BlendState();
        }

        public Renderer(long id, string name) : this()
        {
            ID = id;
            Name = name;
        }

        #endregion

        #region Methods

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Point resolution)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new RenderTarget2D(GraphicsDevice, resolution.X, resolution.Y);
        }

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Point resolution, Color graphicsClearColor)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new RenderTarget2D(GraphicsDevice, resolution.X, resolution.Y);
            GraphicsClearColor = graphicsClearColor;
        }

        public virtual void Init(GraphicsDeviceManager graphicsDevice, Point resolution, Color graphicsClearColor, BlendFunction alphaBlendFunction, Blend alphaSourceBlend, Blend alphaDestinationBlend, BlendFunction colorBlendFunction, Blend colorSourceBlend, Blend colorDestinationBlend)
        {
            GraphicsDevice = graphicsDevice.GraphicsDevice;
            RenderTarget = new RenderTarget2D(GraphicsDevice, resolution.X, resolution.Y);
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

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (SetRenderTarget_BeforeDraw)
            {
                GraphicsDevice.SetRenderTarget(RenderTarget);
            }

            if (ClearGraphics_BeforeDraw)
            {
                GraphicsDevice.Clear(GraphicsClearColor);
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, blendState: BlendState);

            CustomDraw(spriteBatch);

            if (DrawToRenderTarget)
            {
                spriteBatch.Draw(RenderTarget, new Rectangle(0, 0, resolution.X, resolution.Y), Color.White);
            }

            spriteBatch.End();

            if (ClearRenderTarget_AfterDraw)
            {
                GraphicsDevice.SetRenderTarget(null);
            }
        }

        public virtual void CustomDraw(SpriteBatch spriteBatch)
        {
            //Where you would handle custom drawing to spritebatch
        }

        public override void Dispose()
        {
            GraphicsDevice = null;
            RenderTarget = null;
            BlendState?.Dispose();

            base.Dispose();
        }

        #endregion
    }
}
