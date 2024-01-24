namespace Cookies_Cookbook.DataAccess;

//INTERFACCIA PER IL SALVATAGGIO NEL FILE
public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}
