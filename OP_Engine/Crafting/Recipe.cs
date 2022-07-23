using System;
using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Crafting
{
    public class Recipe : Something
    {
        #region Variables

        public TimeSpan CraftingTime;
        public List<string> Categories = new List<string>();
        public Dictionary<string, int> Ingredients = new Dictionary<string, int>();
        public Dictionary<string, int> Yield = new Dictionary<string, int>();

        #endregion

        #region Constructors

        public Recipe()
        {

        }

        public Recipe(List<string> categories)
        {
            Categories.AddRange(categories);
        }

        public Recipe(List<string> categories, Dictionary<string, int> ingredients)
        {
            Categories.AddRange(categories);

            foreach (var item in ingredients)
            {
                Ingredients.Add(item.Key, item.Value);
            }
        }

        public Recipe(List<string> categories, Dictionary<string, int> ingredients, Dictionary<string, int> yield)
        {
            Categories.AddRange(categories);

            foreach (var item in ingredients)
            {
                Ingredients.Add(item.Key, item.Value);
            }

            foreach (var item in yield)
            {
                Yield.Add(item.Key, item.Value);
            }
        }

        #endregion

        #region Methods

        public virtual void AddIngredient(string name, int amount)
        {
            if (!Ingredients.ContainsKey(name))
            {
                Ingredients.Add(name, amount);
            }
            else
            {
                Ingredients[name] += amount;
            }
        }

        public virtual void AddYield(string name, int amount)
        {
            if (!Yield.ContainsKey(name))
            {
                Yield.Add(name, amount);
            }
            else
            {
                Yield[name] += amount;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
