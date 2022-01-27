/*
  Test de peuplement de la base de donnée.
    Sur les problème de sérialisation/désérialisation : NewTon Soft est une solution mais aps très sécure!
    La manière propre de faire seriat de passer par System.Net.Http.Json qui utilise System.Net.Http.Json.
    Il faut ensuite passer à JsonSerializerOptions un JsonConverter<T> qu'il faut implémenter soit même!
https://stackoverflow.com/questions/58074304/is-polymorphic-deserialization-possible-in-system-text-json
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-6-0#support-polymorphic-deserialization
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
 */
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;

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

Console.WriteLine("Presser une touche pour commencer");
Console.ReadLine();

// Création d'une bière
var guid = Guid.Empty;
var belgique = new CountryModel(name: "Belgique");
var france = new CountryModel(name: "France");
var ecosse = new CountryModel(name: "Ecosse");

var brewdog = new BreweryModel(name: "Brewdog", description: "Des chiens qui brassent", country: ecosse);
var linderman = new BreweryModel(name: "Brasserie Lindemans", description: "...", country: belgique);
var ninkasi = new BreweryModel(name: "Ninkasi", description: "...", country: france);

var blonde = new BeerColorModel(name: "Blonde");
var brune = new BeerColorModel(name: "Brune");
var blanche = new BeerColorModel(name: "Blanche");

var ipa = new BeerStyleModel(name: "IPA", description: "");
var lambic = new BeerStyleModel(name: "Lambic", description: "");
var speciale = new BeerStyleModel(name: "Spéciale", description: "");
var apa = new BeerStyleModel(name: "American pale ale", description: "");
var smok = new BeerStyleModel(name: "Smoked Beer", description: "");
var ale = new BeerStyleModel(name: "Blonde Ale", description: "");

var hop = new HopModel(name: "Houblon", description: "desc", alphaacid: 4);
var malt = new CerealModel(name: "Malt d'orge", description: "desc", ebc: 4);
var water = new AdditiveModel(name: "Eau", description: "de source", use: "pour rendre la bière liquide mon pote !");

IEnumerable<IngredientModel> ingredient = new[] { hop };

var punk = new BeerModel();
punk.Id = guid;
punk.Name = "Punk IPA";
punk.Brewery = brewdog;
punk.Ibu = 10;
punk.Degree = 5;
punk.Color = blonde;
punk.Style = ipa;
punk.Ingredients = ingredient;

await beerManager.Add(punk);
Console.WriteLine("punk ajouté");


var tokyo = new BeerModel();
tokyo.Name = "Tokyo";
tokyo.Brewery = brewdog;
tokyo.Ibu = 5;
tokyo.Degree = 6;
tokyo.Color = brune;
tokyo.Style = speciale;

await beerManager.Add(tokyo);
Console.WriteLine("tokyo ajouté");


var pony = new BeerModel();
pony.Name = "DEAD PONY CLUB";
pony.Brewery = brewdog;
pony.Ibu = 8;
pony.Degree = 4;
pony.Color = blonde;
pony.Style = apa;

await beerManager.Add(pony);
Console.WriteLine("pony ajouté");


var peche = new BeerModel();
peche.Name = "La Pêcheresse";
peche.Brewery = linderman;
peche.Ibu = 8;
peche.Degree = 4;
peche.Color = blonde;
peche.Style = lambic;

await beerManager.Add(peche);
Console.WriteLine("peche ajouté");


var kriek = new BeerModel();
kriek.Name = "Lindemans Kriek";
kriek.Brewery = linderman;
kriek.Ibu = 8;
kriek.Degree = 4;
kriek.Color = blonde;
kriek.Style = lambic;

await beerManager.Add(kriek);
Console.WriteLine("kriek ajouté");


var smoky = new BeerModel();
smoky.Name = "Smoky Oak Ale";
smoky.Brewery = ninkasi;
smoky.Ibu = 8;
smoky.Degree = 4;
smoky.Color = brune;
smoky.Style = smok;

await beerManager.Add(smoky);
Console.WriteLine("smoky ajouté");


var mango = new BeerModel();
mango.Name = "Mango No°5";
mango.Brewery = ninkasi;
mango.Ibu = 8;
mango.Degree = 4;
mango.Color = blonde;
mango.Style = ale;

await beerManager.Add(mango);
Console.WriteLine("mango ajouté");


var beers = await beerManager.GetAll();

Console.WriteLine("Execution terminée");
Console.ReadLine();
