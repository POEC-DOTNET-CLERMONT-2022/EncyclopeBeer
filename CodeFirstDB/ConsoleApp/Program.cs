/*
 Programme de test rapide des des controllers de type Get
 ----> A utiliser pour implémenter un ApiRequestManager coté client!

 Il est préférable de passer dans le debbuger pour comparer le swager aux sortie sous forme de model
 PAsser par swagger directement pour l'instant pour tester autre choise que les GetRequests

 Attention, pour l'instant on a un problème de conversion entre model et dto (sur les ingrédients)
 ----> à débugger avant de continuer les tests sur les controllers

 Pour l'instant via swagger les Get fonctionnent, le Delete aussi
 Le Post a encore des problèmes de sérialisation lié a NewtonJson (les ingredients encore...à creuser[peut etre une modif dans le header???])
 (Put à implémenter dans le repos avant de pouvoir
 tester dans le controller)  
*/
using AutoMapper;
using Dtos;
using Extensions.MapProfiles;
using Models;
using Newtonsoft.Json;

// Config Automappeur 
var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
var mapper = new Mapper(configuration);

// local host pour monter les requests
var localHost = "https://localhost:7137/";

// Controller à tester
var controller = "api/FakeBeer";

// Ressource à tester 
var ressource = "";
//var ressource = "GetbyName/"; //Name Guid à récuppérer à la main via swagger

// Montage de la requetes
var url = $"{localHost}{controller}{ressource}";

// Création de la requêtes
var getRequest = new HttpRequestMessage(HttpMethod.Get, url);
getRequest.Headers.Add("Accept", "application/json"); // header

// Création du client pour les requetes
var client = new HttpClient();

// Test du Get
var beersDto = await TestGetAsync(client, getRequest); // await nécessaire pour la transformation en list
var beersModel = mapper.Map<List<BeerModel>>(beersDto); // problème de mappage des ingrédients ici
// Construction nouvelle beer
var id = beersModel[0].Id;
//beer0.Name = "MaBeer";
//beer0.Id = Guid.NewGuid();

// Construction nouvelle requêtes
ressource = $"/GetbyId/{id}";
url = $"{localHost}{controller}{ressource}";

// Nouvelle requete
var getByIdRequest = new HttpRequestMessage(HttpMethod.Get, url);
getByIdRequest.Headers.Add("Accept", "application/json");

//async Task<BeerModel> TestGetByIdAsync(HttpClient client, HttpRequestMessage request)
//{
//    var response = await client.SendAsync(getByIdRequest);
//    if (response.IsSuccessStatusCode)
//    {
//        // Désérialisation avec NewwtonSoft pour ne pas avoir à décorer les Dto
//        var responseString = await response.Content.ReadAsStringAsync();
//        var beerDto = JsonConvert.DeserializeObject<BeerDto>(responseString,
//            GetJsonSettings());

//        // Mapping de Dto à Model
//        var beerModel = mapper.Map<BeerModel>(beerDto);

//        Console.WriteLine("GetRequest récupérée");
//        return beerModel;
//    }
//    else
//    {
//        Console.WriteLine("GetRequest non parsable");
//        //return new BeerModel();
//    }
//}


// async Task car gère de l'async
async Task<List<BeerDto>> TestGetAsync(HttpClient client, HttpRequestMessage request)
{
    var response = await client.SendAsync(request);

    if (response.IsSuccessStatusCode)
    {
        // Désérialisation avec NewwtonSoft pour ne pas avoir à décorer les Dto
        var responseString = await response.Content.ReadAsStringAsync();
        var beersDto = JsonConvert.DeserializeObject<List<BeerDto>>(responseString,
            GetJsonSettings());

        // Mapping de Dto à Model
        //var beersModel = mapper.Map<List<BeerModel>>(beersDto);

        Console.WriteLine("GetRequest récupérée");
        return beersDto;
    }
    else
    {
        Console.WriteLine("GetRequest non parsable");
        return new List<BeerDto>();
    }
}



// Les settings nécessaire au parsage (doit être le même coté API)
JsonSerializerSettings GetJsonSettings()
{
    return new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
}


//Console.ReadLine();// Pour bloquer la console ouverte
