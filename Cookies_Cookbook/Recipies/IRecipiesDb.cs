namespace Cookies_Cookbook.Recipies;

//INTERFACCIA PER IL SALVATAGGIO DELLE RICETTE
public interface IRecipiesDb
{
    List<Recipie> Read(string filePath);
    void Write(string filePath, List<Recipie> recipieList);
}
