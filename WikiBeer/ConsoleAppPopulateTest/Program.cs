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
    Voir également
https://github.com/manuc66/JsonSubTypes
 */
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using ConsoleAppPopulateTest;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System.Collections.ObjectModel;

// Config Automappeur 
var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
var mapper = new Mapper(configuration);

// Config Autofixture
var fixture = new Fixture();
fixture.Customizations.Add(new TypeRelay(typeof(IngredientModel), typeof(HopModel)));

// Configuration BeerManager
var url = "https://localhost:7160";
var client = new HttpClient();
var beerManager = new BeerDataManager(client, mapper, url);
var breweryManager = new BreweryDataManager(client, mapper, url);
var countryManager = new CountryDataManager(client, mapper, url);
var styleManager = new StyleDataManager(client, mapper, url);
var colorManager = new ColorDataManager(client, mapper, url);

#region Génération et mise en bdd
// Pays
var belgique = new CountryModel(name: "Belgique");
var france = new CountryModel(name: "France");
var ecosse = new CountryModel(name: "Ecosse");

List<CountryModel> countryList = new List<CountryModel>();
countryList.Add(belgique);
countryList.Add(france);
countryList.Add(ecosse);

//Ajout des countries en base
countryList.ForEach(async country => await countryManager.Add(country));

//Récupération
var bddcountry = await countryManager.GetAll();
var bddFrance = bddcountry.FirstOrDefault(c => c.Name == "France");
var bddBelgique = bddcountry.FirstOrDefault(c => c.Name == "Belgique");
var bddEcosse = bddcountry.FirstOrDefault(c => c.Name == "Ecosse");


// Brasseries
var brewdog = new BreweryModel(name: "Brewdog", description: "Des chiens qui brassent", bddEcosse);
var linderman = new BreweryModel(name: "Brasserie Lindemans", description: "Ils aiment les fruits", bddBelgique);
var ninkasi = new BreweryModel(name: "Ninkasi", description: "A l'eau pure du Rhone", country: bddFrance);

List<BreweryModel> breweryList = new List<BreweryModel>();
breweryList.Add(ninkasi);
breweryList.Add(brewdog);
breweryList.Add(linderman);

////Ajout des breweries en base
breweryList.ForEach(async brewery => await breweryManager.Add(brewery));

//Récupération
var bddCountry = await breweryManager.GetAll();
var bddBrewdog = bddCountry.FirstOrDefault(c => c.Name == "Brewdog");
var bddLinderman = bddCountry.FirstOrDefault(c => c.Name == "Brasserie Lindemans");
var bddNinkasi = bddCountry.FirstOrDefault(c => c.Name == "Ninkasi");


// Couleurs
var blonde = new BeerColorModel(name: "Blonde");
var brune = new BeerColorModel(name: "Brune");
var blanche = new BeerColorModel(name: "Blanche");
var fruitee = new BeerColorModel(name: "Fruitée");

List<BeerColorModel> colorList = new List<BeerColorModel>();
colorList.Add(blonde);
colorList.Add(brune);
colorList.Add(blanche);
colorList.Add(fruitee);

////Ajout des colors en base
colorList.ForEach(async color => await colorManager.Add(color));

//Récupération
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

List<BeerStyleModel> styleList = new List<BeerStyleModel>();
styleList.Add(ale);
styleList.Add(speciale);
styleList.Add(apa);
styleList.Add(smok);
styleList.Add(lambic);
styleList.Add(ipa);

////Ajout des styles en base
styleList.ForEach(async style => await styleManager.Add(style));

//Récupération
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

//Beers
//var randomFloat = new RandomFloat();
//var randomABV = randomFloat.RandABV();
//var randomIBU = randomFloat.RandIBU();
var ibu = 35F;
var abv = 5.5F;

var punk = new BeerModel("PUNK IPA", "Avec ou sans créte", abv, ibu, bddIPA, bddBlonde, bddBrewdog, ingredients);
await beerManager.Add(punk);

//Récupération ingrédients
var bddBeer = await beerManager.GetAll();
var bddPunk = await beerManager.GetById(bddBeer.ToList()[0].Id);

