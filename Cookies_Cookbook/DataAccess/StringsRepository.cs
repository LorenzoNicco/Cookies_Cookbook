namespace Cookies_Cookbook.DataAccess;

//CLASSE ASTRATTA PER IL SALVATAGGIO NEL FILE
public abstract class StringsRepository : IStringsRepository
{
    public List<string> Read(string filePath)
    {
        //Controllo se il file esiste
        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            return TextToListOfStrings(fileContent);
        }

        return new List<string>();
    }

    protected abstract List<string> TextToListOfStrings(string fileContent);

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, ListOfStringsToText(strings));
    }

    protected abstract string ListOfStringsToText(List<string> strings);
}
