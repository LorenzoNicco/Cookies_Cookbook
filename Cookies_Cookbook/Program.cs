Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
Console.ReadKey();

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