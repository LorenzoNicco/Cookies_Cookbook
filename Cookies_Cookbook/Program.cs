using Cookies_Cookbook.Recipies;
using Cookies_Cookbook.Recipies.Ingredients;
using System.Collections.Generic;
using System.Reflection.Metadata;

var cookieApp = new CookieApp(new RecipiesDb(), new UserInteractionWithRecipies(new IngredientsRegister()));
cookieApp.Run("recipies.txt");

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
    public void Run(string filePath)
    {
        //Legge il contenuto del file dove sono salvate le ricette
        var recipiesList = _recipiesDb.Read(filePath);
        //Stampa le ricette esistenti
        _userInteractionWithRecipies.PrintExistingRecipies(recipiesList);
        //Chiede all'utente di creare una nuova ricetta
        _userInteractionWithRecipies.AskToMakeRecipe();
        /*
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
        */
        //Chiusura dell'app
        _userInteractionWithRecipies.Exit();
    }
}

//INTERFACCIA PER L'INTERAZIONE CON L'UTENTE
public interface IUserInteractionWithRecipies
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipies(IEnumerable<Recipie> recipiesList);
    void AskToMakeRecipe();
}

public class IngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new CoconoutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder(),
    };
}

//CLASSE PER L'INTERAZIONE CON L'UTENTE
public class UserInteractionWithRecipies : IUserInteractionWithRecipies
{
    private readonly IngredientsRegister _ingredientsRegister;

    //Costruttore
    public UserInteractionWithRecipies(IngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }

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

    public void PrintExistingRecipies(IEnumerable<Recipie> recipiesList)
    {
        if(recipiesList.Count() > 0)
        {
            Console.WriteLine($"Existing recipies are: " + Environment.NewLine);

            var counter = 1;

            foreach(var singleRecipie in recipiesList)
            {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(singleRecipie);
                Console.WriteLine();
                ++counter;
            }
        }
    }

    public void AskToMakeRecipe()
    {
        Console.WriteLine("Create a new cookie recipe! Available ingredients are:");

        foreach(var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }
}

//INTERFACCIA PER IL SALVATAGGIO DELLE RICETTE
public interface IRecipiesDb
{
    List<Recipie> Read(string filePath);
}

//CLASSE PER IL SALVATAGGIO DELLE RICETTE
public class RecipiesDb : IRecipiesDb
{
    public List<Recipie> Read(string filePath)
    {
        return new List<Recipie>
        {
            new Recipie(new List<Ingredient>{
                new WheatFlour(),
                new Butter(),
                new Sugar()
            }),
            new Recipie(new List<Ingredient>{
                new CocoaPowder(),
                new Sugar()
            })
        };
    }
}
