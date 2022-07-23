using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Crafting
{
    public class CraftingManager : GameComponent
    {
        #region Variables

        public static List<Recipe> Recipes = new List<Recipe>();

        #endregion

        #region Constructors

        public CraftingManager(Game game) : base(game)
        {

        }

        #endregion

        #region Methods

        public static Recipe GetRecipe(string name)
        {
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.Name == name)
                {
                    return recipe;
                }
            }

            return null;
        }

        public static Recipe GetRecipe(long id)
        {
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.ID == id)
                {
                    return recipe;
                }
            }

            return null;
        }

        public virtual List<Recipe> GetRecipes(string type, List<string> categories)
        {
            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe item in Recipes)
            {
                bool type_found = true;
                if (!string.IsNullOrEmpty(type) &&
                    item.Type != type)
                {
                    type_found = false;
                }

                bool categories_found = true;
                if (categories != null)
                {
                    foreach (string category in categories)
                    {
                        if (!item.Categories.Contains(category))
                        {
                            categories_found = false;
                            break;
                        }
                    }
                }

                if (type_found &&
                    categories_found)
                {
                    recipes.Add(item);
                }
            }

            return recipes;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Recipe recipe in Recipes)
            {
                recipe.Dispose();
            }
        }

        #endregion
    }
}
