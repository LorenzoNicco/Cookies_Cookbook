using Cookies_Cookbook.Recipies;
using Cookies_Cookbook.Recipies.Ingredients;
using System.Collections.Generic;
using System.Reflection.Metadata;

//Inizializzo una variabile contenente IngredientsRegister per usarlo piu volte
var ingredientsRegister = new IngredientsRegister();

//Inizializzo l'app e la faccio partire, indicando il percorso del file dove salvare le ricette
var cookieApp = new CookieApp(
    new RecipiesDb(new StringsTextualRepository(), ingredientsRegister), 
    new UserInteractionWithRecipies(ingredientsRegister)
);
cookieApp.Run("recipies.txt");

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

//INTERFACCIA PER L'INTERAZIONE CON L'UTENTE
public interface IUserInteractionWithRecipies
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipies(IEnumerable<Recipie> recipiesList);
    void AskToMakeRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}

//INTERFACCIA PER IL REGISTRO DELLE RICETTE
public interface IIngredientsRegister
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetIngredientById(int id);
}

//CLASSE PER IL REGISTRO DELLE RICETTE
public class IngredientsRegister : IIngredientsRegister
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
        foreach (var ingredient in All)
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
    private readonly IIngredientsRegister _ingredientsRegister;

    //Costruttore
    public UserInteractionWithRecipies(IIngredientsRegister ingredientsRegister)
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
    void Write(string filePath, List<Recipie> recipieList);
}

//CLASSE PER IL SALVATAGGIO DELLE RICETTE
public class RecipiesDb : IRecipiesDb
{
    //Creo la dependency per la Stringsrepository
    private readonly IStringsRepository _stringsRepository;

    //Creo la dependency per IngredientsRegister
    private readonly IIngredientsRegister _ingredientsRegister;

    //Inizializzo una costante per il separatore delle stringhe
    private const string Separator = ",";

    //Costruttore
    public RecipiesDb(IStringsRepository stringsRepository, IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    //Metodo per la lettura del file contenente le ricette
    public List<Recipie> Read(string filePath)
    {
        List<string> recipiesFromFile = _stringsRepository.Read(filePath);
        var recipies = new List<Recipie>();

        foreach(var singleRecipieFromFIle in recipiesFromFile)
        {
            var recipie = RecipieFromString(singleRecipieFromFIle);
            recipies.Add(recipie);
        }

        return recipies;
    }

    private Recipie RecipieFromString(string singleRecipieFromFIle)
    {
        //Spezzo la stringa della ricetta per ricavare gli id
        var textualIds = singleRecipieFromFIle.Split(Separator);
        //Dichiaro una lista di ingredienti
        var ingredients = new List<Ingredient>();

        //Parso gli id ricavati in int, li uso per ricavare l'ingrediente corrispondente e lo aggiungo alla lista
        foreach(var singleTextualId in textualIds)
        {
            var id = int.Parse(singleTextualId);
            var ingredient = _ingredientsRegister.GetIngredientById(id);
            ingredients.Add(ingredient);
        }

        return new Recipie(ingredients);
    }

    //Metodo per scrivere la lista di id degli ingredienti nel file
    public void Write(string filePath, List<Recipie> recipieList)
    {
        //Dichiaro la lista dove salverò gli id degli ingredienti
        var recipiesAsStrings = new List<string>();

        //Per ogni ricetta nella lista delle ricette
        foreach(var singleRecipie in recipieList)
        {
            //Dichiaro una lista di id per salvare gli id di ogni ingrediente della ricetta
            var allIds = new List<int>();
            foreach(var ingredient in singleRecipie.Ingredients)
            {
                allIds.Add(ingredient.ID);
            }

            //Unisco gli id scelti in una stringa unica per ogni ricetta e aggiungo tutto alla lista delle ricette
            recipiesAsStrings.Add(string.Join(Separator, allIds));
        }

        //Salvo la lista delle ricette con gli id nel file al percorso indicato
        _stringsRepository.Write(filePath, recipiesAsStrings);
    }
}

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}

public class StringsTextualRepository : IStringsRepository
{
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filePath)
    {
        //Controllo se il file esiste
        if(File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            return fileContent.Split(Separator).ToList();
        }

        return new List<string> ();
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, string.Join(Separator, strings));
    }
}