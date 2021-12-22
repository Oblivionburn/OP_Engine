using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Menus
{
    public class MenuManager : GameComponent
    {
        #region Variables

        public static int CurrentMenu_ID;
        public static int PreviousMenu_ID;

        public static List<Menu> Menus = new List<Menu>();

        #endregion

        #region Constructor

        public MenuManager(Game game) : base(game)
        {
            
        }

        #endregion

        #region Methods

        public static void Update()
        {
            foreach (Menu existing in Menus)
            {
                if (existing.Visible ||
                    existing.Active)
                {
                    existing.Update();
                }
            }
        }

        public static void Update(Game gameRef, ContentManager content)
        {
            foreach (Menu existing in Menus)
            {
                if (existing.Visible ||
                    existing.Active)
                {
                    existing.Update(gameRef, content);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Menu existing in Menus)
            {
                if (existing.Visible)
                {
                    existing.Draw(spriteBatch);
                }
            }
        }

        public static Menu GetMenu(string name)
        {
            foreach (Menu menu in Menus)
            {
                if (menu.Name == name)
                {
                    return menu;
                }
            }

            return null;
        }

        public static Menu GetMenu(int id)
        {
            foreach (Menu menu in Menus)
            {
                if (menu.ID == id)
                {
                    return menu;
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

        public static void ChangeMenu(int id)
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

        public static void ChangeMenu(int id, bool stay_active, bool stay_visible)
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

        public static void DisposeMenus()
        {
            foreach (Menu menu in Menus)
            {
                menu.Dispose();
            }
        }

        #endregion
    }
}
