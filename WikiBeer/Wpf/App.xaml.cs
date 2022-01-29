using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.Wpf.UC;
using Ipme.WikiBeer.Wpf.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        // Nécessaire par contre
        public INavigator Navigator { get; } = new Navigator();
        public App()
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(typeof(DtoModelProfile)));
            Mapper = new Mapper(configuration);
            HttpClient = new HttpClient();
            BeerDataManager = new BeerDataManager(HttpClient, Mapper, ServerUrl);
        }

        /// <summary>
        /// Se lance au démarage de l'app (voir Startup dans le xaml)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Navigator.RegisterView(new HomeUC());
        }
    }
}
