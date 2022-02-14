using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewIngredients.xaml
    /// </summary>
    public partial class ViewIngredients : UserControl
    {
        private IDataManager<IngredientModel, IngredientDto> _ingredientDataManager = ((App)Application.Current).IngredientDataManager;

        public IGenericListModel<IngredientModel> Ingredients { get; }

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
    }
}
