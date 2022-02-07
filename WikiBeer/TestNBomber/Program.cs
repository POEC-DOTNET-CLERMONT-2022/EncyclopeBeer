// See https://aka.ms/new-console-template for more information
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using NBomber.Plugins.Network.Ping;

var Url = "https://localhost:7160/api/beers";
using var client = new HttpClient();
// version avec juste un httpclient
var step1 = Step.Create("step1", async context =>
{
    //var client = new HttpClient();
    var response = await client.GetAsync(Url);

    


    return Response.Ok();
});

// POur l'instant mal configuré -> ne fait que renvoyer des erreurs
var step2 = Step.Create("step2",
                           clientFactory: HttpClientFactory.Create(),
                           execute: context =>
                           {
                               var request = Http.CreateRequest("GET", Url)
                                                         .WithHeader("Accept", "*/*");

                               return Http.Send(request, context);
                           });
// creates ping plugin that brings additional reporting data
var pingPluginConfig = PingPluginConfig.CreateDefault(new[] { "localhost:7160/api" });
var pingPlugin = new PingPlugin(pingPluginConfig);

// second, we add our step to the scenario
var scenario = ScenarioBuilder
    .CreateScenario("simple_http", step2)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(
        Simulation.RampConstant(copies: 100, during: TimeSpan.FromSeconds(30))
        //Simulation.InjectPerSec(rate: 1000, during: TimeSpan.FromSeconds(30))
    );


NBomberRunner.RegisterScenarios(scenario).WithWorkerPlugins(pingPlugin).Run();