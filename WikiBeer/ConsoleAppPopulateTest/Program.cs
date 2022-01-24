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

// Création d'une bière
var beer = fixture.Create<BeerModel>();

await beerManager.Add(beer);
var beers = await beerManager.GetAll();


Console.ReadLine();
