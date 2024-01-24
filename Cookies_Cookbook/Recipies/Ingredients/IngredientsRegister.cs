namespace Cookies_Cookbook.Recipies.Ingredients;

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
