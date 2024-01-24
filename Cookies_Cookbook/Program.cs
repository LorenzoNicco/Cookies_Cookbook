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
        //Prende gli ingredienti scelti dall'utente
        var ingredients = _userInteractionWithRecipies.ReadIngredientsFromUser();

        if(ingredients.Count() > 0)
        {
            //Prende gli ingredienti scelti dall'utente e li salva nel file
            var recipie = new Recipie(ingredients);
            recipiesList.Add(recipie);
           // _recipiesDb.Write(filePath, recipieList);

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

//INTERFACCIA PER L'INTERAZIONE CON L'UTENTE
public interface IUserInteractionWithRecipies
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipies(IEnumerable<Recipie> recipiesList);
    void AskToMakeRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}

public class IngredientsRegister
{
    //Inizializzo una interfaccia IEnumerable di ingredienti con le classi create in precedenza
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

    //Metodo per prendere l'ingrediente tramite il numero inserito dall'utente
    public Ingredient GetIngredientById(int id)
    {
        //Per ogni ingrediente nell'IEnumerable, controllo che l'input inserito dall'utente corrisponda ad un id, altrimenti ritorno null
        foreach(var ingredient in All)
        {
            if (ingredient.ID == id)
            {
                return ingredient;
            }
        }

        return null;
    }
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

    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        //Inizializzo una flag di controllo per fermare il loop
        bool stopFlag = false;
        //Dichiaro una lista di ingredienti
        var ingredients = new List<Ingredient>();

        while(!stopFlag)
        {
            Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");

            //Raccolgo il numero inserito dall'utente
            var userInput = Console.ReadLine();

            //Se l'utente ha inserito un numero vado a prendere l'ingrediente, altrimenti fermo il loop
            if(int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetIngredientById(id);

                //Controllo che l'utente abbia inserito un id valido
                if(selectedIngredient is not null)
                {
                    //Aggiungo l'ingrediente alla lista
                    ingredients.Add(selectedIngredient);
                }    
            }
            else
            {
                stopFlag = true;
            }
        }

        return ingredients;
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
