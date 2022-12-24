using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Menus;
using OP_Engine.Scenes;

namespace OP_Engine.Rendering
{
    public class RenderingManager : GameComponent
    {
        #region Variables

        public static bool UsingDefaults;
        public static Lighting Lighting;
        public static Renderer LightingRenderer;
        public static Renderer AddLightingRenderer;

        public static List<Renderer> Renderers = new List<Renderer>();

        #endregion

        #region Constructors

        public RenderingManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void InitDefaults(GraphicsDeviceManager graphicsManager, Point resolution)
        {
            Lighting = new Lighting();

            LightingRenderer = new Renderer();
            LightingRenderer.Name = "Lighting";
            LightingRenderer.Init(graphicsManager, resolution);
            LightingRenderer.SetRenderTarget_BeforeDraw = true;
            LightingRenderer.ClearGraphics_BeforeDraw = true;
            LightingRenderer.ClearRenderTarget_AfterDraw = true;
            LightingRenderer.BlendState = BlendState.Additive;

            AddLightingRenderer = new Renderer();
            AddLightingRenderer.Name = "Add Lighting to World";
            AddLightingRenderer.RenderTarget = LightingRenderer.RenderTarget;
            AddLightingRenderer.DrawToRenderTarget = true;
            AddLightingRenderer.BlendState = new BlendState
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.One,
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            };

            UsingDefaults = true;
        }

        public static void Update()
        {
            if (UsingDefaults)
            {
                LightingRenderer.Update();
            }

            int count = Renderers.Count;
            for (int i = 0; i < count; i++)
            {
                Renderers[i]?.Update();
            }
        }

        public static void Draw(GameWindow window, GraphicsDeviceManager graphicsManager, SpriteBatch spriteBatch, Point resolution)
        {
            if (window != null)
            {
                //Don't bother drawing if the window is minimized
                if (window.ClientBounds.Width > 0 &&
                    window.ClientBounds.Height > 0)
                {
                    if (spriteBatch != null)
                    {
                        if (UsingDefaults &&
                            graphicsManager != null)
                        {
                            /*
                                This is a very basic setup for rendering whatever's currently visible in SceneManager,
                                    applying deferred lighting on the scene, and then rendering menus ontop of that.

                                Can just not run InitDefaults() if you want to use your own custom rendering.
                            */

                            //Set ambient light in case the color changed
                            LightingRenderer.GraphicsClearColor = Lighting.DrawColor;

                            //Render lighting
                            LightingRenderer.Draw(spriteBatch, resolution);

                            //Render world
                            graphicsManager.GraphicsDevice.Clear(Color.Black);
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                            SceneManager.Draw_WorldsOnly(spriteBatch, resolution, Color.White);
                            spriteBatch.End();

                            //Add lighting to world
                            AddLightingRenderer.Draw(spriteBatch, resolution);

                            //Render menus
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                            SceneManager.Draw_WorldsOnly(spriteBatch, resolution); //Alt method with no lighting applied
                            SceneManager.Draw_MenusOnly(spriteBatch);
                            MenuManager.Draw(spriteBatch);
                            spriteBatch.End();
                        }

                        int count = Renderers.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Renderers[i].Draw(spriteBatch, resolution);
                        }
                    }
                }
            }
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            if (Lighting != null)
            {
                Lighting.Dispose();
            }

            if (LightingRenderer != null)
            {
                LightingRenderer.Dispose();
            }

            if (AddLightingRenderer != null)
            {
                AddLightingRenderer.Dispose();
            }

            foreach (Renderer renderer in Renderers)
            {
                renderer.Dispose();
            }
        }

        #endregion
    }
}
