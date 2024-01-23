namespace Cookies_Cookbook.Recipies.Ingredients
{
    public abstract class Spice : Ingredient
    {
        public override string InstructionOfPreparing => $"Take half a teaspoon. {base.InstructionOfPreparing}";
    }
}
