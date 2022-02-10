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
using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System.Collections.ObjectModel;

// Config program
bool generate = true; // génère ou non de nouvelles entrées en base

// Config Automappeur 
var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
var mapper = new Mapper(configuration);

// Config Database : TODO si l'on veut -> Drop puis recréer la table comme un gros sale


// Configuration BeerManager
var url = "https://localhost:7160";
var client = new HttpClient();
var beerManager = new BeerDataManager(client, mapper, url);
var breweryManager = new BreweryDataManager(client, mapper, url);
var countryManager = new CountryDataManager(client, mapper, url);
var styleManager = new StyleDataManager(client, mapper, url);
var colorManager = new ColorDataManager(client, mapper, url);
var ingredientManager = new IngredientDataManager(client, mapper, url);

//---------------------------------------------------------------------------------------------------------------------------------
// Ingredients 
//var hop = new HopModel(name: "Houblon", description: "Pour l'amertume !", alphaAcid: 4);
//var malt = new CerealModel(name: "Malt d'orge", description: "Du sucre pour nourir les levures !", ebc: 4);
//var water = new AdditiveModel(name: "Eau", description: "Ben c'est de l'eau quoi", use: "Pour rendre la bière liquide mon pote !");
//ObservableCollection<IngredientModel> bddingredients = new ObservableCollection<IngredientModel>(await ingredientManager.GetAll());
//var tempDdbHop = bddingredients[0];
////var ddbHop = await ingredientManager.GetById(tempDdbHop.Id); // nécessite de passer TypeNameHandling.All ( ou .Object) au lieu de .Auto au niveau de l'api et sa c'est chiant...
//ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel> { hop, malt, water };
//AddAndWait<IngredientModel, IngredientDto>(ingredients, ingredientManager);

//Récupérations ingredients
//ObservableCollection<IngredientModel> bddingredients = new ObservableCollection<IngredientModel>(await ingredientManager.GetAll());
//---------------------------------------------------------------------------------------------------------------------------------

