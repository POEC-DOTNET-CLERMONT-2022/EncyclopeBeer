using AutoMapper;
//using Ipme.WikiBeer.ApiDatas;
//using Ipme.WikiBeer.ApiDatas.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System.Collections.ObjectModel;

/// <summary>
/// Classe contenant les ressources que l'on veut utiliser pour les tests d'intégration
/// </summary>
namespace Ipme.WikiBeer.Tools
{
    //public class DataBaseRessource
    //{
    //    public Mapper Mapper { get; }
    //    public string ApiUrl { get; private set; }
    //    public HttpClient Client { get; private set; }
    //    public BeerDataManager BeerManager { get; private set; }
    //    public BreweryDataManager BreweryManager { get; private set; }
    //    public CountryDataManager CountryManager { get; private set; }
    //    public StyleDataManager StyleManager { get; private set; }
    //    public ColorDataManager ColorManager { get; private set; }
    //    public DataBaseRessource()
    //    {
    //        // Config
    //        var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoModelProfile)));
    //        Mapper = new Mapper(configuration);
    //        ApiUrl = "https://localhost:7160";
    //        Client = new HttpClient();

    //        // Managers
    //        BeerManager = new BeerDataManager(Client, Mapper, ApiUrl);
    //        BreweryManager = new BreweryDataManager(Client, Mapper, ApiUrl);
    //        CountryManager = new CountryDataManager(Client, Mapper, ApiUrl);
    //        StyleManager = new StyleDataManager(Client, Mapper, ApiUrl);
    //        ColorManager = new ColorDataManager(Client, Mapper, ApiUrl);
    //    }
    
    //    public IEnumerable<CountryModel> GetCountriesSample()
    //    {
    //        var belgique = new CountryModel(name: "Belgique");
    //        var france = new CountryModel(name: "France");
    //        var ecosse = new CountryModel(name: "Ecosse");
    //        return new List<CountryModel>() { belgique, france, ecosse};
    //    }
        
    //    AddAndWait<CountryModel, CountryDto>(countries, countryManager);

        

    //        //Récupération pays pour brasseries
    //        var bddcountry = await countryManager.GetAll();
    //        var bddFrance = bddcountry.FirstOrDefault(c => c.Name == "France");
    //        var bddBelgique = bddcountry.FirstOrDefault(c => c.Name == "Belgique");
    //        var bddEcosse = bddcountry.FirstOrDefault(c => c.Name == "Ecosse");

    //        // Brasseries
    //        var brewdog = new BreweryModel(name: "Brewdog", description: "Des chiens qui brassent", bddEcosse);
    //        var linderman = new BreweryModel(name: "Brasserie Lindemans", description: "Ils aiment les fruits", bddBelgique);
    //        var ninkasi = new BreweryModel(name: "Ninkasi", description: "A l'eau pure du Rhone", country: bddFrance);
    //        List<BreweryModel> breweries = new List<BreweryModel>();
    //        breweries.Add(ninkasi);
    //        breweries.Add(brewdog);
    //        breweries.Add(linderman);
    //        AddAndWait<BreweryModel, BreweryDto>(breweries, breweryManager);

    //        //Récupération brasseries pour bières
    //        var bddBreweries = await breweryManager.GetAll();
    //        var bddBrewdog = bddBreweries.FirstOrDefault(c => c.Name == "Brewdog");
    //        var bddLinderman = bddBreweries.FirstOrDefault(c => c.Name == "Brasserie Lindemans");
    //        var bddNinkasi = bddBreweries.FirstOrDefault(c => c.Name == "Ninkasi");

    //        // Couleurs
    //        var blonde = new BeerColorModel(name: "Blonde");
    //        var brune = new BeerColorModel(name: "Brune");
    //        var blanche = new BeerColorModel(name: "Blanche");
    //        var fruitee = new BeerColorModel(name: "Fruitée");
    //        List<BeerColorModel> colors = new List<BeerColorModel>();
    //        colors.Add(blonde);
    //        colors.Add(brune);
    //        colors.Add(blanche);
    //        colors.Add(fruitee);
    //        AddAndWait<BeerColorModel, BeerColorDto>(colors, colorManager);

    //        //Récupération couleur pour bières
    //        var bddColor = await colorManager.GetAll();
    //        var bddBlonde = bddColor.FirstOrDefault(c => c.Name == "Blonde");
    //        var bddBrune = bddColor.FirstOrDefault(c => c.Name == "Brune");
    //        var bddBlanche = bddColor.FirstOrDefault(c => c.Name == "Blanche");
    //        var bddFruitee = bddColor.FirstOrDefault(c => c.Name == "Fruitée");

    //        // Styles
    //        var ipa = new BeerStyleModel(name: "IPA", description: "");
    //        var lambic = new BeerStyleModel(name: "Lambic", description: "");
    //        var speciale = new BeerStyleModel(name: "Spéciale", description: "");
    //        var apa = new BeerStyleModel(name: "American pale ale", description: "");
    //        var smok = new BeerStyleModel(name: "Smoked Beer", description: "");
    //        var ale = new BeerStyleModel(name: "Ale", description: "");
    //        List<BeerStyleModel> styles = new List<BeerStyleModel>();
    //        styles.Add(ale);
    //        styles.Add(speciale);
    //        styles.Add(apa);
    //        styles.Add(smok);
    //        styles.Add(lambic);
    //        styles.Add(ipa);
    //        AddAndWait<BeerStyleModel, BeerStyleDto>(styles, styleManager);