// Suite Beers
var hazy = new BeerModel("HAZY JANE", "", abv, ibu, bddIPA, bddBlonde, bddBrewdog, bddPunk.Ingredients);
var cloud = new BeerModel("BREWDOG VS CLOUDWATER", "", abv, ibu, bddAle, bddBlonde, bddBrewdog, bddPunk.Ingredients);
var elvis = new BeerModel("ELVIS JUICE", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddPunk.Ingredients);
var kriek = new BeerModel("KRIEK", "", abv, ibu, bddLambic, bddFruitee, bddLinderman, bddPunk.Ingredients);
var gueuze = new BeerModel("GUEUZE", "", abv, ibu, bddLambic, bddBlonde, bddLinderman, bddPunk.Ingredients);
var faro = new BeerModel("FARO LAMBIC", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddPunk.Ingredients);
var nipa = new BeerModel("NINKASI IPA", "", abv, ibu, bddIPA, bddBlonde, bddNinkasi, bddPunk.Ingredients);
var nblance = new BeerModel("NINKASI BLANCHE", "", abv, ibu, bddAle, bddBlanche, bddNinkasi, bddPunk.Ingredients);
var npa = new BeerModel("NINKASI PALE ALE", "", abv, ibu, bddAle, bddBrune, bddNinkasi, bddPunk.Ingredients);

List<BeerModel> beerList = new List<BeerModel>();
beerList.Add(hazy);
beerList.Add(cloud);
beerList.Add(elvis);
beerList.Add(kriek);
beerList.Add(gueuze);
beerList.Add(faro);
beerList.Add(nipa);
beerList.Add(nblance);
beerList.Add(npa);

////Ajout des bières en base
beerList.ForEach(async beer => await beerManager.Add(beer));
var bddBeers = await beerManager.GetAll();

#endregion


//Test ajout beer avec ingredient
//Récupération composants deja en base
//var breweryModels = await breweryManager.GetAll();
//var colorModels = await colorManager.GetAll();
//var styleModels = await styleManager.GetAll();
//var countryModels =await countryManager.GetAll();


// Bière de test, sans ingrédients en attendant de trouver une solution
//var pony = new BeerModel("DEAD PONY CLUB", "", 8, 4, apa, blonde, brewdog, ingredients);
//await beerManager.Add(pony);
//var beers = await beerManager.GetAll();
//var new_pony = await beerManager.GetById(beers.ToList()[0].Id);
//var peche = new BeerModel("La Pêcheresse","", 10, 4, lambic, new_pony.Color, linderman, new_pony.Ingredients);
//await beerManager.Add(peche);

// Création liste de bières 
//var beers = new List<BeerModel> { pony, peche };

// Injection bière dans la database. Attention en faisant comme sa on duplique plusieurs objets en bdd
// Pour éviter sa il faudrait injecter une bière, récupérer les Guid des objets communs à tt (ingrédient, couleur), 
// les donner aux modèles, puis faire un add de la beer en question (serait un bon test pour voir si la base est bien branlé!)
//foreach (var beer in beers)
//{
//    await beerManager.Add(beer);
//}


Console.WriteLine("Execution terminée");
Console.ReadLine();

//
//var punk = new BeerModel();
//punk.Name = "Punk IPA";
//punk.Brewery = brewdog;
//punk.Ibu = 10;
//punk.Degree = 5;
//punk.Color = blonde;
//punk.Style = ipa;
//punk.Ingredients = ingredient;

//var kriek = new BeerModel();
//kriek.Name = "Lindemans Kriek";
//kriek.Brewery = linderman;
//kriek.Ibu = 8;
//kriek.Degree = 4;
//kriek.Color = blonde;
//kriek.Style = lambic;


//var smoky = new BeerModel();
//smoky.Name = "Smoky Oak Ale";
//smoky.Brewery = ninkasi;
//smoky.Ibu = 8;
//smoky.Degree = 4;
//smoky.Color = brune;
//smoky.Style = smok;

//await beerManager.Add(smoky);
//Console.WriteLine("smoky ajouté");


//await beerManager.Add(mango);
