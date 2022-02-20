using Ipme.WikiBeer.ApiDatas;
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
    /// Logique d'interaction pour ViewIngredients.xaml
    /// </summary>
    public partial class ViewIngredients : UserControl, INotifyPropertyChanged
    {
        private IDataManager<IngredientModel, IngredientDto> _ingredientDataManager = ((App)Application.Current).IngredientDataManager;

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
                ((CollectionViewSource)Resources["IngredientsViewSource"]).View.Refresh();
            }
        }

        public ViewIngredients()
        {
            Ingredients = new GenericListModel<IngredientModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadIngredients();
        }

        public async Task LoadIngredients()
        {
            var ingredients = await _ingredientDataManager.GetAll();
            Ingredients.List = new ObservableCollection<IngredientModel>(ingredients);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var newIngredient = await _ingredientDataManager.Add(Ingredients.ToModify);
            Ingredients.List.Add(newIngredient);

            TypesSelector.Visibility = Visibility.Collapsed;
            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Ingredients.ToModify != null)
            {
                await _ingredientDataManager.Update(Ingredients.ToModify.Id, Ingredients.ToModify);
                var index = Ingredients.List.IndexOf(Ingredients.Current);
                Ingredients.List[index] = Ingredients.ToModify.DeepClone();
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Ingredients.ToModify != null)
            {
                await _ingredientDataManager.DeleteById(Ingredients.ToModify.Id);
                Ingredients.List.Remove(Ingredients.Current);
                Ingredients.ToModify = null;
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            TypesSelector.Visibility = Visibility.Collapsed;
            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            TypesSelector.Visibility = Visibility.Visible;
            Update_Button.Visibility = Visibility.Collapsed;
            Delete_Button.Visibility = Visibility.Collapsed;
            Create_Button.Visibility = Visibility.Visible;
            ListOverlay.Visibility = Visibility.Visible;
            Ingredients.ToModify = null;
        }

        private void HopRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Ingredients.ToModify = new HopModel(string.Empty, string.Empty, 0);
        }

        private void CerealRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Ingredients.ToModify = new CerealModel(string.Empty, string.Empty, 0);
        }

        private void AdditiveRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Ingredients.ToModify = new AdditiveModel(string.Empty, string.Empty, string.Empty);
        }

        public void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var ingredient = e.Item as IngredientModel;

            if (string.IsNullOrWhiteSpace(TextSearch))
            {
                e.Accepted = true;
                return;
            }

            if (ingredient != null)
            {
                if (!string.IsNullOrWhiteSpace(ingredient.Name) && ingredient.Name.ToLower().Contains(TextSearch.ToLower()))
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
    }
}