    //        //Récupération styles pour bières
    //        var bddStyle = await styleManager.GetAll();
    //        var bddIPA = bddStyle.FirstOrDefault(c => c.Name == "IPA");
    //        var bddLambic = bddStyle.FirstOrDefault(c => c.Name == "Lambic");
    //        var bddSpeciale = bddStyle.FirstOrDefault(c => c.Name == "Spéciale");
    //        var bddAPA = bddStyle.FirstOrDefault(c => c.Name == "American pale ale");
    //        var bddSmoked = bddStyle.FirstOrDefault(c => c.Name == "Smoked Beer");
    //        var bddAle = bddStyle.FirstOrDefault(c => c.Name == "Ale");

    //        // Ingredients
    //        var hop = new HopModel(name: "Houblon", description: "Pour l'amertume !", alphaAcid: 4);
    //        var malt = new CerealModel(name: "Malt d'orge", description: "Du sucre pour nourir les levures !", ebc: 4);
    //        var water = new AdditiveModel(name: "Eau", description: "Ben c'est de l'eau quoi", use: "Pour rendre la bière liquide mon pote !");
    //        ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel> { hop, malt, water };

    //        //Beers
    //        //var randomFloat = new RandomFloat();
    //        //var randomABV = randomFloat.RandABV();
    //        //var randomIBU = randomFloat.RandIBU();
    //        var ibu = 35F;
    //        var abv = 5.5F;

    //        var punk = new BeerModel("PUNK IPA", "Avec ou sans créte", abv, ibu, bddIPA, bddBlonde, bddBrewdog, ingredients);
    //        beerManager.Add(punk).Wait();

    //        //Récupération ingrédients pour bière
    //        var bddBeer = await beerManager.GetAll();
    //        var bddPunk = await beerManager.GetById(bddBeer.ToList()[0].Id);

    //        // Suite Beers
    //        var hazy = new BeerModel("HAZY JANE", "", abv, ibu, bddIPA, bddBlonde, bddBrewdog, bddPunk.Ingredients);
    //        var cloud = new BeerModel("BREWDOG VS CLOUDWATER", "", abv, ibu, bddAle, bddBlonde, bddBrewdog, bddPunk.Ingredients);
    //        var elvis = new BeerModel("ELVIS JUICE", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddPunk.Ingredients);
    //        var kriek = new BeerModel("KRIEK", "", abv, ibu, bddLambic, bddFruitee, bddLinderman, bddPunk.Ingredients);
    //        var gueuze = new BeerModel("GUEUZE", "", abv, ibu, bddLambic, bddBlonde, bddLinderman, bddPunk.Ingredients);
    //        var faro = new BeerModel("FARO LAMBIC", "", abv, ibu, bddAle, bddFruitee, bddBrewdog, bddPunk.Ingredients);
    //        var nipa = new BeerModel("NINKASI IPA", "", abv, ibu, bddIPA, bddBlonde, bddNinkasi, bddPunk.Ingredients);
    //        var nblance = new BeerModel("NINKASI BLANCHE", "", abv, ibu, bddAle, bddBlanche, bddNinkasi, bddPunk.Ingredients);
    //        var npa = new BeerModel("NINKASI PALE ALE", "", abv, ibu, bddAle, bddBrune, bddNinkasi, bddPunk.Ingredients);
    //        List<BeerModel> beers = new List<BeerModel>();
    //        beers.Add(hazy);
    //        beers.Add(cloud);
    //        beers.Add(elvis);
    //        beers.Add(kriek);
    //        beers.Add(gueuze);
    //        beers.Add(faro);
    //        beers.Add(nipa);
    //        beers.Add(nblance);
    //        beers.Add(npa);
    //        beers.ForEach(async beer => await beerManager.Add(beer));
    //        AddAndWait<BeerModel, BeerDto>(beers, beerManager);
    //        // Récupération pour check dans le debuger
    //        var bddBeers = await beerManager.GetAll();

        
    //    #endregion

    //    #region Tests divers
    //    //var toDeleteStyle = bddAle;
    //    //styleManager.DeleteById(bddAle.Id).Wait();
    //    var modifiedbddBeers = await beerManager.GetAll();
    //    var bddStyles = await styleManager.GetAll();
    //    var updatedBeer = modifiedbddBeers.ToList()[0];
    //    updatedBeer.Name = "Biere modifiée";
    //    updatedBeer.Style = bddStyles.ToList()[0];
    //    beerManager.Update(updatedBeer.Id, updatedBeer).Wait();
    //    updatedBeer.Name = "A nouveau modifée";
    //    updatedBeer.Ingredients.RemoveAt(0);
    //    beerManager.Update(updatedBeer.Id, updatedBeer).Wait();
    //    var newUpdatedBeer = await beerManager.GetById(updatedBeer.Id);
    //    var updatedStyle = updatedBeer.Style;
    //    updatedStyle.Description = "Un gout fumé";
    //    styleManager.Update(updatedBeer.Style.Id, updatedStyle).Wait();
    //    styleManager.DeleteById(updatedStyle.Id).Wait();

    //    #endregion



    //    #region Méthode(s) locale(s)
    //    /// <summary>
    //    /// Méthode qui permet l'ajout via méthodes asynchrone (de IDataManager)
    //    /// tt en attendant la fin des taches avant de lacher la main
    //    /// Equivalent d'un foreach manager.Add(item).RunAsynchronously() au final
    //    /// </summary>
    //    private void AddAndWait<TModel, TDto>(List<TModel> list, IDataManager<TModel, TDto> manager)
    //        where TModel : class where TDto : class
    //    {
    //        var tasks = new List<Task>();
    //        foreach (var item in list)
    //        {
    //            //var task = manager.Add(item);
    //            tasks.Add(manager.Add(item));
    //        }
    //        Task.WaitAll(tasks.ToArray());
    //    }
    //    #endregion

    //}
}