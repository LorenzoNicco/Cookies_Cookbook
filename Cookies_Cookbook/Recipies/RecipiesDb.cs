using Cookies_Cookbook.DataAccess;
using Cookies_Cookbook.Recipies.Ingredients;

namespace Cookies_Cookbook.Recipies;

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
        return _stringsRepository.Read(filePath)
            .Select(RecipieFromString)
            .ToList();
    }

    private Recipie RecipieFromString(string singleRecipieFromFIle)
    {
        //Spezzo la stringa della ricetta per ricavare gli id
        var textualIds = singleRecipieFromFIle.Split(Separator);
        //Dichiaro una lista di ingredienti
        var ingredients = new List<Ingredient>();

        //Parso gli id ricavati in int, li uso per ricavare l'ingrediente corrispondente e lo aggiungo alla lista
        foreach (var singleTextualId in textualIds)
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
        foreach (var singleRecipie in recipieList)
        {
            //Dichiaro una lista di id per salvare gli id di ogni ingrediente della ricetta
            var allIds = new List<int>();
            foreach (var ingredient in singleRecipie.Ingredients)
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
