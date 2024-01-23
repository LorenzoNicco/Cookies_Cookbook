using Cookies_Cookbook.Recipies.Ingredients;

namespace Cookies_Cookbook.Recipies
{
    public class Recipie
    {
        public IEnumerable<Ingredient> Ingredients { get; }

        public Recipe(IEnumerable<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }
    }
}
