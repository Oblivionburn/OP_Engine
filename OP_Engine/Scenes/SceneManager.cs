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

        public static long CurrentScene_ID;
        public static long PreviousScene_ID;

        public static List<Scene> Scenes = new List<Scene>();

        #endregion

        #region Properties

        public static Scene CurrentScene
        {
            get
            {
                return GetCurrentScene();
            }
        }

        public static Scene PreviousScene
        {
            get
            {
                return GetPreviousScene();
            }
        }

        #endregion

        #region Events

        public static event EventHandler OnSceneChange;

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
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scenes[i]?.Update();
            }
        }

        public static void Update(Game gameRef, ContentManager content)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scenes[i]?.Update(gameRef, content);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scene existing = Scenes[i];
                if (existing == null)
                {
                    existing.DrawWorld(spriteBatch, resolution);
                    existing.DrawMenu(spriteBatch);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scene existing = Scenes[i];
                if (existing == null)
                {
                    existing.DrawWorld(spriteBatch, resolution, color);
                    existing.DrawMenu(spriteBatch);
                }
            }
        }

        public static void Draw_WorldsOnly(SpriteBatch spriteBatch, Point resolution)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scenes[i]?.DrawWorld(spriteBatch, resolution);
            }
        }

        public static void Draw_WorldsOnly(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scenes[i]?.DrawWorld(spriteBatch, resolution, color);
            }
        }

        public static void Draw_MenusOnly(SpriteBatch spriteBatch)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scenes[i]?.DrawMenu(spriteBatch);
            }
        }

        public static Scene GetScene(string name)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scene existing = Scenes[i];
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

        public static Scene GetScene(long id)
        {
            int count = Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                Scene existing = Scenes[i];
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

        public static Scene GetCurrentScene()
        {
            return GetScene(CurrentScene_ID);
        }

        public static Scene GetPreviousScene()
        {
            return GetScene(PreviousScene_ID);
        }

        public static void ChangeScene(string name)
        {
            Scene current_scene = GetCurrentScene();
            if (current_scene != null)
            {
                PreviousScene_ID = CurrentScene_ID;

                current_scene.Visible = false;
            }

            Scene new_scene = GetScene(name);
            if (new_scene != null)
            {
                CurrentScene_ID = new_scene.ID;

                new_scene.Visible = true;
            }

            OnSceneChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeScene(long id)
        {
            Scene current_scene = GetCurrentScene();
            if (current_scene != null)
            {
                PreviousScene_ID = CurrentScene_ID;

                current_scene.Visible = false;
            }

            Scene new_scene = GetScene(id);
            if (new_scene != null)
            {
                CurrentScene_ID = new_scene.ID;

                new_scene.Visible = true;
            }

            OnSceneChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeScene(Scene scene)
        {
            Scene current_scene = GetCurrentScene();
            if (current_scene != null)
            {
                PreviousScene_ID = CurrentScene_ID;

                current_scene.Visible = false;
            }

            if (scene != null)
            {
                CurrentScene_ID = scene.ID;

                scene.Visible = true;
            }

            OnSceneChange?.Invoke(null, EventArgs.Empty);
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
