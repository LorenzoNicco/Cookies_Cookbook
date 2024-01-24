using Cookies_Cookbook.App;
using Cookies_Cookbook.DataAccess;
using Cookies_Cookbook.FileAccess;
using Cookies_Cookbook.Recipies;
using Cookies_Cookbook.Recipies.Ingredients;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json;

//Costante per la scelta del formato di file
const FileFormat Format = FileFormat.Txt;

//Operatore ternario per la scelta di quale repository inizializzare in base al formato (non possiamo usare un tipo generico)
IStringsRepository stringsRepository = Format == FileFormat.Json ? new StringsJsonRepository() : new StringsTextualRepository();

//Inizializzo il nome del file e lo uso come argomento per creare dinamicamente il percorso
const string FileName = "reicpies";
var fileMetaData = new FileMetaData(FileName, Format);

//Inizializzo una variabile contenente IngredientsRegister per usarlo piu volte
var ingredientsRegister = new IngredientsRegister();

//Inizializzo l'app e la faccio partire, indicando il percorso del file dove salvare le ricette
var cookieApp = new CookieApp(
    new RecipiesDb(stringsRepository, ingredientsRegister),
    new UserInteractionWithRecipies(ingredientsRegister)
);
cookieApp.Run(fileMetaData.ToString());



