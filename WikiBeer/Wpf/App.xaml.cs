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
        private const string ServerUrl = "https://localhost:7160";
        public IDataManager<BeerModel, BeerDto> BeerDataManager { get; }
        public HttpClient HttpClient{ get; set; }  
        public IMapper Mapper { get; }
        public INavigator Navigator { get; } = new Navigator();
        public App()
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(typeof(DtoModelProfile)));
            Mapper = new Mapper(configuration);
            HttpClient = new HttpClient();
            BeerDataManager = new BeerDataManager(HttpClient, Mapper, ServerUrl);
        }

        /// <summary>
        /// Se lance au démarage de l'app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Navigator.RegisterView(new HomeUC());
        }
    }
}
