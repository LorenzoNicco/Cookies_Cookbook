using Cookies_Cookbook.Recipies.Ingredients;
using Cookies_Cookbook.Recipies;

namespace Cookies_Cookbook.App;

//INTERFACCIA PER L'INTERAZIONE CON L'UTENTE
public interface IUserInteractionWithRecipies
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipies(IEnumerable<Recipie> recipiesList);
    void AskToMakeRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}
