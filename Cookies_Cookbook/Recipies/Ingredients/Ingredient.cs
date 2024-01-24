namespace Cookies_Cookbook.Recipies.Ingredients;

public abstract class Ingredient
{
    public abstract int ID { get; }
    public abstract string Name { get; }
    public virtual string InstructionOfPreparing => "Add to other ingredients";

    public override string ToString() => $"{ID} . {Name}";
}
