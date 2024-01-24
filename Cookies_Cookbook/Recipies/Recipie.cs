﻿using Cookies_Cookbook.Recipies.Ingredients;

namespace Cookies_Cookbook.Recipies;

public class Recipie
{
    public IEnumerable<Ingredient> Ingredients { get; }

    public Recipie(IEnumerable<Ingredient> ingredients)
    {
        Ingredients = ingredients;
    }

    //Sovreascrittura del comportamento base del metodo ToString() per stampare la lista degli ingredienti
    public override string ToString()
    {
        var steps = new List<string>();
        foreach (var ingredient in Ingredients)
        {
            steps.Add($"{ingredient.Name} . {ingredient.InstructionOfPreparing}");
        }
        return string.Join(Environment.NewLine, steps);
    }
}
