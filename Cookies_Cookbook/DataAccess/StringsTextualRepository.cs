namespace Cookies_Cookbook.DataAccess;

//CLASSE PER IL SALVATAGGIO NEL FORMATO TXT
public class StringsTextualRepository : StringsRepository
{
    //Inizializzo un separatore
    private static readonly string Separator = Environment.NewLine;

    protected override string ListOfStringsToText(List<string> strings)
    {
        return string.Join(Separator, strings);
    }

    protected override List<string> TextToListOfStrings(string fileContent)
    {
        return fileContent.Split(Separator).ToList();
    }
}
