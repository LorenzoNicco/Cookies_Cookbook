using Cookies_Cookbook.Recipies.Ingredients;
using Cookies_Cookbook.Recipies;

namespace Cookies_Cookbook.App;

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

    //Metodo per stampare le ricette esistenti
    public void PrintExistingRecipies(IEnumerable<Recipie> recipiesList)
    {
        if (recipiesList.Count() > 0)
        {
            Console.WriteLine($"Existing recipies are: " + Environment.NewLine);

            var allRecipiesAsStrings = recipiesList
                .Select((recipie, index) =>
$@"*****{index + 1}*****
{recipie}");

            Console.WriteLine(
                string.Join(Environment.NewLine, allRecipiesAsStrings));
            Console.WriteLine();
        }
    }

    public void AskToMakeRecipe()
    {
        Console.WriteLine("Create a new cookie recipe! Available ingredients are:");

        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    //Metodo per creare la lista degli ingredienti scelti dall'utente
    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        //Inizializzo una flag di controllo per fermare il loop
        bool stopFlag = false;
        //Dichiaro una lista di ingredienti
        var ingredients = new List<Ingredient>();

        while (!stopFlag)
        {
            Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");

            //Raccolgo il numero inserito dall'utente
            var userInput = Console.ReadLine();

            //Se l'utente ha inserito un numero vado a prendere l'ingrediente, altrimenti fermo il loop
            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetIngredientById(id);

                //Controllo che l'utente abbia inserito un id valido
                if (selectedIngredient is not null)
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
