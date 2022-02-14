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
            await LoadCountry();
            Breweries.ToModify = null;
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

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var name = Breweries.ToModify.Name;
            var description = Breweries.ToModify.Description;
            CountryModel country = (CountryModel)BreweryDetailsComponent.CountryBox.SelectedItem;
            var brewery = new BreweryModel(name, description, country);
            var newBrewery = await _breweryDataManager.Add(brewery);
            Breweries.List.Add(newBrewery);
            Breweries.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Breweries.ToModify != null)
            {
                await _breweryDataManager.Update(Breweries.ToModify.Id, Breweries.ToModify);
                var index = Breweries.List.IndexOf(Breweries.Current);
                Breweries.List[index] = Breweries.ToModify.DeepClone();
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Breweries.ToModify != null)
            {
                await _breweryDataManager.DeleteById(Breweries.ToModify.Id);
                Breweries.List.Remove(Breweries.Current);
                Breweries.ToModify = null;
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Collapsed;
            Delete_Button.Visibility = Visibility.Collapsed;
            Create_Button.Visibility = Visibility.Visible;
            ListOverlay.Visibility = Visibility.Visible;
            Breweries.ToModify = new BreweryModel(string.Empty, string.Empty, null);
        }
    }
}
