/*
  Test de peuplement de la base de donnée.
    Sur les problème de sérialisation/désérialisation : NewTon Soft est une solution mais aps très sécure!
    La manière propre de faire seriat de passer par System.Net.Http.Json qui utilise System.Net.Http.Json.
    Il faut ensuite passer à JsonSerializerOptions un JsonConverter<T> qu'il faut implémenter soit même!
    https://stackoverflow.com/questions/58074304/is-polymorphic-deserialization-possible-in-system-text-json
    https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-6-0#support-polymorphic-deserialization
    https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism.
    On peut également resté sur NewtonSoft et gérer ensuite la tranformation coté angular via 
    https://github.com/typestack/class-transformer
    https://www.npmjs.com/package/json2typescript
    Pour plus d'info sur comment faire proprement avec NewtonSoft
    https://www.newtonsoft.com/json/help/html/SerializationAttributes.htm
    https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonConverterAttribute.htm
    https://www.newtonsoft.com/json/help/html/SerializeSerializationBinder.htm
    https://www.newtonsoft.com/json/help/html/CustomJsonConverterGeneric.htm
    https://www.newtonsoft.com/json/help/html/CustomJsonConverter.htm
    Voir également
    https://github.com/manuc66/JsonSubTypes
 */
using Ipme.WikiBeer.Ressources;
using Ipme.WikiBeer.Tools;

// Paramètres
var dbName = "CrashTest";
var cs = @$"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = {dbName}; Integrated Security = True;";
//var apiUrl = "https://localhost:7160";
var apiUrl = "https://localhost:5001"; // valeur par défaut si lancée en Auto
var apiPath = @"C:\Users\armel\git\Formation_IPME_dot_net\Projet\EncyclopeBeer\WikiBeer\API\bin\Debug\net6.0\Ipme.WikiBeer.Api.exe";
bool autoRunApi = true;
       
// Classes utilitaires
var dbr = new DataBaseRessource(apiUrl);
var manager = new DbManager(cs);
var launcher = new ApiLauncher(apiUrl, apiPath, cs);

manager.DropDataBase();
if (autoRunApi)
{
    // Si l'API n'est pas lancée, la base non existente
    AutoFill(dbr, manager, launcher);
}
else
{
    // Si l'API est lancé, la base existante ou non
    manager.EnsureDatabaseCreation();
    dbr.FillDatabase();
}

// Méthodes utilitaires
/// <summary>
/// Pour lancer l'api tt en gardant le controle sur le processus
/// </summary>
void AutoFill(DataBaseRessource dbr, DbManager manager , ApiLauncher launcher)
{
    try
    {
        launcher.StartApi();
        manager.EnsureDatabaseCreation();
        dbr.FillDatabase();
    }
    catch(Exception ex)
    {

    }
    finally
    {
        launcher.StopApi();
    }
}

Console.WriteLine("Execution terminée");
Console.ReadLine();


