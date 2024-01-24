namespace Cookies_Cookbook.FileAccess;

//CLASSE PER SWITCHARE TRA I FORMATI DI FILE
public static class FileFormatExtension
{
    public static string AsFileExtension(this FileFormat fileFormat) => fileFormat == FileFormat.Json ? "json" : "txt";
}
