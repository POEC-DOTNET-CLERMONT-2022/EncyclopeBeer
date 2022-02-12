﻿using AutoMapper;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using Ipme.WikiBeer.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;

/// <summary>
/// Classe contenant les ressources que l'on veut utiliser pour les tests d'intégration
/// Agit comme un client fictif qui remplirai une base vierge.
/// Note : pour démarer une appli à partir d'une autre appli : (classe process)
/// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=net-6.0
/// https://stackoverflow.com/questions/70690219/net-6-0-configuration-files
/// </summary>
namespace Ipme.WikiBeer.Tools
{
    public class DataBaseRessource
    {
        private Process Api { get; }
        public Mapper Mapper { get; }
        public string ApiUrl { get; }
        public HttpClient Client { get; }

        public string ConnectionString { get; }

        public BeerDataManager BeerManager { get; }
        public BreweryDataManager BreweryManager { get; }
        public CountryDataManager CountryManager { get; }
        public StyleDataManager StyleManager { get; }
        public ColorDataManager ColorManager { get; }
        public IngredientDataManager IngredientManager { get; }

        public IEnumerable<BeerModel> Beers { get; set; }
        public IEnumerable<BreweryModel> Breweries { get; set; }
        public IEnumerable<CountryModel> Countries { get; set; }
        public IEnumerable<BeerStyleModel> Styles { get; set; }
        public IEnumerable<BeerColorModel> Colors { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }

        public DataBaseRessource(string dbName, string url = "https://localhost:7160")
        {
            // Config            
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

            // DataBase            
            ConnectionString = $"Data Source = (LocalDb)\\MSSQLLocalDB; Initial Catalog = {dbName}; Integrated Security = True;";
            
            // Démaragge automatique de l'api

        }

        public void StartApi(string[] apiArgs)
        {
            Process
            var api = Program.Main({ string[] apiArgs})
        }

        public void FillDatabase()
        {
            EnsureDatabaseCreation();
            Countries = InsertCountries();
            Breweries = InsertBreweries(Countries);
            Colors = InsertColors();
            Styles = InsertStyles();
            Ingredients = InsertIngredients();
            Beers = InsertBeer(Breweries, Styles, Colors, Ingredients);
        }

        public void EnsureDatabaseCreation()
        {
            using (var context = new WikiBeerSqlContext(GetContextOptions()))
            {
                context.Database.EnsureCreated();
            }
        }

        public void DropDataBase()
        {            
            using (var context = new WikiBeerSqlContext(GetContextOptions()))
            {
                context.Database.EnsureDeleted();
            }
        }

        private DbContextOptions<WikiBeerSqlContext> GetContextOptions()
        {
            var contextOptionBuilder = new DbContextOptionsBuilder<WikiBeerSqlContext>();
            contextOptionBuilder.UseSqlServer(ConnectionString);
            return contextOptionBuilder.Options;
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

        public IEnumerable<BeerModel> InsertBeer(IEnumerable<BreweryModel> breweries, IEnumerable<BeerStyleModel> styles,
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
            var punk = new BeerModel("PUNK IPA", "Avec ou sans créte", 1, 9, ipa, blonde, brewdog, un);
            var hazy = new BeerModel("HAZY JANE", "La préférée de Jimmy", 2, 10, ipa, blonde, brewdog, deux);
            var cloud = new BeerModel("BREWDOG VS CLOUDWATER", "Wouaf fait plouf", 3, 6, ale, blonde, brewdog, trois);
            var elvis = new BeerModel("ELVIS JUICE", "Oh Yeah", 4, 66, speciale, fruitee, brewdog, deux);
            var kriek = new BeerModel("KRIEK", "Pour tuer Sylvain", 5, 8, lambic, fruitee, linderman, un);
            var gueuze = new BeerModel("GUEUZE", "Messire!", 6, 4, lambic, brune, linderman, deux);
            var faro = new BeerModel("FARO LAMBIC", "Sans W", 7, 7, ale, fruitee, brewdog, trois);
            var nipa = new BeerModel("NINKASI IPA", "La lyonnaise classique", 8, (float)6.05, ipa, blonde, ninkasi, deux);
            var nblance = new BeerModel("NINKASI BLANCHE", "La lyonnaise au blé", 9, 3, ale, blanche, ninkasi, un);
            var npa = new BeerModel("NINKASI PALE ALE", "La lyonnaise d'Angleterre", 10, (float)4.5, smoked, brune, ninkasi, deux);
            IEnumerable<BeerModel> beers = new BeerModel[] { punk, hazy, cloud, elvis, kriek, gueuze, faro, nipa, nblance, npa };

            return AddAndWait(beers, BeerManager);
        }
        #endregion

        private IEnumerable<TModel> AddAndWait<TModel, TDto>(IEnumerable<TModel> models, IDataManager<TModel, TDto> manager)
                where TModel : class
                where TDto : class, IDto
        {
            var dbModels = new List<TModel>();
            foreach (var model in models)
            {
                dbModels.Add(manager.Add(model).Result);
            }
            return dbModels;
        }
    }
}
