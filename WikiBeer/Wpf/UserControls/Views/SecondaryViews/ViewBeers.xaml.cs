using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
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
    /// Logique d'interaction pour ViewBeers.xaml
    /// </summary>
    public partial class ViewBeers : UserControl, INotifyPropertyChanged
    {
        private IDataManager<BeerModel, BeerDto> _beerDataManager = ((App)Application.Current).BeerDataManager;
        private IDataManager<BreweryModel, BreweryDto> _breweryDataManager = ((App)Application.Current).BreweryDataManager;
        private IDataManager<BeerColorModel, BeerColorDto> _colorDataManager = ((App)Application.Current).ColorDataManager;
        private IDataManager<BeerStyleModel, BeerStyleDto> _styleDataManager = ((App)Application.Current).StyleDataManager;
        private IDataManager<IngredientModel, IngredientDto> _ingredientDataManager = ((App)Application.Current).IngredientDataManager;

        public IGenericListModel<BeerModel> Beers { get; } 
        public IGenericListModel<BreweryModel> Breweries { get; }
        public IGenericListModel<BeerStyleModel> Styles { get; }
        public IGenericListModel<BeerColorModel> Colors { get; }
        public IGenericListModel<IngredientModel> Ingredients { get; }

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
                ((CollectionViewSource)Resources["BeersViewSource"]).View.Refresh();
            }
        }

        public ViewBeers()
        {
            Beers = new GenericListModel<BeerModel>();
            Breweries = new GenericListModel<BreweryModel>();
            Styles = new GenericListModel<BeerStyleModel>();
            Colors = new GenericListModel<BeerColorModel>();
            Ingredients = new GenericListModel<IngredientModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            Task[] tasks = { LoadBeers(), LoadBreweries(),
                LoadStyles(), LoadColors(), LoadIngredients() };
            await Task.WhenAll(tasks);            
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
        public async Task LoadStyles()
        {
            var styles = await _styleDataManager.GetAll();
            Styles.List = new ObservableCollection<BeerStyleModel>(styles);
        }

        public async Task LoadColors()
        {
            var colors = await _colorDataManager.GetAll();
            Colors.List = new ObservableCollection<BeerColorModel>(colors);
        }

        public async Task LoadIngredients()
        {
            var ingredients = await _ingredientDataManager.GetAll();
            Ingredients.List = new ObservableCollection<IngredientModel>(ingredients);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            //var name = Beers.ToModify.Name;
            //var description = Beers.ToModify.Description;
            //float ibu = Beers.ToModify.Ibu;
            //float abv = Beers.ToModify.Degree;
            //BreweryModel brewery = (BreweryModel)BeerDetailsComponent.BreweriesComboBox.SelectedItem;
            //BeerStyleModel style = (BeerStyleModel)BeerDetailsComponent.StylesComboBox.SelectedItem;
            //BeerColorModel color = (BeerColorModel)BeerDetailsComponent.ColorsComboBox.SelectedItem;
            //IngredientModel ingredient = (IngredientModel)BeerDetailsComponent.IngredientsComboBox.SelectedItem;
            //ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel>();
            //ingredients.Add(ingredient);
            //var beer = new BeerModel(name, description, ibu, abv, style, color, brewery, ingredients);
            //var newBeer = await _beerDataManager.Add(beer);
            //Beers.List.Add(newBeer);
            //Beers.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Beers.ToModify != null)
            {
                await _beerDataManager.Update(Beers.ToModify.Id, Beers.ToModify);
                var index = Beers.List.IndexOf(Beers.Current);
                Beers.List[index] = Beers.ToModify.DeepClone();
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Beers.ToModify != null)
            {
                await _beerDataManager.DeleteById(Beers.ToModify.Id);
                Beers.List.Remove(Beers.Current);
                Beers.ToModify = null;
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
            Beers.ToModify = new BeerModel();
        }


        public void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var beer = e.Item as BeerModel;

            if (string.IsNullOrWhiteSpace(TextSearch))
            {
                e.Accepted = true;
                return;
            }

            if (beer != null)
            {
                string searchParams = BuildBeerSearchParams(beer);
                if (!string.IsNullOrWhiteSpace(searchParams) && searchParams.Contains(TextSearch.ToLower()))
                {
                    e.Accepted = true;
                    return;
                }

                e.Accepted = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string BuildBeerSearchParams(BeerModel beer)
        {
            string searchParams = beer.Name;

            if (beer.Brewery != null)
            {
                searchParams = searchParams + " " + beer.Brewery.Name;

                if (beer.Brewery.Country != null)
                {
                    searchParams = searchParams + "" + beer.Brewery.Country.Name;
                }

            }
            if (beer.Style != null)
            {
                searchParams = searchParams + " " + beer.Style.Name;
            }
            if (beer.Color != null)
            {
                searchParams = searchParams + " " + beer.Color.Name;
            }

            return searchParams.ToLower();
        }
    }
}
