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

        public static List<Menu> Menus;
        public static List<Menu> PreviousMenus;

        #endregion

        #region Properties

        public static Menu CurrentMenu
        {
            get
            {
                return GetCurrentMenu();
            }
        }

        public static Menu PreviousMenu
        {
            get
            {
                return GetPreviousMenu();
            }
        }

        #endregion

        #region Events

        public static event EventHandler OnMenuChange;

        #endregion

        #region Constructor

        public MenuManager(Game game) : base(game)
        {
            Menus = new List<Menu>();
            PreviousMenus = new List<Menu>();

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

        public static Menu GetCurrentMenu()
        {
            return GetMenu(CurrentMenu_ID);
        }

        public static Menu GetPreviousMenu()
        {
            if (PreviousMenus.Count > 0)
            {
                return PreviousMenus[PreviousMenus.Count - 1];
            }

            return null;
        }

        public static void ChangeMenu(string name)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu(long id)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu(Menu menu)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu(string name, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu(long id, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu(Menu menu, bool stay_active, bool stay_visible)
        {
            Menu current_menu = GetCurrentMenu();
            if (current_menu != null)
            {
                PreviousMenus.Add(current_menu);

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

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        public static void ChangeMenu_Previous()
        {
            Menu new_menu = GetPreviousMenu();
            if (new_menu != null)
            {
                Menu current_menu = GetCurrentMenu();
                if (current_menu != null)
                {
                    current_menu.Active = false;
                    current_menu.Visible = false;
                }

                PreviousMenus.Remove(new_menu);

                CurrentMenu_ID = new_menu.ID;
                new_menu.Load();
                new_menu.Active = true;
                new_menu.Visible = true;
            }

            OnMenuChange?.Invoke(null, EventArgs.Empty);
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Menu menu in Menus)
            {
                menu.Dispose();
            }

            foreach (Menu menu in PreviousMenus)
            {
                menu.Dispose();
            }
        }

        #endregion
    }
}
