using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using Ipme.WikiBeer.Wpf.Test;
using Ipme.WikiBeer.Wpf.UserControls.Views;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using Auth0.OidcClient;

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
        public IDataManager<BreweryModel, BreweryDto> BreweryDataManager { get; }
        public IDataManager<CountryModel, CountryDto> CountryDataManager { get; }
        public IDataManager<BeerStyleModel, BeerStyleDto> StyleDataManager { get; }
        public IDataManager<BeerColorModel, BeerColorDto> ColorDataManager { get; }
        public IDataManager<IngredientModel, IngredientDto> IngredientDataManager { get; }
        public IDataManager<UserModel, UserDto> UserDataManager { get; }

        // Pas nécessaire on pourrait juste faire un new HttpClient() [voir IHttpClientFactory aussi pour de meilleurs performances]
        public HttpClient HttpClient{ get; set; } 
        // Même chose
        public IMapper Mapper { get; }

        public INavigator Navigator { get; } = new Navigator();

        public Auth0Client AuthClient { get; set; }

        Auth0ClientOptions clientOptions = new Auth0ClientOptions()
        {
            Domain = "docapc.eu.auth0.com",
            ClientId = "gWxCRG1wMFIllvK17H6dI4QeRrRR3DGt"
        };

        public App()
        {
            // Création du BeerDataManager
            var configuration = new MapperConfiguration(config => config.AddProfile(typeof(DtoModelProfile)));
            Mapper = new Mapper(configuration);
            HttpClient = new HttpClient();
            BeerDataManager = new BeerDataManager(HttpClient, Mapper, ServerUrl);
            BreweryDataManager = new BreweryDataManager(HttpClient, Mapper, ServerUrl);
            CountryDataManager = new CountryDataManager(HttpClient, Mapper, ServerUrl);
            StyleDataManager = new StyleDataManager(HttpClient, Mapper, ServerUrl);
            ColorDataManager = new ColorDataManager(HttpClient, Mapper, ServerUrl);
            IngredientDataManager = new IngredientDataManager(HttpClient, Mapper, ServerUrl);
            UserDataManager = new UserDataManager(HttpClient, Mapper, ServerUrl);

            AuthClient = new Auth0Client(clientOptions);
            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // Définition des deux vues principales
            Navigator.RegisterView(new ViewLogin());
            Navigator.RegisterView(new ViewHome());
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorTraceListener());

        }
    }
}
