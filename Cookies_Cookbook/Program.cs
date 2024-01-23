//Console.WriteLine("Create a new cookie recipe! Available ingredients are:");

using System.Reflection.Metadata;

var cookieApp = new CookieApp();
cookieApp.Run();

Console.ReadKey();

//CLASSE GENERALE DELL'APP
public class CookieApp
{
    //Area di salvataggio delle ricette
    private readonly RecipiesDb _recipiesDb;
    //Interazione con l'utente
    private readonly UserInteractionWithRecipies _userInteractionWithRecipies;

    //Costruttore
    public CookieApp(RecipiesDb recipiesDb, UserInteractionWithRecipies userInteractionWithRecipies)
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

//CLASSE PER L'INTERAZIONE CON L'UTENTE
public class UserInteractionWithRecipies
{
}

//CLASSE PER IL SALVATAGGIO DELLE RICETTE
public class RecipiesDb
{
}

public class IngredientsPrinter
{
    readonly Ingredients[] ingredients = new Ingredients[]
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

    public void Printer()
    {
        foreach (Ingredients ingredient in ingredients)
        {
            Console.WriteLine($"{ingredient.ID}. {ingredient.Name}");
        }
    }
}

public abstract class Ingredients
{
    public abstract string Name { get; }
    public abstract string InstructionOfPreparing { get; }
    public abstract int ID { get; }
}

public class WheatFlour : Ingredients
{
    public override string Name => "Wheat Flour";

    public override string InstructionOfPreparing => "Sieve. Add to other ingredients.";

    public override int ID => 1;
}

public class CoconoutFlour : Ingredients
{
    public override string Name => "Coconout Flour";

    public override string InstructionOfPreparing => "Sieve. Add to other ingredients.";

    public override int ID => 2;
}

public class Butter : Ingredients
{
    public override string Name => "Butter";

    public override string InstructionOfPreparing => "Melt on low heat. Add to other ingredients.";

    public override int ID => 3;
}

public class Chocolate : Ingredients
{
    public override string Name => "Chocolate";

    public override string InstructionOfPreparing => "Melt in a water bath. Add to other ingredients.";

    public override int ID => 4;
}

public class Sugar : Ingredients
{
    public override string Name => "Sugar";

    public override string InstructionOfPreparing => "Add to other ingredients.";

    public override int ID => 5;
}

public class Cardamom : Ingredients
{
    public override string Name => "Cardamom";

    public override string InstructionOfPreparing => "Take half a teaspoon. Add to other ingredients.";

    public override int ID => 6;
}

public class Cinnamon : Ingredients
{
    public override string Name => "Cinnamon";

    public override string InstructionOfPreparing => "Take half a teaspoon. Add to other ingredients.";

    public override int ID => 7;
}

public class CocoaPowder : Ingredients
{
    public override string Name => "Cocoa Powder";

    public override string InstructionOfPreparing => "Add to other ingredients.";

    public override int ID => 8;
}