using System.Text.Json;

namespace Cookies_Cookbook.DataAccess;

//CLASSE PER IL SALVATAGGIO NEL FORMATO JSON
public class StringsJsonRepository : StringsRepository
{
    protected override string ListOfStringsToText(List<string> strings)
    {
        return JsonSerializer.Serialize(strings);
    }

    protected override List<string> TextToListOfStrings(string fileContent)
    {
        return JsonSerializer.Deserialize<List<string>>(fileContent);
    }
}
