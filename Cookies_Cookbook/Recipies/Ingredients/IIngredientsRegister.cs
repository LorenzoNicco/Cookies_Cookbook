namespace Cookies_Cookbook.Recipies.Ingredients;

//INTERFACCIA PER IL REGISTRO DELLE RICETTE
public interface IIngredientsRegister
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetIngredientById(int id);
}
