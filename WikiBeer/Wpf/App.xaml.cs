using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Wpf.UserControls.Views;
using Ipme.WikiBeer.Wpf.Utils;
using System.Net.Http;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // A remplacer par un appel à une fonction utilitaire qui va chercher cette info dans l'API
        private const string ServerUrl = "https://localhost:7160";
        public IDataManager<BeerModel, BeerDto> BeerDataManager { get; }
        // Pas nécessaire on pourrait juste faire un new HttpClient() [voir IHttpClientFactory aussi pour de meilleurs performances]
        public HttpClient HttpClient{ get; set; } 
        // Même chose
        public IMapper Mapper { get; }

        public INavigator Navigator { get; } = new Navigator();

        public App()
        {
            // Création du BeerDataManager
            var configuration = new MapperConfiguration(config => config.AddProfile(typeof(DtoModelProfile)));
            Mapper = new Mapper(configuration);
            HttpClient = new HttpClient();
            BeerDataManager = new BeerDataManager(HttpClient, Mapper, ServerUrl);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // Définition des deux vues principales
            Navigator.RegisterView(new ViewLogin());
            Navigator.RegisterView(new ViewHome());
        }
    }
}
