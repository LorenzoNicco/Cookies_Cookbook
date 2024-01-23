namespace Cookies_Cookbook.Recipies.Ingredients
{
    public class CocoaPowder : Ingredient
    {
        public override int ID => 8;
        public override string Name => "Cocoa Powder";
        public override string InstructionOfPreparing => $"{base.InstructionOfPreparing}";

    }
}
