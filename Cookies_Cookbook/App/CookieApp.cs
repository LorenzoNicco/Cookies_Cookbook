using Cookies_Cookbook.Recipies;

namespace Cookies_Cookbook.App;

//CLASSE GENERALE DELL'APP
public class CookieApp
{
    //Area di salvataggio delle ricette
    private readonly IRecipiesDb _recipiesDb;
    //Interazione con l'utente
    private readonly IUserInteractionWithRecipies _userInteractionWithRecipies;

    //Costruttore
    public CookieApp(IRecipiesDb recipiesDb, IUserInteractionWithRecipies userInteractionWithRecipies)
    {
        _recipiesDb = recipiesDb;
        _userInteractionWithRecipies = userInteractionWithRecipies;
    }

    //METODO CON LOGICA DI FUNZIONAMENTO DELL'APP
    public void Run(string filePath)
    {
        //Legge il contenuto del file dove sono salvate le ricette
        var recipiesList = _recipiesDb.Read(filePath);
        //Stampa le ricette esistenti
        _userInteractionWithRecipies.PrintExistingRecipies(recipiesList);
        //Chiede all'utente di creare una nuova ricetta
        _userInteractionWithRecipies.AskToMakeRecipe();
        //Prende gli ingredienti scelti dall'utente
        var ingredients = _userInteractionWithRecipies.ReadIngredientsFromUser();

        if (ingredients.Count() > 0)
        {
            //Prende gli ingredienti scelti dall'utente e li salva nel file
            var recipie = new Recipie(ingredients);
            recipiesList.Add(recipie);
            _recipiesDb.Write(filePath, recipiesList);

            //Avvisa l'utente che la ricetta è stata salvata nel file
            _userInteractionWithRecipies.ShowMessage("Recipe added:");
            _userInteractionWithRecipies.ShowMessage(recipie.ToString());
        }
        else
        {
            _userInteractionWithRecipies.ShowMessage("No ingredients have been selected. Recipe will not be saved.");
        }

        //Chiusura dell'app
        _userInteractionWithRecipies.Exit();
    }
}
