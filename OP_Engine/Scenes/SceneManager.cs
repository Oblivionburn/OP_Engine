using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Scenes
{
    public class SceneManager : GameComponent
    {
        #region Variables

        public static List<Scene> Scenes = new List<Scene>();

        #endregion

        #region Constructor

        public SceneManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void Update()
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Visible)
                {
                    scene.Update();
                }
            }
        }

        public static void Update(Game gameRef, ContentManager content)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Visible)
                {
                    scene.Update(gameRef, content);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Visible)
                {
                    scene.Draw(spriteBatch, resolution);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Visible)
                {
                    scene.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public static void ChangeScene(string name)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Name == name)
                {
                    scene.Visible = true;
                }
                else
                {
                    scene.Visible = false;
                }
            }
        }

        public static void ChangeScene(long id)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.ID == id)
                {
                    scene.Visible = true;
                }
                else
                {
                    scene.Visible = false;
                }
            }
        }

        public static void ChangeScene(Scene scene)
        {
            foreach (Scene existing in Scenes)
            {
                if (existing.ID == scene.ID)
                {
                    existing.Visible = true;
                }
                else
                {
                    existing.Visible = false;
                }
            }
        }

        public static Scene GetScene(string name)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.Name == name)
                {
                    return scene;
                }
            }

            return null;
        }

        public static Scene GetScene(long id)
        {
            foreach (Scene scene in Scenes)
            {
                if (scene.ID == id)
                {
                    return scene;
                }
            }

            return null;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Scene scene in Scenes)
            {
                scene.Dispose();
            }
        }

        #endregion
    }
}
