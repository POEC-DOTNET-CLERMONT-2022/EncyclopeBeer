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
    /// Logique d'interaction pour ViewBeers.xaml
    /// </summary>
    public partial class ViewBeers : UserControl
    {
        private IDataManager<BeerModel, BeerDto> _beerDataManager = ((App)Application.Current).BeerDataManager;
        private IDataManager<BreweryModel, BreweryDto> _breweryDataManager = ((App)Application.Current).BreweryDataManager;
        private IDataManager<BeerColorModel, BeerColorDto> _colorDataManager = ((App)Application.Current).ColorDataManager;
        private IDataManager<BeerStyleModel, BeerStyleDto> _styleDataManager = ((App)Application.Current).StyleDataManager;

        public GenericListModel<BeerModel> Beers { get; } 
        public GenericListModel<BreweryModel> Breweries { get; }
        public GenericListModel<BeerStyleModel> Styles { get; } 
        public GenericListModel<BeerColorModel> Colors { get; }

        public ViewBeers()
        {
            Beers = new GenericListModel<BeerModel>();
            Breweries = new GenericListModel<BreweryModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBeers();
            await LoadBreweries();
        }

        public async Task LoadBeers()
        {
            var beers = await _beerDataManager.GetAll();
            Beers.List = new ObservableCollection<BeerModel>(beers);
        }

        public async Task LoadBreweries()
        {
            var breweries = await _breweryDataManager.GetAll();
            Breweries.List = new ObservableCollection<BreweryModel>(breweries);
        }
    }
}
