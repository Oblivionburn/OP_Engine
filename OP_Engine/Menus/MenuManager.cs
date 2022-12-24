using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Menus
{
    public class MenuManager : GameComponent
    {
        #region Variables

        public static long CurrentMenu_ID;
        public static long PreviousMenu_ID;

        public static List<Menu> Menus = new List<Menu>();

        #endregion

        #region Constructor

        public MenuManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void Update()
        {
            int count = Menus.Count;
            for (int i = 0; i < count; i++)
            {
                Menus[i]?.Update();
            }
        }

        public static void Update(Game gameRef, ContentManager content)
        {
            int count = Menus.Count;
            for (int i = 0; i < count; i++)
            {
                Menus[i]?.Update(gameRef, content);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            int count = Menus.Count;
            for (int i = 0; i < count; i++)
            {
                Menus[i]?.Draw(spriteBatch);
            }
        }

        public static Menu GetMenu(string name)
        {
            int count = Menus.Count;
            for (int i = 0; i < count; i++)
            {
                Menu existing = Menus[i];
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

        public static Menu GetMenu(long id)
        {
            int count = Menus.Count;
            for (int i = 0; i < count; i++)
            {
                Menu existing = Menus[i];
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

        public static void ChangeMenu(string name)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = false;
                current_menu.Visible = false;
            }

            Menu new_menu = GetMenu(name);
            if (new_menu != null)
            {
                CurrentMenu_ID = new_menu.ID;
                new_menu.Load();
                new_menu.Active = true;
                new_menu.Visible = true;
            }
        }

        public static void ChangeMenu(long id)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = false;
                current_menu.Visible = false;
            }

            Menu new_menu = GetMenu(id);
            if (new_menu != null)
            {
                CurrentMenu_ID = new_menu.ID;
                new_menu.Load();
                new_menu.Active = true;
                new_menu.Visible = true;
            }
        }

        public static void ChangeMenu(Menu menu)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = false;
                current_menu.Visible = false;
            }

            if (menu != null)
            {
                CurrentMenu_ID = menu.ID;
                menu.Load();
                menu.Active = true;
                menu.Visible = true;
            }
        }

        public static void ChangeMenu(string name, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = stay_active;
                current_menu.Visible = stay_visible;
            }

            Menu new_menu = GetMenu(name);
            if (new_menu != null)
            {
                CurrentMenu_ID = new_menu.ID;
                new_menu.Load();
                new_menu.Active = true;
                new_menu.Visible = true;
            }
        }

        public static void ChangeMenu(long id, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = stay_active;
                current_menu.Visible = stay_visible;
            }

            Menu new_menu = GetMenu(id);
            if (new_menu != null)
            {
                CurrentMenu_ID = new_menu.ID;
                new_menu.Load();
                new_menu.Active = true;
                new_menu.Visible = true;
            }
        }

        public static void ChangeMenu(Menu menu, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetMenu(CurrentMenu_ID);
            if (current_menu != null)
            {
                PreviousMenu_ID = CurrentMenu_ID;

                current_menu.Active = stay_active;
                current_menu.Visible = stay_visible;
            }

            if (menu != null)
            {
                CurrentMenu_ID = menu.ID;
                menu.Load();
                menu.Active = true;
                menu.Visible = true;
            }
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Menu menu in Menus)
            {
                menu.Dispose();
            }
        }

        #endregion
    }
}
