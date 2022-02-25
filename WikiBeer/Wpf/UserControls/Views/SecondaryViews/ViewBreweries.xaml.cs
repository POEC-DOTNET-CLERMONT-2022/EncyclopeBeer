using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewBrewery.xaml
    /// </summary>
    public partial class ViewBreweries : UserControl, INotifyPropertyChanged
    {
        private IDataManager<BreweryModel, BreweryDto> _breweryDataManager = ((App)Application.Current).BreweryDataManager;
        private IDataManager<CountryModel, CountryDto> _countryDataManager = ((App)Application.Current).CountryDataManager;

        public IGenericListModel<BreweryModel> Breweries { get; }
        public IGenericListModel<CountryModel> Countries { get; }

        private string _textSearch;
        public string TextSearch
        {
            get
            {
                return _textSearch;
            }
            set
            {
                _textSearch = value;
                OnPropertyChanged();
                ((CollectionViewSource)Resources["BreweriesViewSource"]).View.Refresh();
            }
        }

        public ViewBreweries()
        {
            Breweries = new GenericListModel<BreweryModel>();
            Countries = new GenericListModel<CountryModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBreweries();
            await LoadCountry();
            Breweries.ToModify = null;
            List.UnselectAll();
        }

        public async Task LoadBreweries()
        {
            var breweries = await _breweryDataManager.GetAll();
            Breweries.List = new ObservableCollection<BreweryModel>(breweries);
        }

        public async Task LoadCountry()
        {
            var countries = await _countryDataManager.GetAll();
            Countries.List = new ObservableCollection<CountryModel>(countries);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            //var name = Breweries.ToModify.Name;
            //var description = Breweries.ToModify.Description;
            //CountryModel country = (CountryModel)BreweryDetailsComponent.CountryBox.SelectedItem;
            //var brewery = new BreweryModel(name, description, country);
            var newBrewery = await _breweryDataManager.Add(Breweries.ToModify);
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

        public void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var brewery = e.Item as BreweryModel;

            if (string.IsNullOrWhiteSpace(TextSearch))
            {
                e.Accepted = true;
                return;
            }

            if (brewery != null)
            {
                string searchParams = BuildBrewerySearchParams(brewery);
                if (!string.IsNullOrWhiteSpace(searchParams) && searchParams.Contains(TextSearch.ToLower()))
                {
                    e.Accepted = true;
                    return;
                }

                e.Accepted = false;
            }
        }

        private string BuildBrewerySearchParams(BreweryModel brewery)
        {
            string searchParams = brewery.Name;

            if (brewery.Country != null)
            {
                searchParams = searchParams + " " + brewery.Country.Name;
            }

            return searchParams.ToLower();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
