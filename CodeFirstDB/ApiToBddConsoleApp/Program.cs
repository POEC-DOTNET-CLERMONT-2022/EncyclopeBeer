/*
  Test de peuplement de la base de donnée.
 */
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using Dtos;
using Extensions.MapProfiles;
using Newtonsoft.Json;

// Config Automappeur 
var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
var mapper = new Mapper(configuration);

// Config Autofixture
var fixture = new Fixture();
fixture.Customizations.Add(new TypeRelay(typeof(IngredientDto),typeof(HopDto)));
var beer = fixture.Create<BeerDto>();

// local host pour monter les requests
var localHost = "https://localhost:7137/";

// Controller à tester
var controller = "api/Beer";
// Ressource à tester 
var ressource = "";
// Montage de la requetes
var url = $"{localHost}{controller}{ressource}";

// Création de la requête Get
var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
postRequest.Headers.Add("Accept", "application/json"); // header
var client = new HttpClient();

var response = await client.SendAsync(postRequest);
if (response.IsSuccessStatusCode)
{
    var responseString = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseString);
}
else
{
    Console.WriteLine("PostRequest non parsable");
}


//JsonSerializerSettings GetJsonSettings()
//{
//    return new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
//}


Console.ReadLine();