#region Génération et mise en bdd
if (generate)
{   
    // Pays
    var belgique = new CountryModel(name: "Belgique");
    var france = new CountryModel(name: "France");
    var ecosse = new CountryModel(name: "Ecosse");
    List<CountryModel> countries = new List<CountryModel>();
    countries.Add(belgique);
    countries.Add(france);
    countries.Add(ecosse);
    AddAndWait<CountryModel, CountryDto>(countries, countryManager);

    //Récupération pays pour brasseries
    var bddcountry = await countryManager.GetAll();
    var bddFrance = bddcountry.FirstOrDefault(c => c.Name == "France");
    var bddBelgique = bddcountry.FirstOrDefault(c => c.Name == "Belgique");
    var bddEcosse = bddcountry.FirstOrDefault(c => c.Name == "Ecosse");

    // Brasseries
    var brewdog = new BreweryModel(name: "Brewdog", description: "Des chiens qui brassent", bddEcosse);
    var linderman = new BreweryModel(name: "Brasserie Lindemans", description: "Ils aiment les fruits", bddBelgique);
    var ninkasi = new BreweryModel(name: "Ninkasi", description: "A l'eau pure du Rhone", country: bddFrance);
    List<BreweryModel> breweries = new List<BreweryModel>() { brewdog, linderman, ninkasi};
    AddAndWait<BreweryModel, BreweryDto>(breweries, breweryManager);

    //Récupération brasseries pour bières
    var bddBreweries = await breweryManager.GetAll();
    var bddBrewdog = bddBreweries.FirstOrDefault(c => c.Name == "Brewdog");
    var bddLinderman = bddBreweries.FirstOrDefault(c => c.Name == "Brasserie Lindemans");
    var bddNinkasi = bddBreweries.FirstOrDefault(c => c.Name == "Ninkasi");

    // Couleurs
    var blonde = new BeerColorModel(name: "Blonde");
    var brune = new BeerColorModel(name: "Brune");
    var blanche = new BeerColorModel(name: "Blanche");
    var fruitee = new BeerColorModel(name: "Fruitée");
    List<BeerColorModel> colors = new List<BeerColorModel>() { blonde, brune, blanche, fruitee};
    AddAndWait<BeerColorModel, BeerColorDto>(colors, colorManager);

    //Récupération couleur pour bières
    var bddColor = await colorManager.GetAll();
    var bddBlonde = bddColor.FirstOrDefault(c => c.Name == "Blonde");
    var bddBrune = bddColor.FirstOrDefault(c => c.Name == "Brune");
    var bddBlanche = bddColor.FirstOrDefault(c => c.Name == "Blanche");
    var bddFruitee = bddColor.FirstOrDefault(c => c.Name == "Fruitée");

    // Styles
    var ipa = new BeerStyleModel(name: "IPA", description: "");
    var lambic = new BeerStyleModel(name: "Lambic", description: "");
    var speciale = new BeerStyleModel(name: "Spéciale", description: "");
    var apa = new BeerStyleModel(name: "American pale ale", description: "");
    var smok = new BeerStyleModel(name: "Smoked Beer", description: "");
    var ale = new BeerStyleModel(name: "Ale", description: "");
    List<BeerStyleModel> styles = new List<BeerStyleModel>() { ale, speciale, apa, smok, lambic, ipa};
    AddAndWait<BeerStyleModel, BeerStyleDto>(styles, styleManager);

    //Récupération styles pour bières
    var bddStyle = await styleManager.GetAll();
    var bddIPA = bddStyle.FirstOrDefault(c => c.Name == "IPA");
    var bddLambic = bddStyle.FirstOrDefault(c => c.Name == "Lambic");
    var bddSpeciale = bddStyle.FirstOrDefault(c => c.Name == "Spéciale");
    var bddAPA = bddStyle.FirstOrDefault(c => c.Name == "American pale ale");
    var bddSmoked = bddStyle.FirstOrDefault(c => c.Name == "Smoked Beer");
    var bddAle = bddStyle.FirstOrDefault(c => c.Name == "Ale");

    // Ingredients
    var hop = new HopModel(name: "Houblon", description: "Pour l'amertume !", alphaAcid: 4);
    var malt = new CerealModel(name: "Malt d'orge", description: "Du sucre pour nourir les levures !", ebc: 4);
    var water = new AdditiveModel(name: "Eau", description: "Ben c'est de l'eau quoi", use: "Pour rendre la bière liquide mon pote !");
    ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel> { hop, malt, water };
    AddAndWait<IngredientModel, IngredientDto>(ingredients, ingredientManager);

    //Récupérations ingredients
    ObservableCollection<IngredientModel> bddingredients = new ObservableCollection<IngredientModel>(await ingredientManager.GetAll());

    // Beers
    float abv = 10;
    float ibu = (float)5.5;
    // Suite Beers
    var punk = new BeerModel("PUNK IPA", "Avec ou sans créte", abv, ibu, bddIPA, bddBlonde, bddBrewdog, bddingredients);
    var hazy = new BeerModel("HAZY JANE", "", abv, ibu, bddIPA, bddBlonde, bddBrewdog, bddingredients);
    var cloud = new BeerModel("BREWDOG VS CLOUDWATER", "", abv, ibu, bddAle, bddBlonde, bddBrewdog, bddingredients);
    var elvis = new BeerModel("ELVIS JUICE", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddingredients);
    var kriek = new BeerModel("KRIEK", "", abv, ibu, bddLambic, bddFruitee, bddLinderman, bddingredients);
    var gueuze = new BeerModel("GUEUZE", "", abv, ibu, bddLambic, bddBlonde, bddLinderman, bddingredients);
    var faro = new BeerModel("FARO LAMBIC", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddingredients);
    var nipa = new BeerModel("NINKASI IPA", "", abv, ibu, bddIPA, bddBlonde, bddNinkasi, bddingredients);
    var nblance = new BeerModel("NINKASI BLANCHE", "", abv, ibu, bddAle, bddBlanche, bddNinkasi, bddingredients);
    var npa = new BeerModel("NINKASI PALE ALE", "", abv, ibu, bddAle, bddBrune, bddNinkasi, bddingredients);
    List<BeerModel> beers = new List<BeerModel>() { punk, hazy, cloud, elvis, kriek, gueuze, faro, nipa, nblance, npa};
    AddAndWait<BeerModel, BeerDto>(beers, beerManager);
    // Récupération pour check dans le debuger
    var bddBeers = await beerManager.GetAll();
    
}
#endregion

#region Tests divers
//var toDeleteStyle = bddAle;
//styleManager.DeleteById(bddAle.Id).Wait();
var modifiedbddBeers = await beerManager.GetAll();
var bddStyles = await styleManager.GetAll();
var updatedBeer = modifiedbddBeers.ToList()[0];
updatedBeer.Name = "Biere azeaze modifiée";
updatedBeer.Style = bddStyles.ToList()[0];
beerManager.Update(updatedBeer.Id, updatedBeer).Wait();
updatedBeer.Name = "A nouveau modifée";
updatedBeer.Ingredients.RemoveAt(0);
beerManager.Update(updatedBeer.Id, updatedBeer).Wait();
var newUpdatedBeer = await beerManager.GetById(updatedBeer.Id);
var updatedStyle = bddStyles.ToList()[bddStyles.Count()-1];// updatedBeer.Style;
updatedStyle.Description = "Un gout étrange";
styleManager.Update(updatedStyle.Id, updatedStyle).Wait();
styleManager.DeleteById(updatedStyle.Id).Wait();
styleManager.Add(updatedStyle).Wait();
#endregion



#region Méthode(s) locale(s)
/// <summary>
/// Méthode qui permet l'ajout via méthodes asynchrone (de IDataManager)
/// tt en attendant la fin des taches avant de lacher la main
/// Equivalent d'un foreach manager.Add(item).RunAsynchronously() au final
/// </summary>
void AddAndWait<TModel, TDto>(IEnumerable<TModel> list, IDataManager<TModel, TDto> manager)
    where TModel : class where TDto : class
{
    var tasks = new List<Task>();
    foreach (var item in list)
    {
        //var task = manager.Add(item);
        tasks.Add(manager.Add(item));
    }
    Task.WaitAll(tasks.ToArray());
}
#endregion

Console.WriteLine("Execution terminée");
Console.ReadLine();
