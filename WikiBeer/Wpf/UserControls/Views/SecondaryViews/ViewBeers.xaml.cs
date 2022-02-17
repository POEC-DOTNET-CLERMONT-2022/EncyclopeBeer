using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
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
        private IDataManager<IngredientModel, IngredientDto> _ingredientDataManager = ((App)Application.Current).IngredientDataManager;

        public IGenericListModel<BeerModel> Beers { get; } 
        public IGenericListModel<BreweryModel> Breweries { get; }
        public IGenericListModel<BeerStyleModel> Styles { get; }
        public IGenericListModel<BeerColorModel> Colors { get; }
        public IGenericListModel<IngredientModel> Ingredients { get; }

        public bool IsUpdating = false;

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
            await LoadBeers();
            await LoadBreweries();
            await LoadStyles();
            await LoadColors();
            await LoadIngredients();
            Beers.ToModify = null;
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
            var name = Beers.ToModify.Name;
            var description = Beers.ToModify.Description;
            float ibu = Beers.ToModify.Ibu;
            float abv = Beers.ToModify.Degree;
            BreweryModel brewery = (BreweryModel)BeerDetailsComponent.BreweriesComboBox.SelectedItem;
            BeerStyleModel style = (BeerStyleModel)BeerDetailsComponent.StylesComboBox.SelectedItem;
            BeerColorModel color = (BeerColorModel)BeerDetailsComponent.ColorsComboBox.SelectedItem;
            IngredientModel ingredient = (IngredientModel)BeerDetailsComponent.IngredientsComboBox.SelectedItem;
            ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel>();
            ingredients.Add(ingredient);
            var beer = new BeerModel(name, description, ibu, abv, style, color, brewery, ingredients);
            var newBeer = await _beerDataManager.Add(beer);
            Beers.List.Add(newBeer);
            Beers.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            //if (Beers.ToModify != null)
            //{
            //    await _beerDataManager.Update(Beers.ToModify.Id, Beers.ToModify);
            //    var index = Beers.List.IndexOf(Beers.Current);
            //    Beers.List[index] = Beers.ToModify.DeepClone();
            //}
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
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Visible;
            Beers.ToModify = new BeerModel();
        }
    }
}
