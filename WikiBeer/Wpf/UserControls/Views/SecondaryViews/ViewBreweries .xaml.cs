using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewBrewery.xaml
    /// </summary>
    public partial class ViewBreweries : UserControl
    {
        private IDataManager<BeerModel, BeerDto> _beerDataManager = ((App)Application.Current).BeerDataManager;
        private IDataManager<BreweryModel, BreweryDto> _breweryDataManager = ((App)Application.Current).BreweryDataManager;
        private IDataManager<CountryModel, CountryDto> _countryDataManager = ((App)Application.Current).CountryDataManager;

        public IGenericListModel<BeerModel> Beers { get; }
        public IGenericListModel<BreweryModel> Breweries { get; }
        public IGenericListModel<CountryModel> Countries { get; }

        public ViewBreweries()
        {
            Beers = new GenericListModel<BeerModel>();
            Breweries = new GenericListModel<BreweryModel>();
            Countries = new GenericListModel<CountryModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBreweries();
            await LoadBeers();
        }

        public async Task LoadBreweries()
        {
            var breweries = await _breweryDataManager.GetAll();
            Breweries.List = new ObservableCollection<BreweryModel>(breweries);
        }

        public async Task LoadBeers()
        {
            var beers = await _beerDataManager.GetAll();
            Beers.List = new ObservableCollection<BeerModel>(beers);
        }

        public async Task LoadCountry()
        {
            var countries = await _countryDataManager.GetAll();
            Countries.List = new ObservableCollection<CountryModel>(countries);
        }
    }
}
