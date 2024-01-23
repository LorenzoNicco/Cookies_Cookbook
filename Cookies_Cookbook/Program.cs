//Console.WriteLine("Create a new cookie recipe! Available ingredients are:");

using System.Reflection.Metadata;

var cookieApp = new CookieApp(new RecipiesDb(), new UserInteractionWithRecipies());
cookieApp.Run();

Console.ReadKey();

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
    public void Run()
    {
        //Legge il contenuto del file dove sono salvate le ricette
        var recipiesList = _recipiesDb.Read(filePath);
        //Stampa le ricette esistenti
        _userInteractionWithRecipies.PrintExistingRecipies(recipiesList);
        //Chiede all'utente di creare una nuova ricetta
        _userInteractionWithRecipies.AskToMakeRecipe();
        //Prende gli ingredienti scelti dall'utente
        var ingredients = _userInteractionWithRecipies.ReadIngredientsFromUser();

        if(ingredients.Count > 0)
        {
            //Prende gli ingredienti scelti dall'utente e li salva nel file
            var recipies = new Recipie(ingredients);
            recipiesList.Add(recipie);
            _recipiesDb.Write(filePath, recipieList);

            //Avvisa l'utente che la ricetta è stata salvata nel file
            _userInteractionWithRecipies.ShowMessage("Recipe added:");
            _userInteractionWithRecipies.ShowMessage(recipe.ToString());
        }
        else
        {
            _userInteractionWithRecipies.ShowMessage("No ingredients have been selected. Recipe will not be saved.")
        }

        //Chiusura dell'app
        _userInteractionWithRecipies.Exit();
    }
}

//INTERFACCIA PER L'INTERAZIONE CON L'UTENTE
public interface IUserInteractionWithRecipies
{
    void ShowMessage(string message);
    void Exit();
}

//CLASSE PER L'INTERAZIONE CON L'UTENTE
public class UserInteractionWithRecipies : IUserInteractionWithRecipies
{
    //Metodo per stampare a schermo il messaggio
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    //Metodo per chiudere l'app
    public void Exit()
    {
        Console.WriteLine("Press any key to close");
        Console.ReadKey();
    }
}

//INTERFACCIA PER IL SALVATAGGIO DELLE RICETTE
public interface IRecipiesDb
{
}

//CLASSE PER IL SALVATAGGIO DELLE RICETTE
public class RecipiesDb : IRecipiesDb
{
}
