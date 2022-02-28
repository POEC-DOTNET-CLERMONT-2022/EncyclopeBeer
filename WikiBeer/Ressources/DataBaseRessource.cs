using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System.Collections.ObjectModel;
using System.Linq;

/// <summary>
/// Classe contenant les ressources que l'on veut utiliser pour les tests d'intégration
/// Agit comme un client fictif qui remplirai une base vierge.
/// Note sur le Verbatim 
/// https://stackoverflow.com/questions/556133/whats-the-in-front-of-a-string-in-c
/// </summary>
namespace Ipme.WikiBeer.Ressources
{
    public class DataBaseRessource
    {
        public string ApiUrl { get; }
        public Mapper Mapper { get; }        
        public HttpClient Client { get; }       
        public BeerDataManager BeerManager { get; }
        public BreweryDataManager BreweryManager { get; }
        public CountryDataManager CountryManager { get; }
        public StyleDataManager StyleManager { get; }
        public ColorDataManager ColorManager { get; }
        public IngredientDataManager IngredientManager { get; }
        public UserDataManager UserManager { get; }

        public IEnumerable<BeerModel> Beers { get; set; }
        public IEnumerable<BreweryModel> Breweries { get; set; }
        public IEnumerable<CountryModel> Countries { get; set; }
        public IEnumerable<BeerStyleModel> Styles { get; set; }
        public IEnumerable<BeerColorModel> Colors { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
        public IEnumerable<UserModel> Users { get; set; }


        public DataBaseRessource(string url = "https://localhost:5001")
        {
            // Config Générale
            ApiUrl = url;
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
            Mapper = new Mapper(configuration);            
            Client = new HttpClient();

            // Managers
            BeerManager = new BeerDataManager(Client, Mapper, ApiUrl);
            BreweryManager = new BreweryDataManager(Client, Mapper, ApiUrl);
            CountryManager = new CountryDataManager(Client, Mapper, ApiUrl);
            StyleManager = new StyleDataManager(Client, Mapper, ApiUrl);
            ColorManager = new ColorDataManager(Client, Mapper, ApiUrl);
            IngredientManager = new IngredientDataManager(Client, Mapper, ApiUrl);
            UserManager = new UserDataManager(Client, Mapper, ApiUrl);
        }

        public void FillDatabase()
        {            
            Countries = InsertCountries();
            Breweries = InsertBreweries(Countries);
            Colors = InsertColors();
            Styles = InsertStyles();
            Ingredients = InsertIngredients();
            Beers = InsertBeers(Breweries, Styles, Colors, Ingredients);
            Users = InsertUsers(Countries, Beers);
        }

       
        #region Valeurs en dur
        public IEnumerable<CountryModel> InsertCountries()
        {
            var belgique = new CountryModel(name: "Belgique");
            var france = new CountryModel(name: "France");
            var ecosse = new CountryModel(name: "Ecosse");
            IEnumerable<CountryModel> countries = new CountryModel[] { belgique, france, ecosse };

            return AddAndWait(countries, CountryManager);
        }

        public IEnumerable<BreweryModel> InsertBreweries(IEnumerable<CountryModel> countries)
        {
            // Récupération pays pour brasseries
            var bddFrance = countries.FirstOrDefault(c => c.Name == "France");
            var bddBelgique = countries.FirstOrDefault(c => c.Name == "Belgique");
            var bddEcosse = countries.FirstOrDefault(c => c.Name == "Ecosse");

            // Brasseries
            var brewdog = new BreweryModel(name: "Brewdog", description: "Des chiens qui brassent", bddEcosse);
            var linderman = new BreweryModel(name: "Brasserie Lindemans", description: "Ils aiment les fruits", bddBelgique);
            var ninkasi = new BreweryModel(name: "Ninkasi", description: "A l'eau pure du Rhone", country: bddFrance);
            List<BreweryModel> breweries = new List<BreweryModel>() { brewdog, linderman, ninkasi };

            return AddAndWait(breweries, BreweryManager);
        }

        public IEnumerable<BeerColorModel> InsertColors()
        {
            var blonde = new BeerColorModel(name: "Blonde");
            var brune = new BeerColorModel(name: "Brune");
            var blanche = new BeerColorModel(name: "Blanche");
            var fruitee = new BeerColorModel(name: "Fruitée");
            IEnumerable<BeerColorModel> colors = new BeerColorModel[] { blonde, brune, blanche, fruitee };

            return AddAndWait(colors, ColorManager);
        }

        public IEnumerable<BeerStyleModel> InsertStyles()
        {
            var ipa = new BeerStyleModel(name: "IPA", description: "");
            var lambic = new BeerStyleModel(name: "Lambic", description: "");
            var speciale = new BeerStyleModel(name: "Spéciale", description: "");
            var apa = new BeerStyleModel(name: "American pale ale", description: "");
            var smok = new BeerStyleModel(name: "Smoked Beer", description: "");
            var ale = new BeerStyleModel(name: "Ale", description: "");
            IEnumerable<BeerStyleModel> styles = new BeerStyleModel[] { ale, speciale, apa, smok, lambic, ipa };

            return AddAndWait(styles, StyleManager);
        }

        public IEnumerable<IngredientModel> InsertIngredients()
        {
            ////Ingredients
            var hop = new HopModel(name: "Houblon", description: "Pour l'amertume !", alphaAcid: 10);
            var malt = new CerealModel(name: "Malt d'orge", description: "Du sucre pour nourir les levures !", ebc: 4);
            var water = new AdditiveModel(name: "Eau", description: "Ben c'est de l'eau quoi", use: "Pour rendre la bière liquide mon pote !");
            IEnumerable<IngredientModel> ingredients = new IngredientModel[] { hop, malt, water };

            return AddAndWait(ingredients, IngredientManager);
        }

        public IEnumerable<BeerModel> InsertBeers(IEnumerable<BreweryModel> breweries, IEnumerable<BeerStyleModel> styles,
            IEnumerable<BeerColorModel> colors, IEnumerable<IngredientModel> ingredients)
        {
            // Récupération brasseries
            var brewdog = breweries.FirstOrDefault(c => c.Name == "Brewdog");
            var linderman = breweries.FirstOrDefault(c => c.Name == "Brasserie Lindemans");
            var ninkasi = breweries.FirstOrDefault(c => c.Name == "Ninkasi");

            // Récupération styles
            var ipa = styles.FirstOrDefault(c => c.Name == "IPA");
            var lambic = styles.FirstOrDefault(c => c.Name == "Lambic");
            var speciale = styles.FirstOrDefault(c => c.Name == "Spéciale");
            var apa = styles.FirstOrDefault(c => c.Name == "American pale ale");
            var smoked = styles.FirstOrDefault(c => c.Name == "Smoked Beer");
            var ale = styles.FirstOrDefault(c => c.Name == "Ale");

            // Récupération couleurs
            var blonde = colors.FirstOrDefault(c => c.Name == "Blonde");
            var brune = colors.FirstOrDefault(c => c.Name == "Brune");
            var blanche = colors.FirstOrDefault(c => c.Name == "Blanche");
            var fruitee = colors.FirstOrDefault(c => c.Name == "Fruitée");

            // Récupération ingredients
            var hop = ingredients.FirstOrDefault(i => i.Name == "Houblon");
            var malt = ingredients.FirstOrDefault(i => i.Name == "Malt d'orge");
            var water = ingredients.FirstOrDefault(i => i.Name == "Eau");
            var un = new ObservableCollection<IngredientModel>() { water };
            var deux = new ObservableCollection<IngredientModel>() { water, hop };
            var trois = new ObservableCollection<IngredientModel>() { water, hop, malt };

            // bière
            var emptyImage = new ImageModel(new byte[] { });
            var punk = new BeerModel("PUNK IPA", "Avec ou sans créte", 1, 9, ipa, blonde, brewdog, emptyImage, un);
            var hazy = new BeerModel("HAZY JANE", "La préférée de Jimmy", 2, 10, ipa, blonde, brewdog, emptyImage, deux);
            var cloud = new BeerModel("BREWDOG VS CLOUDWATER", "Wouaf fait plouf", 3, 6, ale, blonde, brewdog, emptyImage, trois);
            var elvis = new BeerModel("ELVIS JUICE", "Oh Yeah", 4, 66, speciale, fruitee, brewdog, emptyImage, deux);
            var kriek = new BeerModel("KRIEK", "Pour tuer Sylvain", 5, 8, lambic, fruitee, linderman, emptyImage, un);
            var gueuze = new BeerModel("GUEUZE", "Messire!", 6, 4, lambic, brune, linderman, emptyImage, deux);
            var faro = new BeerModel("FARO LAMBIC", "Sans W", 7, 7, ale, fruitee, brewdog, emptyImage, trois);
            var nipa = new BeerModel("NINKASI IPA", "La lyonnaise classique", 8, (float)6.05, ipa, blonde, ninkasi, emptyImage, deux);
            var nblance = new BeerModel("NINKASI BLANCHE", "La lyonnaise au blé", 9, 3, ale, blanche, ninkasi, emptyImage, un);
            var npa = new BeerModel("NINKASI PALE ALE", "La lyonnaise d'Angleterre", 10, (float)4.5, smoked, brune, ninkasi, emptyImage, deux);
            IEnumerable<BeerModel> beers = new BeerModel[] { punk, hazy, cloud, elvis, kriek, gueuze, faro, nipa, nblance, npa };
            //AddAndWait(BeerManager, punk, hazy, ...);
            return AddAndWait(beers, BeerManager);
        }

        private IEnumerable<UserModel> InsertUsers(IEnumerable<CountryModel> countries, IEnumerable<BeerModel> beers)
        {
            // Récupération pays pour brasseries
            var bddFrance = countries.FirstOrDefault(c => c.Name == "France");
            var bddBelgique = countries.FirstOrDefault(c => c.Name == "Belgique");
            var bddEcosse = countries.FirstOrDefault(c => c.Name == "Ecosse");

            // Récupération bières
            var beersId0 = new ObservableCollection<Guid>();
            var beersId1 = new ObservableCollection<Guid>() { beers.ToList()[0].Id };
            var beersId2 = new ObservableCollection<Guid>() { beers.ToList()[0].Id , beers.ToList()[1].Id };

            // Users
            //var dede = new UserModel("Dédé", new DateTime(1960, 1, 1), "dede@bmail", 50, false, bddFrance, beersId0);
            //var maurice = new UserModel("Momo", new DateTime(1960, 2, 10), "momo@bmail", 60, true, bddBelgique, beersId1);
            //var marcel = new UserModel("FuturGuy", new DateTime(2050, 12, 30), "ff@bmail", 99, false, bddEcosse, beersId2);
            //IEnumerable<UserModel> users = new UserModel[] { dede, maurice, marcel };

            //string nickName, DateTime birthDate, bool isCertified,
            //ConnectionInfosModel connectionInfos, CountryModel? country, ObservableCollection< Guid > favoriteBeerIds
            var cArmel = new ConnectionInfosModel("auth0|6215640551eb1e00703daa24", "armel.pitelet@gmail.com", true);
            var cClement = new ConnectionInfosModel("auth0|6219e3e290b20b0070d50e7e", "clem.heritier@gmail.com", false);
            var armel = new UserModel("Armel", new DateTime(1960, 1, 1), false, cArmel, bddFrance, beersId0);
            var clement = new UserModel("Clément", new DateTime(1960, 2, 10), true, cClement, bddBelgique, beersId1);
            
            IEnumerable<UserModel> users = new UserModel[] { armel, clement };

            return AddAndWait(users, UserManager);
        }

        #endregion

        private IEnumerable<TModel> AddAndWait<TModel, TDto>(IEnumerable<TModel> models, IDataManager<TModel, TDto> manager)
                where TModel : class
                where TDto : class, IDto
        {
            var dbModels = new List<TModel>();
            foreach (var model in models)
            {
                var tt = manager.Add(model).Result;
                dbModels.Add(tt);
            }
            return dbModels;
        }
    }
}
