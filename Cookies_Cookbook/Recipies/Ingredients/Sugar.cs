namespace Cookies_Cookbook.Recipies.Ingredients
{
    public class Sugar : Ingredient
    {
        public override int ID => 5;
        public override string Name => "Sugar";
        public override string InstructionOfPreparing => $"{base.InstructionOfPreparing}";

    }
}
