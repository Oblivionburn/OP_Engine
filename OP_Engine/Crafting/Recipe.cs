namespace OP_Engine.Crafting
{
    public class Recipe : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Description;
        public string? Type;

        public TimeSpan CraftingTime;
        public List<string> Categories = [];
        public Dictionary<string, int> Ingredients = [];
        public Dictionary<string, int> Yield = [];

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
            if (!Ingredients.TryAdd(name, amount))
            {
                Ingredients[name] += amount;
            }
        }

        public virtual void AddYield(string name, int amount)
        {
            if (!Yield.TryAdd(name, amount))
            {
                Yield[name] += amount;
            }
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
