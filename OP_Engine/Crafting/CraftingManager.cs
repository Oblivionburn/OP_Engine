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
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static Recipe GetRecipe(string name)
        {
            int count = Recipes.Count;
            for (int i = 0; i < count; i++)
            {
                Recipe existing = Recipes[i];
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

        public static Recipe GetRecipe(long id)
        {
            int count = Recipes.Count;
            for (int i = 0; i < count; i++)
            {
                Recipe existing = Recipes[i];
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

        public virtual List<Recipe> GetRecipes(string type, List<string> categories)
        {
            List<Recipe> recipes = new List<Recipe>();

            int count = Recipes.Count;
            for (int i = 0; i < count; i++)
            {
                Recipe existing = Recipes[i];
                if (existing != null)
                {
                    bool type_found = true;
                    if (!string.IsNullOrEmpty(type) &&
                        existing.Type != type)
                    {
                        type_found = false;
                    }

                    bool categories_found = true;
                    if (categories != null)
                    {
                        int categoryCount = categories.Count;
                        for (int c = 0; c < categoryCount; c++)
                        {
                            string category = categories[c];
                            if (!existing.Categories.Contains(category))
                            {
                                categories_found = false;
                                break;
                            }
                        }
                    }

                    if (type_found &&
                        categories_found)
                    {
                        recipes.Add(existing);
                    }
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
