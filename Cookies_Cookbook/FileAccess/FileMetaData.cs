namespace Cookies_Cookbook.FileAccess;

//CLASSE PER LA COSTRUZIONE DEL PERCORSO FILE (NOME+FORMATO)
public class FileMetaData
{
    public string Name { get; }
    public FileFormat Format { get; }

    public FileMetaData(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    //ToPath() serve a convertire le due proprietà in un percorso
    public String ToPath() => $"{Name}.{Format.AsFileExtension()}";
}
